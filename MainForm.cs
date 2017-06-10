using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FileChecksum
{
	public partial class MainForm : Form
	{
		private class ChecksumResult
		{
			public string MD5;
			public string SHA1;
			public string SHA256;
			public string SHA512;
			public string CRC32;
		}

		public MainForm(string path)
		{
			InitializeComponent();

			md5TextBox.Text =
			sha1TextBox.Text =
			sha256TextBox.Text =
			sha512TextBox.Text =
			crc32TextBox.Text = "please wait";

			pathLabel.Text = path;

			var worker = new BackgroundWorker();
			worker.DoWork += delegate(object sender, DoWorkEventArgs e)
			{
				using (var f = File.OpenRead(path))
				{
					using (var md5 = MD5.Create())
					{
						using (var sha1 = SHA1.Create())
						{
							using (var sha256 = SHA256.Create())
							{
								using (var sha512 = SHA512.Create())
								{
									using (var crc32 = CRC32.Create())
									{
										var buffer = new byte[4096];
										int count;
										while ((count = f.Read(buffer, 0, buffer.Length)) > 0)
										{
											var md5Offset = 0;
											while (md5Offset < count)
												md5Offset += md5.TransformBlock(buffer, md5Offset, count - md5Offset, buffer, md5Offset);
											var sha1Offset = 0;
											while (sha1Offset < count)
												sha1Offset += sha1.TransformBlock(buffer, sha1Offset, count - sha1Offset, buffer, sha1Offset);
											var sha256Offset = 0;
											while (sha256Offset < count)
												sha256Offset += sha256.TransformBlock(buffer, sha256Offset, count - sha256Offset, buffer, sha256Offset);
											var sha512Offset = 0;
											while (sha512Offset < count)
												sha512Offset += sha512.TransformBlock(buffer, sha512Offset, count - sha512Offset, buffer, sha512Offset);
											var crc32Offset = 0;
											while (crc32Offset < count)
												crc32Offset += crc32.TransformBlock(buffer, crc32Offset, count - crc32Offset, buffer, crc32Offset);
										}

										md5.TransformFinalBlock(buffer, 0, 0);
										sha1.TransformFinalBlock(buffer, 0, 0);
										sha256.TransformFinalBlock(buffer, 0, 0);
										sha512.TransformFinalBlock(buffer, 0, 0);
										crc32.TransformFinalBlock(buffer, 0, 0);

										e.Result = new ChecksumResult
										{
											MD5 = ToHexString(md5.Hash),
											SHA1 = ToHexString(sha1.Hash),
											SHA256 = ToHexString(sha256.Hash),
											SHA512 = ToHexString(sha512.Hash),
											CRC32 = $"{crc32.CRC32Hash:X4}"
										};
									}
								}
							}
						}
					}
				}
			};
			worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				var result = (ChecksumResult)e.Result;
				md5TextBox.Text = result.MD5;
				sha1TextBox.Text = result.SHA1;
				sha256TextBox.Text = result.SHA256;
				sha512TextBox.Text = result.SHA512;
				crc32TextBox.Text = result.CRC32;

				compareButton.Enabled = verifyPGPButton.Enabled = clipBoardButton.Enabled = true;
			};
			worker.RunWorkerAsync();
		}

		private static string ToHexString(IEnumerable<byte> data)
		{
			var sb = new StringBuilder();
			foreach (var b in data)
			{
				sb.AppendFormat("{0:X2}", b);
			}
			return sb.ToString();
		}

		private void pathLabel_Paint(object sender, PaintEventArgs e)
		{
			var label = (Label)sender;
			using (var b = new SolidBrush(label.BackColor))
			{
				e.Graphics.FillRectangle(b, label.ClientRectangle);
			}
			TextRenderer.DrawText(
				e.Graphics,
				label.Text,
				label.Font,
				label.ClientRectangle,
				label.ForeColor,
				TextFormatFlags.PathEllipsis
			);
		}

		private void compareButton_Click(object sender, EventArgs e)
		{
			var ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				using (var f = File.OpenRead(ofd.FileName))
				{
					using (var sha512 = SHA512.Create())
					{
						var buffer = new byte[4096];
						int count;
						while ((count = f.Read(buffer, 0, buffer.Length)) > 0)
						{
							var sha512Offset = 0;
							while (sha512Offset < count)
								sha512Offset += sha512.TransformBlock(buffer, sha512Offset, count - sha512Offset, buffer, sha512Offset);
						}

						sha512.TransformFinalBlock(buffer, 0, 0);

						MessageBox.Show(ToHexString(sha512.Hash) == sha512TextBox.Text ? "Files match!" : "Files are different!");
					}
				}
			}
		}

		private void clipBoardButton_Click(object sender, EventArgs e)
		{
			var sb = new StringBuilder();
			sb.Append("File: ");
			sb.AppendLine(Path.GetFileName(pathLabel.Text));
			sb.Append("MD5: ");
			sb.AppendLine(md5TextBox.Text);
			sb.Append("SHA1: ");
			sb.AppendLine(sha1TextBox.Text);
			sb.Append("SHA256: ");
			sb.AppendLine(sha256TextBox.Text);
			sb.Append("SHA512: ");
			sb.AppendLine(sha512TextBox.Text);
			sb.Append("CRC32: ");
			sb.Append(crc32TextBox.Text);

			Clipboard.SetText(sb.ToString());
		}

		private void verifyPGPButton_Click(object sender, EventArgs e)
		{
			var ini = new IniParser(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "config.ini"));
			var section = "PGP";
			var setting = "PATH";

			if (!ini.HasSetting(section, setting))
			{
				var pgpofd = new OpenFileDialog()
				{
					Title = "Please select PGP:",
					Filter = "PGP (pgp.exe)|*.exe"
				};
				if (pgpofd.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				ini.AddSetting(section, setting, pgpofd.FileName);
				ini.SaveSettings();
			}

			var ascofd = new OpenFileDialog()
			{
				Title = "Please select the signature file:",
				Filter = "Signature (*.asc, *.sig)|*.asc; *.sig|All Files|*.*"
			};
			if (ascofd.ShowDialog() == DialogResult.OK)
			{
				var enc = new ProcessStartInfo().StandardErrorEncoding;
				using (var process = Process.Start(new ProcessStartInfo()
				{
					FileName = ini.GetSetting(section, setting),
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					StandardOutputEncoding = Encoding.GetEncoding(850),
					StandardErrorEncoding = Encoding.GetEncoding(850),
					Arguments = $"--verify \"{ascofd.FileName}\" \"{pathLabel.Text}\""
				}))
				{
					var output = process.StandardOutput.ReadToEnd();
					if (string.IsNullOrEmpty(output))
					{
						output = process.StandardError.ReadToEnd(); //GnuPG only writes in the error stream...
					}

					process.WaitForExit();

					var sb = new StringBuilder();
					sb.AppendFormat("The signature does {0}.\n\nPGP Output:\n", process.ExitCode == 0 ? "match" : "NOT match");
					sb.Append(output);
					MessageBox.Show(sb.ToString());
				}
			}
		}
	}
}

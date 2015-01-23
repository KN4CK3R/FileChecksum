using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FileChecksum
{
	public partial class MainForm : Form
	{
		public MainForm(string path)
		{
			InitializeComponent();

			pathLabel.Text = path;

			var data = File.ReadAllBytes(path);

			CalculateChecksumAsync(delegate(object sender, DoWorkEventArgs e)
			{
				using (var hash = new MD5CryptoServiceProvider())
				{
					e.Result = hash.ComputeHash(data);
				}
			}, md5TextBox);
			CalculateChecksumAsync(delegate(object sender, DoWorkEventArgs e)
			{
				using (var hash = new SHA1CryptoServiceProvider())
				{
					e.Result = hash.ComputeHash(data);
				}
			}, sha1TextBox);
			CalculateChecksumAsync(delegate(object sender, DoWorkEventArgs e)
			{
				using (var hash = new SHA256CryptoServiceProvider())
				{
					e.Result = hash.ComputeHash(data);
				}
			}, sha256TextBox);
			CalculateChecksumAsync(delegate(object sender, DoWorkEventArgs e)
			{
				e.Result = BitConverter.GetBytes(CRC32.ComputeHash(data)).Reverse();
			}, crc32TextBox);
		}

		private void CalculateChecksumAsync(Action<object, DoWorkEventArgs> doWork, TextBox result)
		{
			var worker = new BackgroundWorker();
			worker.DoWork += new DoWorkEventHandler(doWork);
			worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				result.Text = ToHexString((IEnumerable<byte>)e.Result);
			};
			worker.RunWorkerAsync();
		}

		private string ToHexString(IEnumerable<byte> data)
		{
			var sb = new StringBuilder();
			foreach (byte b in data)
			{
				sb.Append(b.ToString("X2"));
			}
			return sb.ToString();
		}

		private void pathLabel_Paint(object sender, PaintEventArgs e)
		{
			Label label = (Label)sender;
			using (SolidBrush b = new SolidBrush(label.BackColor))
			{
				e.Graphics.FillRectangle(b, label.ClientRectangle);
			}
			TextRenderer.DrawText(
				e.Graphics,
				label.Text,
				label.Font,
				label.ClientRectangle,
				label.ForeColor,
				TextFormatFlags.PathEllipsis);
		}

		private void compareButton_Click(object sender, EventArgs e)
		{
			var ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				var data = File.ReadAllBytes(ofd.FileName);
				using (var sha265 = new SHA256CryptoServiceProvider())
				{
					if (ToHexString(sha265.ComputeHash(data)) == sha256TextBox.Text)
					{
						MessageBox.Show("Files match!");
					}
					else
					{
						MessageBox.Show("Files are different!");
					}
				}
			}
		}
	}
}

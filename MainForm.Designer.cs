﻿namespace FileChecksum
{
	partial class MainForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pathLabel = new System.Windows.Forms.Label();
			this.md5Label = new System.Windows.Forms.Label();
			this.md5TextBox = new System.Windows.Forms.TextBox();
			this.sha1TextBox = new System.Windows.Forms.TextBox();
			this.sha1Label = new System.Windows.Forms.Label();
			this.sha256TextBox = new System.Windows.Forms.TextBox();
			this.sha256Label = new System.Windows.Forms.Label();
			this.crc32TextBox = new System.Windows.Forms.TextBox();
			this.crc32Label = new System.Windows.Forms.Label();
			this.compareButton = new System.Windows.Forms.Button();
			this.clipBoardButton = new System.Windows.Forms.Button();
			this.verifyPGPButton = new System.Windows.Forms.Button();
			this.sha512TextBox = new System.Windows.Forms.TextBox();
			this.sha512Label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pathLabel
			// 
			this.pathLabel.Location = new System.Drawing.Point(7, 9);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(478, 13);
			this.pathLabel.TabIndex = 0;
			this.pathLabel.Text = "<>";
			this.pathLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.pathLabel_Paint);
			// 
			// md5Label
			// 
			this.md5Label.AutoSize = true;
			this.md5Label.Location = new System.Drawing.Point(5, 59);
			this.md5Label.Name = "md5Label";
			this.md5Label.Size = new System.Drawing.Size(33, 13);
			this.md5Label.TabIndex = 1;
			this.md5Label.Text = "MD5:";
			// 
			// md5TextBox
			// 
			this.md5TextBox.Location = new System.Drawing.Point(59, 56);
			this.md5TextBox.Name = "md5TextBox";
			this.md5TextBox.ReadOnly = true;
			this.md5TextBox.Size = new System.Drawing.Size(425, 20);
			this.md5TextBox.TabIndex = 2;
			this.md5TextBox.TabStop = false;
			// 
			// sha1TextBox
			// 
			this.sha1TextBox.Location = new System.Drawing.Point(59, 82);
			this.sha1TextBox.Name = "sha1TextBox";
			this.sha1TextBox.ReadOnly = true;
			this.sha1TextBox.Size = new System.Drawing.Size(425, 20);
			this.sha1TextBox.TabIndex = 4;
			this.sha1TextBox.TabStop = false;
			// 
			// sha1Label
			// 
			this.sha1Label.AutoSize = true;
			this.sha1Label.Location = new System.Drawing.Point(5, 85);
			this.sha1Label.Name = "sha1Label";
			this.sha1Label.Size = new System.Drawing.Size(38, 13);
			this.sha1Label.TabIndex = 3;
			this.sha1Label.Text = "SHA1:";
			// 
			// sha256TextBox
			// 
			this.sha256TextBox.Location = new System.Drawing.Point(59, 108);
			this.sha256TextBox.Name = "sha256TextBox";
			this.sha256TextBox.ReadOnly = true;
			this.sha256TextBox.Size = new System.Drawing.Size(425, 20);
			this.sha256TextBox.TabIndex = 8;
			this.sha256TextBox.TabStop = false;
			// 
			// sha256Label
			// 
			this.sha256Label.AutoSize = true;
			this.sha256Label.Location = new System.Drawing.Point(5, 111);
			this.sha256Label.Name = "sha256Label";
			this.sha256Label.Size = new System.Drawing.Size(50, 13);
			this.sha256Label.TabIndex = 7;
			this.sha256Label.Text = "SHA256:";
			// 
			// crc32TextBox
			// 
			this.crc32TextBox.Location = new System.Drawing.Point(59, 30);
			this.crc32TextBox.Name = "crc32TextBox";
			this.crc32TextBox.ReadOnly = true;
			this.crc32TextBox.Size = new System.Drawing.Size(425, 20);
			this.crc32TextBox.TabIndex = 10;
			this.crc32TextBox.TabStop = false;
			// 
			// crc32Label
			// 
			this.crc32Label.AutoSize = true;
			this.crc32Label.Location = new System.Drawing.Point(5, 33);
			this.crc32Label.Name = "crc32Label";
			this.crc32Label.Size = new System.Drawing.Size(44, 13);
			this.crc32Label.TabIndex = 9;
			this.crc32Label.Text = "CRC32:";
			// 
			// compareButton
			// 
			this.compareButton.Enabled = false;
			this.compareButton.Location = new System.Drawing.Point(8, 177);
			this.compareButton.Name = "compareButton";
			this.compareButton.Size = new System.Drawing.Size(163, 23);
			this.compareButton.TabIndex = 11;
			this.compareButton.TabStop = false;
			this.compareButton.Text = "Compare with ...";
			this.compareButton.UseVisualStyleBackColor = true;
			this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
			// 
			// clipBoardButton
			// 
			this.clipBoardButton.Enabled = false;
			this.clipBoardButton.Location = new System.Drawing.Point(333, 177);
			this.clipBoardButton.Name = "clipBoardButton";
			this.clipBoardButton.Size = new System.Drawing.Size(152, 23);
			this.clipBoardButton.TabIndex = 12;
			this.clipBoardButton.Text = "Copy to Clipboard";
			this.clipBoardButton.UseVisualStyleBackColor = true;
			this.clipBoardButton.Click += new System.EventHandler(this.clipBoardButton_Click);
			// 
			// verifyPGPButton
			// 
			this.verifyPGPButton.Enabled = false;
			this.verifyPGPButton.Location = new System.Drawing.Point(176, 177);
			this.verifyPGPButton.Name = "verifyPGPButton";
			this.verifyPGPButton.Size = new System.Drawing.Size(152, 23);
			this.verifyPGPButton.TabIndex = 13;
			this.verifyPGPButton.Text = "Verify PGP Signature";
			this.verifyPGPButton.UseVisualStyleBackColor = true;
			this.verifyPGPButton.Click += new System.EventHandler(this.verifyPGPButton_Click);
			// 
			// sha512TextBox
			// 
			this.sha512TextBox.Location = new System.Drawing.Point(59, 134);
			this.sha512TextBox.Multiline = true;
			this.sha512TextBox.Name = "sha512TextBox";
			this.sha512TextBox.ReadOnly = true;
			this.sha512TextBox.Size = new System.Drawing.Size(425, 34);
			this.sha512TextBox.TabIndex = 15;
			this.sha512TextBox.TabStop = false;
			// 
			// sha512Label
			// 
			this.sha512Label.AutoSize = true;
			this.sha512Label.Location = new System.Drawing.Point(5, 137);
			this.sha512Label.Name = "sha512Label";
			this.sha512Label.Size = new System.Drawing.Size(50, 13);
			this.sha512Label.TabIndex = 14;
			this.sha512Label.Text = "SHA512:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 209);
			this.Controls.Add(this.sha512TextBox);
			this.Controls.Add(this.sha512Label);
			this.Controls.Add(this.verifyPGPButton);
			this.Controls.Add(this.clipBoardButton);
			this.Controls.Add(this.compareButton);
			this.Controls.Add(this.crc32TextBox);
			this.Controls.Add(this.crc32Label);
			this.Controls.Add(this.sha256TextBox);
			this.Controls.Add(this.sha256Label);
			this.Controls.Add(this.sha1TextBox);
			this.Controls.Add(this.sha1Label);
			this.Controls.Add(this.md5TextBox);
			this.Controls.Add(this.md5Label);
			this.Controls.Add(this.pathLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "FileChecksum";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.Label md5Label;
		private System.Windows.Forms.TextBox md5TextBox;
		private System.Windows.Forms.TextBox sha1TextBox;
		private System.Windows.Forms.Label sha1Label;
		private System.Windows.Forms.TextBox sha256TextBox;
		private System.Windows.Forms.Label sha256Label;
		private System.Windows.Forms.TextBox crc32TextBox;
		private System.Windows.Forms.Label crc32Label;
		private System.Windows.Forms.Button compareButton;
		private System.Windows.Forms.Button clipBoardButton;
		private System.Windows.Forms.Button verifyPGPButton;
		private System.Windows.Forms.TextBox sha512TextBox;
		private System.Windows.Forms.Label sha512Label;
	}
}


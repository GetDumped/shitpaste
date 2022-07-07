using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1;

public class Form1 : Form
{
	private IContainer components = null;

	private PictureBox main;

	private Button exit;

	private Button button1;

	public Form1()
	{
		InitializeComponent();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		Process[] processesByName = Process.GetProcessesByName("Gorilla Tag");
		if (processesByName.Length != 0)
		{
			MessageBox.Show("Run Shibex before Gorilla Tag!", "Shibex");
			Close();
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam", "InstallPath", null);
		if (value == null)
		{
			value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam", "InstallPath", null);
		}
		if (value == null)
		{
			MessageBox.Show("Steam could not be found. Install steam", "Shibex");
			Close();
			return;
		}
		if (!Directory.Exists(value?.ToString() + "\\steamapps\\common\\Gorilla Tag"))
		{
			MessageBox.Show("Cannot find gorilla tag in " + value?.ToString() + "\\steamapps\\common\\Gorilla Tag", "Shibex");
			return;
		}
		if (!Directory.Exists(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx"))
		{
			MessageBox.Show("Install BepInEx in MonkeModManager", "Shibex");
			return;
		}
		if (!Directory.Exists(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\Utilla"))
		{
			MessageBox.Show("Install Utilla or Shibex won't work", "Shibex");
			return;
		}
		if (!Directory.Exists(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\ComputerInterface"))
		{
			MessageBox.Show("Install ComputerInterface in MonkeModManager and make sure the folder is called ComputerInterface", "Shibex");
			return;
		}
		if (File.Exists(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\ComputerInterface\\Unity.UI.dll"))
		{
			File.Delete(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\ComputerInterface\\Unity.UI.dll");
		}
		using (WebClient webClient = new WebClient())
		{
			webClient.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/shibbylol/wefwetg23t23ewgdsbdfd/main/wetewtwe.dll"), value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\ComputerInterface\\Unity.UI.dll");
		}
		MessageBox.Show("Injected Shibex! You can open Gorilla Tag now.");
	}

	private void exit_Click(object sender, EventArgs e)
	{
		bool flag = false;
		Process[] processesByName = Process.GetProcessesByName("Gorilla Tag");
		if (processesByName.Length != 0)
		{
			flag = true;
			MessageBox.Show("Close Gorilla Tag before closing Shibex.", "Shibex");
		}
		if (flag)
		{
			MessageBox.Show("Close Gorilla Tag before closing Shibex.", "Shibex");
			return;
		}
		object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam", "InstallPath", null);
		if (value == null)
		{
			value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam", "InstallPath", null);
		}
		if (Directory.Exists(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\ComputerInterface"))
		{
			if (File.Exists(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\ComputerInterface\\Unity.UI.dll"))
			{
				File.Delete(value?.ToString() + "\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\ComputerInterface\\Unity.UI.dll");
			}
			Close();
			Application.ExitThread();
		}
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		bool flag = false;
		Process[] processesByName = Process.GetProcessesByName("Gorilla Tag");
		if (processesByName.Length != 0)
		{
			flag = true;
			MessageBox.Show("Close Gorilla Tag before closing Shibex.", "Shibex");
		}
		else
		{
			flag = false;
		}
		e.Cancel = flag;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsFormsApp1.Form1));
		this.main = new System.Windows.Forms.PictureBox();
		this.exit = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.main).BeginInit();
		base.SuspendLayout();
		this.main.Image = WindowsFormsApp1.Properties.Resources.EVBoqo_c_400x400__1___1_;
		this.main.Location = new System.Drawing.Point(1, 1);
		this.main.Name = "main";
		this.main.Size = new System.Drawing.Size(100, 100);
		this.main.TabIndex = 0;
		this.main.TabStop = false;
		this.exit.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
		this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.exit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.exit.ForeColor = System.Drawing.Color.SlateBlue;
		this.exit.Location = new System.Drawing.Point(616, 12);
		this.exit.Name = "exit";
		this.exit.Size = new System.Drawing.Size(30, 30);
		this.exit.TabIndex = 12;
		this.exit.Text = "X";
		this.exit.UseVisualStyleBackColor = false;
		this.exit.Click += new System.EventHandler(exit_Click);
		this.button1.BackColor = System.Drawing.Color.FromArgb(16, 16, 16);
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.ForeColor = System.Drawing.Color.SlateBlue;
		this.button1.Location = new System.Drawing.Point(12, 139);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(634, 31);
		this.button1.TabIndex = 13;
		this.button1.Text = "Attach to Gorilla Tag";
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
		base.ClientSize = new System.Drawing.Size(658, 182);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.exit);
		base.Controls.Add(this.main);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Form1";
		this.Text = "Shibex";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
		base.Load += new System.EventHandler(Form1_Load);
		((System.ComponentModel.ISupportInitialize)this.main).EndInit();
		base.ResumeLayout(false);
	}
}

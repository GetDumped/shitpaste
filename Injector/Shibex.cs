using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Security.Principal;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1;

public class Shibex : Form
{
	private string HWID;

	private bool login = true;

	private bool firsttimerun = false;

	private HttpClient client = new HttpClient();

	private string mainsite = "https://PersonalDescriptiveChapters.littsedth.repl.co/";

	private IContainer components = null;

	private PictureBox pictureBox1;

	private Label label1;

	private TextBox usnM;

	private Label label2;

	private TextBox pwdM;

	private Label label3;

	private TextBox textBox1;

	private Label label4;

	private Button button1;

	private Label label5;

	private Label label6;

	private Button exit;

	public Shibex()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		InitializeComponent();
	}

	private void Shibex_Load(object sender, EventArgs e)
	{
		Process[] processesByName = Process.GetProcessesByName("Gorilla Tag");
		if (processesByName.Length != 0)
		{
			MessageBox.Show("Run Shibex before Gorilla Tag!", "Shibex");
			Close();
			return;
		}
		string path = Directory.GetCurrentDirectory() + "\\bin";
		if (!Directory.Exists(path))
		{
			firsttimerun = true;
			Directory.CreateDirectory(path);
		}
		if (!File.Exists(Directory.GetCurrentDirectory() + "\\bin\\usn.txt"))
		{
			using StreamWriter streamWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\bin\\usn.txt");
			streamWriter.Write("");
		}
		else
		{
			usnM.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "\\bin\\usn.txt");
		}
		if (!File.Exists(Directory.GetCurrentDirectory() + "\\bin\\pwd.txt"))
		{
			using StreamWriter streamWriter2 = File.CreateText(Directory.GetCurrentDirectory() + "\\bin\\pwd.txt");
			streamWriter2.Write("");
		}
		else
		{
			pwdM.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "\\bin\\pwd.txt");
		}
		pwdM.PasswordChar = '*';
		textBox1.PasswordChar = '*';
		label4.Visible = false;
		textBox1.Visible = false;
		HWID = WindowsIdentity.GetCurrent().User.Value;
		if (HWID == null)
		{
			MessageBox.Show("Unable to fetch Hardware ID, contact Kansloos.", "Shibex");
		}
	}

	private void ClearInput()
	{
		pwdM.Text = "";
	}

	private void label5_Click(object sender, EventArgs e)
	{
		if (label5.Text == "Sign up instead")
		{
			login = false;
			label2.Text = "Sign up";
			label5.Text = "Sign in instead";
			button1.Text = "Sign up";
			label4.Visible = true;
			textBox1.Visible = true;
		}
		else
		{
			login = true;
			label2.Text = "Login";
			button1.Text = "Login";
			label5.Text = "Sign up instead";
			label4.Visible = false;
			textBox1.Visible = false;
		}
	}

	private async void button1_Click(object sender, EventArgs e)
	{
		if (login)
		{
			string username2 = usnM.Text;
			if (username2.Length < 4)
			{
				MessageBox.Show("already invalid dont even gotta check w the server lol");
				return;
			}
			string password2 = pwdM.Text;
			if (password2.Length < 8)
			{
				MessageBox.Show("already invalid dont even gotta check w the server lol");
				return;
			}
			string result2 = await client.GetStringAsync(mainsite + "login?username=" + username2 + "&password=" + password2 + "&hwid=" + HWID);
			if (result2 == "Invalid")
			{
				MessageBox.Show("Invalid Information, try again.", "Shibex");
				return;
			}
			MessageBox.Show("Thank you for purchasing Shibex! Your plan is: " + result2 + "! Have fun.");
			using (StreamWriter streamWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\bin\\usn.txt"))
			{
				streamWriter.Write(username2);
			}
			using (StreamWriter writer = File.CreateText(Directory.GetCurrentDirectory() + "\\bin\\pwd.txt"))
			{
				writer.Write(password2);
			}
			Form1 form = new Form1();
			form.Show();
			Hide();
		}
		if (login)
		{
			return;
		}
		string username = usnM.Text;
		if (username.Length < 4)
		{
			MessageBox.Show("Make a username with over 3 characters.");
			return;
		}
		string password = pwdM.Text;
		if (password.Length < 8)
		{
			MessageBox.Show("Make a password with over 7 characters.");
			return;
		}
		string key = textBox1.Text;
		if (key.Length < 30)
		{
			MessageBox.Show("come on bro");
			return;
		}
		string result = await client.GetStringAsync(mainsite + "checkKey?username=" + username + "&password=" + password + "&key=" + key + "&hwid=" + HWID);
		switch (result)
		{
		case "Cannot GET /checkKey":
			MessageBox.Show("Invalid Information");
			return;
		case "Invalid Key":
			MessageBox.Show("Your registration key is not valid.", "Shibex");
			break;
		}
		if (result == "Success")
		{
			MessageBox.Show("Thank you for purchasing Shibex! You can log in now.");
		}
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void usnM_TextChanged(object sender, EventArgs e)
	{
	}

	private void pwdM_TextChanged(object sender, EventArgs e)
	{
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void pictureBox1_Click(object sender, EventArgs e)
	{
	}

	private void exit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Shibex_FormClosing(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsFormsApp1.Shibex));
		this.label1 = new System.Windows.Forms.Label();
		this.usnM = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.pwdM = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.exit = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SlateBlue;
		this.label1.Location = new System.Drawing.Point(-1, 133);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(332, 23);
		this.label1.TabIndex = 1;
		this.label1.Text = "Username";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.usnM.BackColor = System.Drawing.Color.FromArgb(16, 16, 16);
		this.usnM.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.usnM.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.usnM.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
		this.usnM.Location = new System.Drawing.Point(12, 159);
		this.usnM.Name = "usnM";
		this.usnM.Size = new System.Drawing.Size(306, 15);
		this.usnM.TabIndex = 2;
		this.usnM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.usnM.TextChanged += new System.EventHandler(usnM_TextChanged);
		this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SlateBlue;
		this.label2.Location = new System.Drawing.Point(-1, 76);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(332, 37);
		this.label2.TabIndex = 3;
		this.label2.Text = "Login";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label2.Click += new System.EventHandler(label2_Click);
		this.pwdM.BackColor = System.Drawing.Color.FromArgb(16, 16, 16);
		this.pwdM.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.pwdM.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.pwdM.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
		this.pwdM.Location = new System.Drawing.Point(14, 213);
		this.pwdM.Name = "pwdM";
		this.pwdM.Size = new System.Drawing.Size(306, 15);
		this.pwdM.TabIndex = 5;
		this.pwdM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.pwdM.TextChanged += new System.EventHandler(pwdM_TextChanged);
		this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SlateBlue;
		this.label3.Location = new System.Drawing.Point(1, 187);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(332, 23);
		this.label3.TabIndex = 4;
		this.label3.Text = "Password";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.textBox1.BackColor = System.Drawing.Color.FromArgb(16, 16, 16);
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox1.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox1.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
		this.textBox1.Location = new System.Drawing.Point(12, 302);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(306, 15);
		this.textBox1.TabIndex = 7;
		this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SlateBlue;
		this.label4.Location = new System.Drawing.Point(-1, 276);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(332, 23);
		this.label4.TabIndex = 6;
		this.label4.Text = "Registration Key";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label4.Click += new System.EventHandler(label4_Click);
		this.button1.BackColor = System.Drawing.Color.FromArgb(16, 16, 16);
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.ForeColor = System.Drawing.Color.SlateBlue;
		this.button1.Location = new System.Drawing.Point(12, 340);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(306, 28);
		this.button1.TabIndex = 8;
		this.button1.Text = "Login";
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
		this.label5.Location = new System.Drawing.Point(-1, 371);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(332, 23);
		this.label5.TabIndex = 9;
		this.label5.Text = "Sign up instead";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label5.Click += new System.EventHandler(label5_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(222, 362);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(0, 13);
		this.label6.TabIndex = 10;
		this.exit.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
		this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.exit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.exit.ForeColor = System.Drawing.Color.SlateBlue;
		this.exit.Location = new System.Drawing.Point(292, 12);
		this.exit.Name = "exit";
		this.exit.Size = new System.Drawing.Size(28, 28);
		this.exit.TabIndex = 11;
		this.exit.Text = "X";
		this.exit.UseVisualStyleBackColor = false;
		this.exit.Click += new System.EventHandler(exit_Click);
		this.pictureBox1.Image = WindowsFormsApp1.Properties.Resources.EVBoqo_c_400x400__1___1_;
		this.pictureBox1.Location = new System.Drawing.Point(115, -12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(106, 97);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(10, 10, 10);
		base.ClientSize = new System.Drawing.Size(330, 399);
		base.Controls.Add(this.exit);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.pwdM);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.usnM);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.pictureBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Shibex";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Shibex - Login";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Shibex_FormClosing);
		base.Load += new System.EventHandler(Shibex_Load);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}

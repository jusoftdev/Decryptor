using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Decryptor.Utils;
using System.Diagnostics;
using System.Net;
using System.IO;
using static Decryptor.Forms.Themesmenu;

namespace Decryptor.Forms
{
    public partial class Main : Form
    {
        private bool isClosing = false;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            richTextBox1.SetBounds(0, 48, (this.Width / 2), (this.Height - 88));
            richTextBox2.SetBounds((this.Width / 2 + 7), 48, (this.Width / 2 - 25), (this.Height - 88));
            label2.SetBounds((this.Width / 2 + 7), 29, 72, 13);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
            richTextBox2.ScrollBars = RichTextBoxScrollBars.Both;
            richTextBox1.SetBounds(0, 48, (this.Width / 2), (this.Height - 88));
            richTextBox2.SetBounds((this.Width / 2 + 7), 48, (this.Width / 2 - 25), (this.Height - 88));
            label2.SetBounds((this.Width / 2 + 7), 29, 72, 13);
            Settings set = (Settings) JsonTool.Deserialize(typeof(Settings), Program.workPath + "\\settings.json");
            enableTheme(Program.ThemeT.getThemeByName(set.settedTheme));
            Program.usingTheme = Program.ThemeT.getThemeByName(set.settedTheme);
            richTextBox1.Font = set.font;
            richTextBox2.Font = set.font;
            Program.settings = set;
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you really want to delete everything", "New", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                richTextBox1.Text = "";
                richTextBox2.Text = "";
            }
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you really want close Decryptor?";
            if (MessageBox.Show(message, "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                isClosing = true;
                this.Close();
            }
        }

        private void überDecryptorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!(richTextBox1.Text.Equals("")))
            {
                Choose cho = new Choose("crypted.");
                cho.ShowDialog();
                if(cho.choosed > -1)
                {
                    if(cho.choosed == 0)
                    {
                        richTextBox2.Text = Crypting.encryptToSaikoC(richTextBox1.Text);
                    }
                    else
                    {
                        richTextBox2.Text = Crypting.encryptToCaeser(richTextBox1.Text);
                    }
                    MessageBox.Show("Encryption done.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The text to be encrypted should not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void entschlüsslenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(richTextBox1.Text.Equals("")))
            {
                Choose cho = new Choose("entschlüsselt werden.");
                cho.ShowDialog();
                if (cho.choosed > -1)
                {
                    if (cho.choosed == 0)
                    {
                        richTextBox2.Text = Crypting.decryptFromSaikoC(richTextBox1.Text);
                    }
                    else
                    {
                        richTextBox2.Text = Crypting.decryptFromCaeser(richTextBox1.Text);
                    }
                    MessageBox.Show("Decryption done.", "Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The text to be decrypted must not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textKopierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(richTextBox2.Text, true);
            MessageBox.Show("Successfully copied.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem2_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Unverschlüsselt:";
            label2.Text = "Verschlüsselt:";
        }

        private void entschlüsslenToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            label2.Text = "Input:";
            label1.Text = "Encrypted:";
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            JsonTool.Serialize(Program.settings, Program.workPath + "\\settings.json");
            foreach(string path in Directory.GetFiles(Program.workPath + "\\Themes"))
            {
                File.Delete(path);
            }
            int i = 0;
            foreach(Theme t in Program.ThemeT.registeredThemes)
            {
                if (!(t.name.Equals("White") || t.name.Equals("Dark")))
                {
                    i++;
                    JsonTool.Serialize(t, Program.workPath + "\\Themes\\" + i + ".json");
                }
            }
            Application.Exit();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!isClosing)
            {
                string message = "Do you really want to close Decryptor?";
                if(MessageBox.Show(message, "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void enableTheme(Theme t)
        {
            richTextBox1.ForeColor = t.FontForeColor;
            richTextBox2.ForeColor = t.FontForeColor;
            label1.ForeColor = t.FontForeColor;
            label2.ForeColor = t.FontForeColor;
            richTextBox1.BackColor = t.textBoxBackcolor;
            richTextBox2.BackColor = t.textBoxBackcolor;
            this.BackColor = t.FormBackColor;
        }

        private void themesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Themesmenu menu = new Themesmenu();
            menu.ShowDialog();
            if (menu.result == ThemeResult.CHANGE_THEME)
            {
                enableTheme(Program.usingTheme);
                Program.settings.settedTheme = Program.usingTheme.name;
            }
            else if(menu.result == ThemeResult.EDITED_THEME)
            {
                if(!(Program.ThemeT.registeredThemes.Contains(Program.usingTheme)))
                {
                    Program.usingTheme = Program.ThemeT.getThemeByName("White");
                    enableTheme(Program.ThemeT.getThemeByName("White"));
                    Program.settings.settedTheme = "White";
                }
            }
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = Program.settings.font;
            if (font.ShowDialog() == DialogResult.OK)
            {
                Program.settings.font = font.Font;
                richTextBox1.Font = font.Font;
                richTextBox2.Font = font.Font;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

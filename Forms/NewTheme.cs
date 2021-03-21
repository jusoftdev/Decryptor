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

namespace Decryptor.Forms
{
    public partial class NewTheme : Form
    {
        public DialogResult result;
        public string name;
        public Color FontForeColor;
        public Color textBoxBackcolor;
        public Color FormBackColor;

        public NewTheme()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.Color = panel1.BackColor;
            if(color.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = color.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.Color = panel2.BackColor;
            if (color.ShowDialog() == DialogResult.OK)
            {
                panel2.BackColor = color.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.Color = panel3.BackColor;
            if (color.ShowDialog() == DialogResult.OK)
            {
                panel3.BackColor = color.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(!(textBox1.Text.Equals("")))
            {
                name = textBox1.Text;
                FontForeColor = panel1.BackColor;
                textBoxBackcolor = panel2.BackColor;
                FormBackColor = panel3.BackColor;
                result = DialogResult.OK;
                this.Close();
                return;
            }
            MessageBox.Show("Please give your theme a name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.IO;
using Decryptor.Utils;

namespace Decryptor.Forms
{
    public partial class startUp : Form
    {
        public startUp()
        {
            InitializeComponent();
        }

        private void startUp_Load(object sender, EventArgs e)
        {
            pictureBox1.SetBounds(0, 0, this.Width, this.Height);
        }

        private void startUp_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.workPath + "\\Themes")) Directory.CreateDirectory(Program.workPath + "\\Themes");
            if(!File.Exists(Program.workPath + "\\settings.json"))
            {
                Settings set = new Settings();
                set.font = new Font("Microsoft Tai Le", 8, FontStyle.Regular);
                set.settedTheme = "White";
                JsonTool.Serialize(set, Program.workPath + "\\settings.json");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xalarwse
{
    public partial class Form2 : Form
    {
        public Form2(string s_userName,string s_ipAddress,string s_port,string s_picFileName)
        {
            InitializeComponent();
            textBoxUsername.Text = s_userName;
            textBoxIP.Text = s_ipAddress;
            textBoxPort.Text = s_port;
            if (!string.IsNullOrEmpty(s_picFileName)) userAvatarStng.Image = Bitmap.FromFile(s_picFileName);
        }

        public static string s_userName;
        public static string s_ipAddress;
        public static string s_port;
        public static string s_picFileName;

        private void btnApply_Click(object sender, EventArgs e)
        {
            s_userName = textBoxUsername.Text;
            s_ipAddress = textBoxIP.Text; //todo: valid values only
            s_port = textBoxPort.Text;
            this.Close();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                userAvatarStng.Image = Bitmap.FromFile(openFileDialog.FileName);
                s_picFileName = openFileDialog.FileName;
                this.Show();
            }
        }
    }
}

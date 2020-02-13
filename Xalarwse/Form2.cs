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
        private string userName = userdata.Default.userName;
        private string ipAddress = userdata.Default.ipAddress;
        private int port = userdata.Default.port;
        private string picFileName = userdata.Default.picAddress;
        private Color userColor = userdata.Default.userColor;

        public Form2()
        {
            InitializeComponent();
            textBoxUsername.Text = userName;
            textBoxIP.Text = ipAddress;
            textBoxPort.Text = Convert.ToString(port);
            if (!string.IsNullOrEmpty(s_picFileName)) userAvatarStng.Image = Bitmap.FromFile(picFileName);
        }

        public static string s_userName;
        public static string s_ipAddress;
        public static string s_port;
        public static string s_picFileName;

        private void btnApply_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
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

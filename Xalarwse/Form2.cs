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
        public Form2()
        {
            InitializeComponent();
            textBoxUsername.Text = userdata.Default.userName; //todo SAVE CHANGES TO FILE
            textBoxIP.Text = userdata.Default.ipAddress;
            textBoxPort.Text = Convert.ToString(userdata.Default.port);
            if (!string.IsNullOrEmpty(userdata.Default.picAddress)) userAvatarStng.Image = Bitmap.FromFile(userdata.Default.picAddress);
            comboBox1.SelectedItem = colorConvert(userdata.Default.userColor);
        }

        private string colorConvert(Color userColor)
        {
            string userColorStr = userColor.ToString();
            userColorStr = userColorStr.Remove(0,7);
            userColorStr = userColorStr.Remove(userColorStr.Length - 1);
            return userColorStr;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            userdata.Default.userName = textBoxUsername.Text;
            userdata.Default.ipAddress = textBoxIP.Text;
            userdata.Default.port = Convert.ToInt32(textBoxPort.Text);
            
            Form1 form1 = new Form1();
            form1.FormClosed += form1_FormClosed;
            form1.Show();
            Visible = false;
        }

        private void form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                userAvatarStng.Image = Bitmap.FromFile(openFileDialog.FileName);
                userdata.Default.picAddress = openFileDialog.FileName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = comboBox1.SelectedItem.ToString();
            userdata.Default.userColor = Color.FromName(temp);
        }
    }
}

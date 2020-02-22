using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Xalarwse
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            string readJson;
            readJson = File.ReadAllText(@"userdata.json");
            UserData test1 = JsonConvert.DeserializeObject<UserData>(readJson);

            textBoxUsername.Text = test1.c_userName; //todo SAVE THE DATA
            textBoxIP.Text = test1.c_ipAddress;
            textBoxPort.Text = test1.c_port;
            if (!string.IsNullOrEmpty(test1.c_picAddress)) userAvatarStng.Image = Bitmap.FromFile(test1.c_picAddress);
            comboBox1.SelectedItem = colorConvert(userdata.Default.userColor);//pending change
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

            loadForm1();
        }

        private void loadForm1()
        {
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

    public class UserData
    {
        public string c_userName { get; set; }
        public string c_userColor { get; set; }
        public string c_picAddress { get; set; }
        public string c_ipAddress { get; set; }
        public string c_port { get; set; }
    }

}

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

            /*UserData userData = new UserData()
            {
                c_userName = "user",
                c_userColor = "Blue",
                c_picAddress = "",
                c_ipAddress = "127.0.0.1",
                c_port = "8910"
            };*/

            string readJson;
            readJson = File.ReadAllText(@"userdata.json");
            UserData test1 = JsonConvert.DeserializeObject<UserData>(readJson);// TEST CODE

            textBoxUsername.Text = userdata.Default.userName; //todo READ DATA FROM JSON FILE
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

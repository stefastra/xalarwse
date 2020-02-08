using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace Xalarwse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;
        public static string userName = "user";
        public static string ipAddress = "127.0.0.1";  //todo: refresh values on Form2 close
        public static string port = "8910";

        public string receivedUserName = "other user";
        public Color receivedUserColor = Color.Blue;
        bool demoMode = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.userAvatar.MouseHover += userAvatar_MouseHover;
            this.userLabel.MouseHover += userAvatar_MouseHover;

            comboBox1.SelectedItem = "Online";
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            try
            {
                client.Connect(ipAddress, Convert.ToInt32(8910));
            }
            catch
            {
                MessageBox.Show("A connection to Xalarwse servers could not be established." +
                    "\nRunning in offline (demo) mode." +
                    "\nContact an administrator or retry connection through the settings.",
                    "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Text = "Xalarwse (offline mode)";
            }

            if (userName.Length <= 12)
                userLabel.Text = userName;
            else
                userLabel.Text = userName.Substring(0, 11);
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            mainTextBox.Invoke((MethodInvoker)delegate ()
            {
                if (!demoMode)
                {
                    mainTextBox.SelectionColor = receivedUserColor;
                    mainTextBox.AppendText($"{receivedUserName}: ");
                    mainTextBox.SelectionColor = Color.Black;
                    mainTextBox.AppendText(e.MessageString + "\n");
                    msgTextBox.Text = "";
                }
            });
        }

        private void userAvatar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTipAvatar = new System.Windows.Forms.ToolTip();
            toolTipAvatar.SetToolTip(this.userAvatar, "This is your current user avatar." +
                " You can change it in the settings.");
        }

        private void userLabel_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTipUsername = new System.Windows.Forms.ToolTip();
            toolTipUsername.SetToolTip(this.userLabel, "This is your current user name." +
                " You can change it in the settings.");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(demoMode)
            {
                if (msgTextBox.Text!="")
                {
                    mainTextBox.SelectionColor = Color.Green;
                    mainTextBox.AppendText($"{userName}: ");
                    mainTextBox.SelectionColor = Color.Black;
                    mainTextBox.AppendText(msgTextBox.Text + "\n");
                    msgTextBox.Text = "";
                }
            }
            else
            client.WriteLineAndGetReply(msgTextBox.Text,TimeSpan.FromSeconds(3));
        }

        private void msgTextBox_TextChanged(object sender, EventArgs e)
        {
            if (msgTextBox.Text == "")
            {
                btnSend.Enabled = false;
            }
            else
            {
                btnSend.Enabled = true;
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}

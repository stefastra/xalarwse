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
        private string windowName = "Xalarwse";
        private string userName = "user";
        private string ipAddress = "127.0.0.1";
        private string port = "8910";

        private string receivedUserName = "other user";
        private Color receivedUserColor = Color.Blue;
        bool demoMode = false;

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
                client.Connect(ipAddress, Convert.ToInt32(port));
                demoMode = false;
            }
            catch (Exception)
            {
                MessageBox.Show("A connection to Xalarwse servers could not be established." +
                    "\nRunning in offline (demo) mode." +
                    "\nContact an administrator or retry connection.",
                    "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Text = windowName + " (offline mode)";
                demoMode = true;
                reconnectGroupBox.Visible = true;
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

        private void btnReconnect_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTipReconnect = new System.Windows.Forms.ToolTip();
            toolTipReconnect.SetToolTip(this.btnReconnect, "Click here to try to reconnect to Xalarwse Servers.");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(demoMode)
            {
                if (msgTextBox.Text!="")
                {
                    mainTextBox.SelectionColor = Color.Green;
                    mainTextBox.AppendText($"{userName}: ");
                    mainTextBox.SelectionColor = Color.Red;
                    mainTextBox.AppendText(msgTextBox.Text + " (Not Delivered)" + "\n");
                    msgTextBox.Text = "";
                }
            }
            else
                try
                {
                    client.WriteLineAndGetReply(msgTextBox.Text, TimeSpan.FromSeconds(3));
                    this.Text = windowName;
                }
                catch (Exception)
                {
                    MessageBox.Show("A connection to Xalarwse servers was terminated." +
                    "\nSwitching to offline (demo) mode." +
                    "\nContact an administrator or retry connection.",
                    "Connection Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Text = windowName + " (offline mode)"; //PROGRESS BAR RECONNECTION!!!
                    demoMode = true;
                    reconnectGroupBox.Visible = true;  //todo: MAKE IT UPDATE IN INTERVALS INSTEAD OF SEND
                }
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
            Form2 form2 = new Form2(userName,ipAddress,port);
            form2.FormClosed += new FormClosedEventHandler(form2_FormClosed);
            form2.ShowDialog();
        }

        void form2_FormClosed(object sender, EventArgs e)
        {
            //load pic code here !!!!!!!!!!!!!!!!!
            userName = Form2.s_userName;
            userLabel.Text = userName;
            ipAddress = Form2.s_ipAddress;
            port = Form2.s_port;
        }

        private void btnReconnect_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect(ipAddress, Convert.ToInt32(port));
                demoMode = false;
                reconnectGroupBox.Visible = false;
                this.Text = windowName;
                MessageBox.Show("Reconnection to Xalarwse servers successful." +
                    "\nYou are now connected to Xalarwse servers." +
                    "\nYou can send and receive messages.",
                    "Reconnection Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception)
            {
                MessageBox.Show("Reconnection to Xalarwse servers failed." +
                    "\nContact an administrator or retry connection again.",
                    "Reconnection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Text = windowName + " (offline Mode)";
                demoMode = true;
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatsonTcp;

namespace Xalarwse
{
    public partial class Form1 : Form
    {
        private string userName = userdata.Default.userName;
        private static string ipAddress = userdata.Default.ipAddress;
        private static int port = userdata.Default.port;
        private string picAddress = userdata.Default.picAddress;
        private Color userColor = userdata.Default.userColor;

        private string windowName = "Xalarwse";
        private bool demoMode = false;

        public WatsonTcpClient client = new WatsonTcpClient(ipAddress, port);

        public Form1()
        {
            InitializeComponent();
            client.ServerConnected = ServerConnected;
            client.ServerDisconnected = ServerDisconnected;
            client.MessageReceived = MessageReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "Online";
            userLabel.Text = usernameShorten(userName);
            if (!string.IsNullOrEmpty(picAddress)) userAvatar.Image = Bitmap.FromFile(picAddress);

            userAvatar.MouseHover += userAvatar_MouseHover;
            userLabel.MouseHover += userAvatar_MouseHover;

            bool isConnected = false;
            while (!isConnected && !demoMode)
            {
                try
                {
                    client.Start();
                    isConnected = true;
                }
                catch (Exception)
                {
                    DialogResult result = MessageBox.Show("A connection to Xalarwse servers could not be established." +
                        "\nSelect \"Retry\" to reattempt a connection to Xalarwse servers." +
                        "\nSelect \"Cancel\" to cancel reconnection and launch the program in offline (demo) mode.  " +
                        "\nYou can always restard the program later from offline (demo) mode.",
                        "Not Connected", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Cancel)
                    {
                        demoMode = true;
                        this.Text = windowName + " (offline mode)";
                        //reconnectGroupBox.Visible = true;
                    }
                }
            }
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
            toolTipReconnect.SetToolTip(this.btnReconnect, "Click here to restart the Messenger," +
                " and attempt to reconnect to Xalarwse servers.");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(demoMode)
            {
                if (msgTextBox.Text!="")
                {
                    mainTextBox.SelectionColor = userColor;
                    mainTextBox.AppendText($"{userName}: ");
                    mainTextBox.SelectionColor = Color.Red;
                    mainTextBox.AppendText(msgTextBox.Text + " (Not Delivered)" + "\n");
                    msgTextBox.Text = "";
                }
            }
            else
                try
                {
                    client.Send(Encoding.UTF8.GetBytes(msgTextBox.Text));
                    mainTextBox.SelectionColor = userColor;
                    mainTextBox.AppendText($"{userName}: ");
                    mainTextBox.SelectionColor = Color.Black;
                    mainTextBox.AppendText(msgTextBox.Text + "\n");
                    msgTextBox.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("A connection to Xalarwse servers was terminated." +
                    "\nSwitching to offline (demo) mode." +
                    "\nContact an administrator or restart application.",
                    "Connection Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Text = windowName + " (offline mode)";
                    demoMode = true;
                    reconnectGroupBox.Visible = true;
                }
        }

        private string usernameShorten(string username)
        {
            if (username.Length <= 12)
                return username;
            else
                return username.Substring(0, 12);
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
         
        private void btnReconnect_Click(object sender, EventArgs e)
        {
            this.Close();
            //userdata.Default.flag = true;
        }

        async Task MessageReceived(byte[] data)
        {
            if (!demoMode)
            {
                mainTextBox.SelectionColor = Color.Yellow;
                mainTextBox.AppendText($"received: ");
                mainTextBox.SelectionColor = Color.Black;
                mainTextBox.AppendText(Encoding.UTF8.GetString(data) + "\n");
            }

        }

        async Task ServerConnected()
        {
            mainTextBox.AppendText("Connected to server\n");
        }

        async Task ServerDisconnected()
        {
            mainTextBox.AppendText("Disconnected from server\n");
        }
    }
}

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
        public string userName = "user";
        public string receivedUserName = "other user";
        public Color receivedUserColor = Color.Blue;
        bool demoMode = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            if (userName.Length <= 16)
                userLabel.Text = userName;
            else
                userLabel.Text = userName.Substring(0, 15);
            MessageBox.Show("You are not logged in. Please log in.");
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            mainTextBox.Invoke((MethodInvoker)delegate ()
            {
                mainTextBox.SelectionColor = receivedUserColor;
                mainTextBox.AppendText($"{receivedUserName}: ");
                mainTextBox.SelectionColor = Color.Black;
                mainTextBox.AppendText(e.MessageString + "\n");
                msgTextBox.Text = "";
            });
        }

        private void comboBox1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedText = "Online";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(demoMode)
            {
                mainTextBox.SelectionColor = Color.Green;
                mainTextBox.AppendText($"{userName}: ");
                mainTextBox.SelectionColor = Color.Black;
                mainTextBox.AppendText(mainTextBox.Text + "\n");
                msgTextBox.Text = "";
            }
            else
            client.WriteLineAndGetReply(msgTextBox.Text,TimeSpan.FromSeconds(3));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //button 2 = debug
        {
            mainTextBox.SelectionColor = receivedUserColor;
            mainTextBox.AppendText("JaJ: ");
            mainTextBox.SelectionColor = Color.Black;
            mainTextBox.AppendText("geia xalarwse :-)" + "\n");
        }

        private void button3_Click(object sender, EventArgs e) //button 3 = connect button
        {
            button3.Enabled = false;
            client.Connect(Convert.ToString("127.0.0.1"), Convert.ToInt32(8910)); //MUST TEST IP CONNECTION TO SERVER
        }

    }
}

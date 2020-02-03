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

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            richTextBox1.Invoke((MethodInvoker)delegate () //richTextBox1 = txtStatus
            {
                richTextBox1.Text += e.MessageString;
            });
        }

        private void comboBox1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedText = "Online";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //button 1 = send
        {
            client.WriteLineAndGetReply(textBox1.Text,TimeSpan.FromSeconds(3));

            /*richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText("Gera: ");
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText(textBox1.Text + "\n");
            textBox1.Text = "";*/ //old dummy sending
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //button 2 = debug
        {
            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.AppendText("JaJ: ");
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.AppendText("geia xalarwse :-)" + "\n");
        }

        private void button3_Click(object sender, EventArgs e) //button 3 = connect button
        {
            button3.Enabled = false;
            client.Connect(textBox2.Text, Convert.ToInt32(8910));
        }

    }
}

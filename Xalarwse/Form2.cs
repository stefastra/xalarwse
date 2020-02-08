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
            textBoxUsername.Text = Form1.userName;
            textBoxIP.Text = Form1.ipAddress;
            textBoxPort.Text = Form1.port;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Form1.userName = textBoxUsername.Text;
            Form1.ipAddress = textBoxIP.Text; //todo: valid values only
            Form1.port = textBoxPort.Text;

        }
    }
}

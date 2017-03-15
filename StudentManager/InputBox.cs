using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManager
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }
        private string Msg;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //confirm 
            Msg = textBox1.Text;

        }
        public string GetMsg()
        {
            return Msg;
        }
    }
}

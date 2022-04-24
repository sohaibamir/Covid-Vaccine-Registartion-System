using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DemoProject
{
    public partial class Form5 : Form
    {
        string email;
        public Form5(string msg, string center, string mail)
        {
            InitializeComponent();
            label10.Text = msg;
            label2.Text = msg;
            label11.Text = center;
            email = mail;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3(email);
            f3.Show();
        }
    }
}

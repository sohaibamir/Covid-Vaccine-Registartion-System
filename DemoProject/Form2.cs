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
    public partial class Form2 : Form
    {
        static SqlConnection jkt = new SqlConnection("Data Source=DESKTOP-ID34DQS\\SOHAIB;Initial Catalog=DBS1;Integrated Security=True");
        static SqlCommand jkt1;

        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Form2_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            int num = 7;
            string cap = "";
            int total = 0;
            do
            {
                int chr = rand.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    cap = cap + (char)chr;
                    total++;
                    if (total == num)
                    {
                        break;
                    }

                }
            }
            while (true);
            label6.Text = cap.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Submit Button
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
            string.IsNullOrWhiteSpace(textBox2.Text) ||
            string.IsNullOrWhiteSpace(textBox3.Text) ||
            string.IsNullOrWhiteSpace(textBox4.Text) ||
            (label6.Text != textBox5.Text))
            {
                MessageBox.Show("Please fill the textbox");
            }
            else
            {
                MessageBox.Show("Now you can Login");
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
            string query = "INSERT INTO T1 VALUES (@USER, @MAIL, @PASS)";
            jkt.Open();
            jkt1 = new SqlCommand(query, jkt);
            jkt1.Parameters.Add("@USER", SqlDbType.VarChar);
            jkt1.Parameters["@USER"].Value = textBox1.Text;
            jkt1.Parameters.Add("@MAIL", SqlDbType.VarChar);
            jkt1.Parameters["@MAIL"].Value = textBox2.Text;
            jkt1.Parameters.Add("@PASS", SqlDbType.VarChar);
            jkt1.Parameters["@PASS"].Value = textBox3.Text;
            jkt1.ExecuteNonQuery();
            jkt.Close();
        }
    }
}

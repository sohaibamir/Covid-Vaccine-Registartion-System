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
    public partial class Form1 : Form
    {
        static SqlConnection jkt = new SqlConnection("Data Source=DESKTOP-ID34DQS\\SOHAIB;Initial Catalog=DBS1;Integrated Security=True");
        static SqlCommand jkt1;
        bool ismailok = false;
        bool ispassok = false;
        string mail;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Register Button
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Login Button
            if(!Authenticate())
            {
                MessageBox.Show("Please fill the textbox");
                return;
            }
            string query = "SELECT *FROM T1 WHERE Email = @MAIL";
            jkt.Open();
            jkt1 = new SqlCommand(query, jkt);

            //Adding parameters
            jkt1.Parameters.Add("@MAIL", SqlDbType.VarChar);
            jkt1.Parameters["@MAIL"].Value = textBox1.Text;
            //Email stored
            mail = textBox1.Text;

            SqlDataReader sdr = jkt1.ExecuteReader();
            if(sdr.HasRows)
            {
                ismailok = true;
            }
            jkt.Close();

            jkt.Open();
            query = "SELECT *FROM T1 WHERE Email = @MAIL AND Passcode = @PASS";
            jkt1 = new SqlCommand(query, jkt);

            //adding parameters
            jkt1.Parameters.Add("@MAIL", SqlDbType.VarChar);
            jkt1.Parameters["@MAIL"].Value = textBox1.Text;

            jkt1.Parameters.Add("@PASS", SqlDbType.VarChar);
            jkt1.Parameters["@PASS"].Value = textBox2.Text;

            sdr = jkt1.ExecuteReader();
            if (sdr.HasRows)
            {
                ispassok = true;
            }

            if (ismailok == true && ispassok == false)
            {
                MessageBox.Show("Wrong password !!");
                textBox1.Clear();
                textBox2.Clear();
            }

            else if(ismailok == false && ispassok == false)
            {
                MessageBox.Show("User does not exist");
                textBox1.Clear();
                textBox2.Clear();
            }

            else
            {
                textBox1.Clear();
                textBox2.Clear();
                this.Hide();

                //Home Page
                Form3 f3 = new Form3(mail);
                f3.Show();
            }
            jkt.Close();
        }
        bool Authenticate()
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

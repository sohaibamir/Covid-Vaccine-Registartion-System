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
    public partial class Form3 : Form
    {
        static SqlConnection jkt = new SqlConnection("Data Source=DESKTOP-ID34DQS\\SOHAIB;Initial Catalog=DBS1;Integrated Security=True");
        static SqlCommand jkt1;
        string em;
        public Form3(string email)
        {
            InitializeComponent();
            em = email;

        }
        string id;
        bool isidok = false;
        private void button2_Click(object sender, EventArgs e)
        {
            //Check Status Button
            id = Microsoft.VisualBasic.Interaction.InputBox("Enter Your CNIC :");

            string query = "SELECT *FROM T2 WHERE CNIC = @ID";
            jkt.Open();
            jkt1 = new SqlCommand(query, jkt);
            jkt1.Parameters.Add("@ID", SqlDbType.VarChar);
            jkt1.Parameters["@ID"].Value = id;

            SqlDataReader sdr = jkt1.ExecuteReader();
            if (sdr.HasRows)
            {
                isidok = true;
            }

            if ((isidok == false) || (id.Length < 13 || id.Length > 13))
            {
                MessageBox.Show("INVALID CNIC NUMBER");
            }

            else
            {
                this.Hide();

                //check status form
                Form6 f6 = new Form6(id);
                f6.Show();
            }
            jkt.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Personal Info Button
            this.Hide();
            Form4 f4 = new Form4(em);
            f4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Update Button
            Form7 f7 = new Form7(id, em);
            f7.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}

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
using System.IO;

namespace DemoProject
{
    public partial class Form6 : Form
    {
        static SqlConnection jkt = new SqlConnection("Data Source=DESKTOP-ID34DQS\\SOHAIB;Initial Catalog=DBS1;Integrated Security=True");
        static SqlCommand jkt1;
        string CNIC;
        public Form6(string Id)
        {
            InitializeComponent();
            CNIC = Id;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string sayan = "SELECT Fname, CNIC, VaccinationDate, VaccinationCenter, NvaccinationDate, Image FROM T2 WHERE CNIC = @ID";
            jkt.Open();
            jkt1 = new SqlCommand(sayan, jkt);
            jkt1.Parameters.Add("@ID", SqlDbType.VarChar);
            jkt1.Parameters["@ID"].Value = CNIC;
            SqlDataReader sdr = jkt1.ExecuteReader();
            if(sdr.Read())
            {
                label2.Text = sdr["Fname"].ToString();
                label5.Text = sdr["VaccinationDate"].ToString();
                label7.Text = sdr["NvaccinationDate"].ToString();
                label8.Text = sdr["VaccinationCenter"].ToString();
                label9.Text = sdr["VaccinationCenter"].ToString();
                MemoryStream stream = new MemoryStream(sdr.GetSqlBytes(5).Buffer);
                pictureBox1.Image = Image.FromStream(stream);
            }
            jkt.Close();
                
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3(CNIC);
            f3.Show();
        }
    }
}

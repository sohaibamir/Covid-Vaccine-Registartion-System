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
using System.Net;
using System.Net.Mail;

namespace DemoProject
{
    public partial class Form7 : Form
    {
        static SqlConnection jkt = new SqlConnection("Data Source=DESKTOP-ID34DQS\\SOHAIB;Initial Catalog=DBS1;Integrated Security=True");
        static SqlCommand jkt1;
        string CNIC;
        string email;
        public Form7(string Id, string mail)
        {
            InitializeComponent();
            CNIC = Id;
            email = mail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBox1.Text) ||
            (comboBox1.SelectedIndex == -1) ||
            (comboBox2.SelectedIndex == -1) ||
            string.IsNullOrWhiteSpace(dateTimePicker1.Text))
            {
                MessageBox.Show("Please fill the textbox");
            }
            else
            {
                string query = "UPDATE T2 SET Vaccine = @VACCINE, VaccinationCenter = @VCENTER, VaccinationDate = @VACCINEDATE, NvaccinationDate = @NVDATE WHERE CNIC = @ID";
                jkt.Open();
                jkt1 = new SqlCommand(query, jkt);
                jkt1.Parameters.Add("@ID", SqlDbType.VarChar);
                jkt1.Parameters["@ID"].Value = kryptonTextBox1.Text;
                jkt1.Parameters.AddWithValue("@VACCINE", comboBox1.Text);
                jkt1.Parameters.AddWithValue("@VCENTER", comboBox2.Text);
                jkt1.Parameters.AddWithValue("@VACCINEDATE", dateTimePicker1.Text);
                string NextDate = (dateTimePicker1.Value.AddDays(21).AddMonths(0).AddYears(0)).ToString();
                jkt1.Parameters.AddWithValue("@NVDATE", NextDate);

                string from = "vaccinationsystem420@gmail.com";
                string pass = "jkt12345";
                string to = email;
                string path = "F:/template1.txt";
                string txt2 = File.ReadAllText(path);

                MailMessage message = new MailMessage();

                message.From = new MailAddress(from);
                message.To.Add(to);
                message.Subject = "VACCINE REGISTRATION";
                message.Body = "Dear, " + "User" + "\n" + txt2  + "\n" + "Your Vaccine : " + comboBox1.Text + "\n" + "Your Centre : " + comboBox2.Text;

                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential(from, pass);
                SmtpServer.Send(message);

                jkt1.ExecuteNonQuery();
                jkt.Close();

                MessageBox.Show("Record Updated Successfully! ");
                jkt.Close();
                kryptonTextBox1.Clear();
                comboBox1.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Text = "";
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3(email);
            f3.Show();
        }
    }
}

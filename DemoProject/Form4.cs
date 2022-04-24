using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


namespace DemoProject
{
    public partial class Form4 : Form
    {

        static SqlConnection jkt = new SqlConnection("Data Source=DESKTOP-ID34DQS\\SOHAIB;Initial Catalog=DBS1;Integrated Security=True");
        static SqlCommand jkt1;
        string NextDate;
        string email;

        public Form4(string mail)
        {
            InitializeComponent();
            email = mail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Home Page
            this.Hide();
            Form3 f3 = new Form3(email);
            f3.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if ((radioButton1.Checked == false && radioButton2.Checked == false) ||
            (radioButton3.Checked == false && radioButton4.Checked == false && radioButton5.Checked == false) ||
            (radioButton8.Checked == false && radioButton9.Checked == false) ||
            string.IsNullOrWhiteSpace(kryptonTextBox1.Text) ||
            string.IsNullOrWhiteSpace(kryptonTextBox2.Text) ||
            string.IsNullOrWhiteSpace(kryptonTextBox3.Text) ||
            (comboBox1.SelectedIndex == -1) ||
            (comboBox2.SelectedIndex == -1))
            {
                MessageBox.Show("Please fill all the information");
            }
            else
            {
                this.Hide();
                Form5 f5 = new Form5(kryptonTextBox3.Text,comboBox2.Text,email);
                f5.Show();
            }
            NextDate = (dateTimePicker1.Value.AddDays(21).AddMonths(0).AddYears(0)).ToString();
           
            string query = "INSERT INTO T2 VALUES (@IACTION, @GENDER, @AGE, @NAME, @ID, @PNO, @VACCINE, @VCENTER, @VACCINEDATE, @NVDATE, @IMAGE)";
            jkt.Open();
            jkt1 = new SqlCommand(query, jkt);

            if(radioButton1.Checked == true)
            {
                jkt1.Parameters.Add("@IACTION", SqlDbType.VarChar);
                jkt1.Parameters["@IACTION"].Value = "Yes";
            }
            else
            {
                jkt1.Parameters.Add("@IACTION", SqlDbType.VarChar);
                jkt1.Parameters["@IACTION"].Value = "No";
            }

            if (radioButton3.Checked == true)
            {
                jkt1.Parameters.Add("@GENDER", SqlDbType.VarChar);
                jkt1.Parameters["@GENDER"].Value = "Male";
            }

            else if(radioButton4.Checked == true)
            {
                jkt1.Parameters.Add("@GENDER", SqlDbType.VarChar);
                jkt1.Parameters["@GENDER"].Value = "Female";
            }

            else
            {
                jkt1.Parameters.Add("@GENDER", SqlDbType.VarChar);
                jkt1.Parameters["@GENDER"].Value = "Other";
            }

            if (radioButton9.Checked == true)
            {
                jkt1.Parameters.Add("@AGE", SqlDbType.VarChar);
                jkt1.Parameters["@AGE"].Value = "18-60";
            }
            else
            {
                jkt1.Parameters.Add("@AGE", SqlDbType.VarChar);
                jkt1.Parameters["@AGE"].Value = "Above 60";
            }

            jkt1.Parameters.Add("@NAME", SqlDbType.VarChar);
            jkt1.Parameters["@NAME"].Value = kryptonTextBox3.Text;

            jkt1.Parameters.Add("@ID", SqlDbType.VarChar);
            jkt1.Parameters["@ID"].Value = kryptonTextBox1.Text;

            jkt1.Parameters.Add("@PNO", SqlDbType.VarChar);
            jkt1.Parameters["@PNO"].Value = kryptonTextBox2.Text;

            jkt1.Parameters.Add("@VACCINE", SqlDbType.VarChar);
            jkt1.Parameters["@VACCINE"].Value = comboBox1.Text;

            jkt1.Parameters.Add("@VCENTER", SqlDbType.VarChar);
            jkt1.Parameters["@VCENTER"].Value = comboBox2.Text;

            jkt1.Parameters.Add("@VACCINEDATE", SqlDbType.VarChar);
            jkt1.Parameters["@VACCINEDATE"].Value = dateTimePicker1.Text;

            jkt1.Parameters.Add("@NVDATE", SqlDbType.VarChar);
            jkt1.Parameters["@NVDATE"].Value = NextDate;

            var image = new ImageConverter().ConvertTo(pictureBox1.Image, typeof(Byte[]));
            jkt1.Parameters.AddWithValue("@image", image);

            string from = "vaccinationsystem420@gmail.com";
            string pass = "jkt12345";
            string to = email;
            string path = "F:/template2.txt";
            string txt2 = File.ReadAllText(path);

            MailMessage message = new MailMessage();

            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = "VACCINE REGISTRATION";
            message.Body = "Dear, " + kryptonTextBox3.Text + "\n" + txt2  + "\n" + "Your Vaccine : " + comboBox1.Text + "\n" + "Your Centre : " + comboBox2.Text;

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new System.Net.NetworkCredential(from, pass);

            try
            {
                SmtpServer.Send(message);
                MessageBox.Show("Email sent");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            jkt1.ExecuteNonQuery();
            jkt.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.FileName = "";
            OD.Filter = "Select Images|*.jpg; *.png; *.jpeg";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(OD.FileName);
            }
        }

    }
}

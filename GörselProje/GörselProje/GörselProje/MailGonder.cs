using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GörselProje
{
    public partial class MailGonder : Form
    {
        public MailGonder()
        {
            InitializeComponent();
        }
        private string konu = "";
        private string icerik = "";
        private string isaret = "xxxxx";
        
        private void button1_Click(object sender, EventArgs e)
        {
            string mailAdresi = "baranalyar7@gmail.com";
            string aliciMailAdresi = textBox2.Text;

            
            string icerik = richTextBox1.Text;

            
            Gonder(mailAdresi, aliciMailAdresi, icerik);

            MessageBox.Show("Mail başarıyla gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void Gonder(string gonderenMail, string aliciMail,string icerik)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(gonderenMail, "aecs odob wzaw liby");
            smtp.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(gonderenMail);
            mail.To.Add(aliciMail);
            mail.Body = icerik;

            smtp.Send(mail);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                e.Handled = true;
            }


            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '@')
            {

                e.Handled = true;
                MessageBox.Show("Lütfen geçerli bir mail adresi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                textBox2.Text = textBox2.Text.ToLower();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            YöneticiMain main = new YöneticiMain();
            main.Show();
            this.Hide();
        }

        private void MailGonder_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                konu = "Yeni Müşteri Bilgileri";
                icerik = $"Merhaba Sayın {isaret};\n\nBankamızı tercih ettiğiniz için teşekkür ederiz yolculuk. Bankamızı kullanabilmeniz için aşağıda gerekli bilgileri bulabilirsiniz. İyi günlerde kullanmanız dileğiyle.\n\nHesap No : {isaret}\nParola : {isaret}";
                GuncelleRichTextBox();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                konu = "Güncellenen Müşteri Bilgileri";
                icerik = $"Merhaba Sayın {isaret};\n\nGüncellediğimiz bilgileriniz. Bankamızı kullandığınız için teşekkür eder iyi günler dileriz.\n\nHesap No : {isaret}\nParola : {isaret}";
                GuncelleRichTextBox();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                konu = "";
                icerik = ""; 
                GuncelleRichTextBox();
            }
        }
        private void GuncelleRichTextBox()
        {
            richTextBox1.Clear();
            richTextBox1.Text = $"Konu: {konu}\n\n\n{icerik}";

            
            richTextBox1.Select(0, konu.Length + 8); 
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14, FontStyle.Regular);

            richTextBox1.Select(konu.Length + 8, richTextBox1.Text.Length - (konu.Length + 8)); 
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 10, FontStyle.Regular);

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


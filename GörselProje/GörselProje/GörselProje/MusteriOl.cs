using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProje
{
    public partial class MusteriOl : Form
    {
        public MusteriOl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False"))
            {
                
                string sorgu = "INSERT INTO istek (adsoyad, tc, parola, mail,resimad,resim) VALUES (@adsoyad, @tc, @parola, @mail,@resimad,@resim)";

                
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                   
                    komut.Parameters.AddWithValue("@adsoyad", textBox1.Text);
                    komut.Parameters.AddWithValue("@tc", textBox2.Text);
                    komut.Parameters.AddWithValue("@parola", textBox3.Text);
                    komut.Parameters.AddWithValue("@mail", textBox4.Text);

                    
                    komut.Parameters.AddWithValue("@resimad", textBox5.Text);

                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        komut.Parameters.AddWithValue("@resim", ms.ToArray());
                    }


                        
                        baglanti.Open();

                        
                        komut.ExecuteNonQuery();

                        
                        MessageBox.Show("İstek başarıyla eklendi", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Tüm Dosyalar|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
               
                pictureBox1.ImageLocation = openFileDialog.FileName;

                
                textBox5.Text = openFileDialog.SafeFileName;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                e.Handled = true;
            }

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("TC Kimlik Numarası sadece rakam içermelidir.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (textBox2.Text.Length >= 11 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("TC Kimlik Numarası 11 rakamdan fazla olamaz.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                e.Handled = true;
            }

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ad ve soyad sadece harf içerebilir.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                e.Handled = true;
            }


            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Parola sadece rakam içermelidir.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            if (textBox3.Text.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Parola 6 rakamdan fazla olamaz.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
                
                textBox4.Text = textBox4.Text.ToLower();
            }
        }

        private void MusteriOl_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            giris gir = new giris();
            gir.Show();
            this.Hide();
        }
    }
}

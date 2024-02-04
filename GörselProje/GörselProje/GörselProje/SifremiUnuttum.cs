using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GörselProje
{
    public partial class SifremiUnuttum : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public SifremiUnuttum()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update musteri set parola=  @p1 where tc= @p2 and hno= @p3  ", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox3.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            komut.Parameters.AddWithValue("@p3", textBox2.Text);



            if (textBox3.Text.Length < 6)
            {
                MessageBox.Show("Lütfen yeni şifreniz en az 6 karakter uzunluğunda olsun!", "Şifre Değiştirme Hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();

                int sonuc = komut.ExecuteNonQuery();
                baglanti.Close();
                if (sonuc == 1)
                {
                    MessageBox.Show("Yeni Şifreniz üretilmiştir ", "Şifre üretme işlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                    MessageBox.Show("Hesap No / TC No Hatası !", "Şifre üretme hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            giris gir = new giris();
            gir.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("TC Kimlik Numarası sadece rakam içermelidir.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (textBox1.Text.Length >= 11 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("TC Kimlik Numarası 11 rakamdan fazla olamaz.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Hesap No sadece rakam içermelidir.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (textBox2.Text.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Hesap No 6 rakamdan fazla olamaz.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void SifremiUnuttum_Load(object sender, EventArgs e)
        {

        }
    }
}

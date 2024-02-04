using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GörselProje
{
    public partial class giris : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public giris()
        {
            InitializeComponent();
        }

        public static string ad = "";
        public static int hno = 1;
        public static int bakiye = 0;
        public static int borc = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            anasayfa anasayfaForm = new anasayfa();
            anasayfaForm.Show();
            this.Hide();
        }

        private void giris_Load(object sender, EventArgs e)
        {


            VerileriGetir();
        }


       

        private void VerileriGetir()
        {

                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

               
                string query = "SELECT TOP 1 dbaslik, duyuru FROM duyuru ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(query, baglanti))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            textBox6.Text = reader["dbaslik"].ToString();
                            
                            textBox5.Text = reader["duyuru"].ToString();
                        }
                    }
                }
            

                
                    baglanti.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool girisKontrolu = false;

            // Kullanıcı giriş
            baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT * FROM musteri WHERE hno = @p1 AND parola = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);

            using (SqlDataReader dr = komut.ExecuteReader())
            {
                if (dr.Read())
                {
                    ad = dr["adsoyad"].ToString();
                    hno = int.Parse(dr["hno"].ToString());
                    bakiye = int.Parse(dr["bakiye"].ToString());
                    string yetki = dr["yetki"].ToString();

                    if (yetki == "admin")
                    {
                        YöneticiMain merakliSayfa = new YöneticiMain();
                        this.Hide();
                        merakliSayfa.Show();
                    }
                    else if (yetki == "musteri")
                    {
                        anasayfa anasayfa = new anasayfa();
                        this.Hide();
                        anasayfa.Show();
                    }

                    girisKontrolu = true;
                }
            }

            if (!girisKontrolu)
            {
                MessageBox.Show("Hatalı HesapNo veya Parola ", "Hatalı Giriş Denemesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            baglanti.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SifremiUnuttum sifre = new SifremiUnuttum();
            sifre.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MusteriOl m11= new MusteriOl();
            m11.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                e.Handled = true;
            }

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Hesap No Sadece Sayı İçermelidir", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (textBox1.Text.Length >= 12 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Hesap No 12 Haneli Olmalıdır", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Parola Sadece Sayı İçermelidir", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (textBox2.Text.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Parola 6 Haneli Olmalıdır", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
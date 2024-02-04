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

namespace GörselProje
{
    public partial class MusteriSil : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public MusteriSil()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string birlesikSorgu = @"
                SELECT musteri.*, kart.*
                FROM musteri
                LEFT JOIN kart ON musteri.hno = kart.mid
                WHERE musteri.hno = @p1;";

            using (SqlCommand birlesikKomut = new SqlCommand(birlesikSorgu, baglanti))
            {
                birlesikKomut.Parameters.AddWithValue("@p1", textBox1.Text);

                baglanti.Open();

                using (SqlDataReader birlesikDr = birlesikKomut.ExecuteReader())
                {
                    if (birlesikDr.Read())
                    {

                        textBox2.Text = birlesikDr["hno"].ToString();
                        textBox3.Text = birlesikDr["tc"].ToString();
                        textBox4.Text = birlesikDr["adsoyad"].ToString();
                        textBox5.Text = birlesikDr["bakiye"].ToString();
                        textBox6.Text = birlesikDr["katilimtarih"].ToString();
                        textBox7.Text = birlesikDr["kart"].ToString();


                        if (birlesikDr["kart"] != DBNull.Value && birlesikDr["kart"].ToString().ToLower() == "var")
                        {

                            textBox13.Text = birlesikDr["KartTuru"].ToString();
                            textBox12.Text = birlesikDr["kartno"].ToString();
                            textBox11.Text = birlesikDr["BorcDurumu"].ToString();
                            textBox10.Text = birlesikDr["KullanilabilirBakiye"].ToString();
                            textBox9.Text = birlesikDr["ccv"].ToString();
                            textBox8.Text = birlesikDr["sonkullanma"].ToString();
                        }
                        else
                        {

                            textBox13.Text = "Kartı Yok";
                            textBox12.Text = "Kartı Yok";
                            textBox11.Text = "Kartı Yok";
                            textBox10.Text = "Kartı Yok";
                            textBox9.Text = "Kartı Yok";
                            textBox8.Text = "Kartı Yok";

                            MessageBox.Show(textBox1.Text + " Numaralı Müşteriye Ait Kredi Kart Bilgisi Bulunamadı!", "Kart Bilgisi Arama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(textBox1.Text + " Numaralı Müşteri Bulunamadı!", "Müşteri Arama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }


            baglanti.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            YöneticiMain main = new YöneticiMain();
            main.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand musteriSilKomut = new SqlCommand("DELETE FROM musteri WHERE hno = @p1", baglanti);
            musteriSilKomut.Parameters.AddWithValue("@p1", textBox1.Text);

            SqlCommand kartSilKomut = new SqlCommand("DELETE FROM kart WHERE mid = @p1", baglanti);
            kartSilKomut.Parameters.AddWithValue("@p1", textBox1.Text);

                baglanti.Open();
                kartSilKomut.ExecuteNonQuery();

                musteriSilKomut.ExecuteNonQuery();

                MessageBox.Show("Müşteri ve kart bilgileri silindi", "Müşteri Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            


                baglanti.Close();

        }

        private void MusteriSil_Load(object sender, EventArgs e)
        {

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

            if (textBox1.Text.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Hesap No 6 Haneli Olmalıdır", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

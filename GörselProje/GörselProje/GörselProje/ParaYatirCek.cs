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
    public partial class ParaYatirCek : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public ParaYatirCek()
        {
            InitializeComponent();
        }

        private void ParaYatirCek_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            label20.Text = giris.ad;
            label22.Text = giris.hno.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = string.IsNullOrEmpty(textBox1.Text);
            button2.Enabled = false;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = string.IsNullOrEmpty(textBox2.Text);
            button1.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sayi = int.Parse(textBox1.Text);

            SqlCommand komut = new SqlCommand("update musteri set bakiye= bakiye +  @p1 where hno= @p2  ", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            komut.Parameters.AddWithValue("@p2", giris.hno);

            if (sayi < 10)
            {
                MessageBox.Show("Lütfen 10 TL ve üzeri giriniz !", "Eksik Kayıt Hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Para yatırma işlemi gerçekleştirildi ", "Para Yatırma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                giris.bakiye += sayi;
                hareket.kaydet(giris.hno, (sayi + " TL Para Yatırıldı "));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sayi = int.Parse(textBox2.Text);

            if (sayi > giris.bakiye)
            {
                MessageBox.Show("Yetersiz Bakiye", "Pare Çekme İşlemi");
            }
            else
            {

                SqlCommand komut = new SqlCommand("update musteri set bakiye= bakiye -  @p1 where hno= @p2  ", baglanti);
                komut.Parameters.AddWithValue("@p1", sayi);
                komut.Parameters.AddWithValue("@p2", giris.hno);

                if (sayi < 10)
                {
                    MessageBox.Show("Lütfen 10 TL ve üzeri giriniz !", "Eksik Kayıt Hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    baglanti.Open();

                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Para çekme işlemi gerçekleştirildi ", "Para Çekme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    giris.bakiye -= sayi;

                    hareket.kaydet(giris.hno, (sayi + " TL Para Çekildi"));


                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;

        }


        private void button9_Click(object sender, EventArgs e)
        {
            ParaTransferi gonder = new ParaTransferi();

            gonder.Show();

            this.Hide();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Hesap y7 = new Hesap();

            y7.Show();
            this.Hide();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            giris y6 = new giris();

            y6.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            BorcOde y5 = new BorcOde();

            y5.Show();

            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            KartBaşvuru y3 = new KartBaşvuru();

            y3.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ParaYatirCek y2 = new ParaYatirCek();

            y2.Show();
            this.Hide();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            HesapHareketleri y1 = new HesapHareketleri();

            y1.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            anasayfa ana = new anasayfa();
            ana.Show();
            this.Hide();
        }
    }
}

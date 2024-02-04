using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProje
{
    public partial class BorcOde : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public BorcOde()
        {
            InitializeComponent();
        }

        private void BorcOde_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            label5.Text = giris.ad;
            label6.Text = giris.hno.ToString();
            label20.Text = giris.ad;
            label22.Text = giris.hno.ToString();

            baglanti.Open();

            if (int.TryParse(label6.Text, out int mid))
            {
                string komut = $"SELECT kartno,BorcDurumu FROM kart WHERE mid = {mid}";

                using (SqlCommand command = new SqlCommand(komut, baglanti))
                {
                    SqlDataReader oku = command.ExecuteReader();

                    if (oku.Read())
                    {
                        label7.Text = oku["kartno"].ToString();

                        if (oku["BorcDurumu"] != DBNull.Value)
                        {
                            float borcDurumu = float.Parse(oku["BorcDurumu"].ToString());
                            textBox1.Text = borcDurumu == 0 ? "Borç Yoktur" : borcDurumu.ToString();
                        }
                        else
                        {
                            textBox1.Text = "Borç Yoktur";
                        }
                    }
                    else
                    {
                        textBox1.Text = "Kredi Kartı Yoktur!";
                    }

                    oku.Close();
                }
            }

            baglanti.Close();
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sayi;

            if (!int.TryParse(textBox2.Text, out sayi))
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.");
                return;
            }

            float borcDurumu;

            if (!float.TryParse(textBox1.Text, out borcDurumu))
            {
                MessageBox.Show("Borç durumu geçerli bir sayı değil.");
                return;
            }
            float bakiye;

            baglanti.Open();
            string bakiyeSorgu = $"SELECT bakiye FROM musteri WHERE hno = {giris.hno}";
            using (SqlCommand bakiyeCommand = new SqlCommand(bakiyeSorgu, baglanti))
            {
                object bakiyeObj = bakiyeCommand.ExecuteScalar();

                if (bakiyeObj != null && float.TryParse(bakiyeObj.ToString(), out bakiye))
                {
                    if (sayi > bakiye)
                    {
                        MessageBox.Show("Yetersiz bakiye! Lütfen daha düşük bir miktar girin.");
                    }
                    else if (sayi > borcDurumu)
                    {
                        MessageBox.Show("Lütfen borcunuzdan düşük bir miktar girerek hesaplama yapınız.");
                    }
                    else
                    {
                        MessageBox.Show("Borcunuzu Ödeyebilirsiniz!");
                        button1.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Bakiye bilgisi alınamadı.");
                }
            }

            baglanti.Close();
            button1.Visible = true;
            button2.Visible=false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sayi = int.Parse(textBox2.Text);


            SqlCommand komut = new SqlCommand("update musteri set bakiye= bakiye -  @p1 where hno= @p2  ", baglanti);
                    komut.Parameters.AddWithValue("@p1", sayi);
                    komut.Parameters.AddWithValue("@p2", giris.hno);

                    SqlCommand komut2 = new SqlCommand("update kart set BorcDurumu= BorcDurumu -  @p1 where mid= @p2  ", baglanti);
                    komut2.Parameters.AddWithValue("@p1", sayi);
                    komut2.Parameters.AddWithValue("@p2", giris.hno);

                    baglanti.Open();

                    int sonuc1 = komut2.ExecuteNonQuery();
                    baglanti.Close();

                    if (sonuc1 == 1)
                    {
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Borç Ödeme İşlemi Gerçekleştirildi", "Borç Ödeme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        giris.bakiye -= sayi;

                        hareket.kaydet(giris.hno, (sayi + " TL Borç Ödendi"));



                    }
            this.Refresh();
        }

       

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            giris y6 = new giris();

            y6.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hesap y7 = new Hesap();

            y7.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ParaTransferi gonder = new ParaTransferi();

            gonder.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BorcOde y5 = new BorcOde();

            y5.Show();

            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KartBaşvuru y3 = new KartBaşvuru();

            y3.Show();
            this.Hide();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            ParaYatirCek y2 = new ParaYatirCek();

            y2.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
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

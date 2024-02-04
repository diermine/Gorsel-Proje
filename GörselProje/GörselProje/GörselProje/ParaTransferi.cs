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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GörselProje
{
    public partial class ParaTransferi : Form
    {
        

        public ParaTransferi()
        {
            InitializeComponent();
            



        }
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sayi = int.Parse(textBox2.Text);

            if (sayi > giris.bakiye)
            {
                MessageBox.Show("Yetersiz Bakiye", "Pare Çekme İşlemi");
            }
            else
            {
                if (hesap.Checked ) { 
                    SqlCommand komut = new SqlCommand("update musteri set bakiye= bakiye -  @p1 where hno= @p2  ", baglanti);
                    komut.Parameters.AddWithValue("@p1", sayi);
                    komut.Parameters.AddWithValue("@p2", giris.hno);

                    SqlCommand komut2 = new SqlCommand("update musteri set bakiye= bakiye +  @p3 where hno= @p4  ", baglanti);
                    komut2.Parameters.AddWithValue("@p3", textBox2.Text);
                    komut2.Parameters.AddWithValue("@p4", textBox1.Text);

                    if (sayi < 10)
                    {
                        MessageBox.Show("Lütfen 10 TL ve üzeri giriniz !", "Eksik Kayıt Hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        baglanti.Open();

                        int sonuc1 = komut2.ExecuteNonQuery();
                        baglanti.Close();

                        if (sonuc1 == 1)
                        {
                            baglanti.Open();
                            komut.ExecuteNonQuery();
                            baglanti.Close();
                            MessageBox.Show("Havale işlemi gerçekleştirildi ", "Havale / EFT ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            giris.bakiye -= sayi;

                            hareket.kaydet(giris.hno, (sayi + " TL Havale/EFT gönderildi"));
                            hareket.kaydet(int.Parse(textBox1.Text), (sayi + " TL Havale/EFT alındı"));


                        }
                        else
                        {
                            MessageBox.Show("Alıcı Hesap No Hatalı !", "Havale/ EFT Hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else if(kart.Checked)
                {
                    SqlCommand komut = new SqlCommand("UPDATE kart SET KullanilabilirBakiye = KullanilabilirBakiye - @p1, BorcDurumu = BorcDurumu + @p1 WHERE mid = @p2", baglanti);
                    komut.Parameters.AddWithValue("@p1", sayi);
                    komut.Parameters.AddWithValue("@p2", giris.hno);

                    SqlCommand komut2 = new SqlCommand("update musteri set bakiye= bakiye +  @p3 where hno= @p4  ", baglanti);
                    komut2.Parameters.AddWithValue("@p3", textBox2.Text);
                    komut2.Parameters.AddWithValue("@p4", textBox1.Text);


                    if (sayi < 10)
                    {
                        MessageBox.Show("Lütfen 10 TL ve üzeri giriniz !", "Eksik Kayıt Hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        baglanti.Open();

                        int sonuc1 = komut2.ExecuteNonQuery();
                        baglanti.Close();

                        if (sonuc1 == 1)
                        {
                            baglanti.Open();
                            komut.ExecuteNonQuery();
                            baglanti.Close();
                            MessageBox.Show("Havale işlemi gerçekleştirildi ", "Havale/EFT Kredi Kartıyla Yapıldı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            giris.bakiye -= sayi;

                            hareket.kaydet(giris.hno, (sayi + " TL Kredi Kartıyla Gönderildi"));
                            hareket.kaydet(int.Parse(textBox1.Text), (sayi + " TL Havale/EFT alındı"));


                        }
                        else
                        {
                            MessageBox.Show("Alıcı Hesap No Hatalı !", "Havale/ EFT Hatası ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }


                
            }
        }




        private void ParaTransferi_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            label20.Text = giris.ad;
            label22.Text=giris.hno.ToString();


            baglanti.Open();

                string query = "SELECT kart FROM musteri WHERE hno = @hno";
                using (SqlCommand command = new SqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@hno", giris.hno);

                    object kartValue = command.ExecuteScalar();

                    if (kartValue != null && kartValue != DBNull.Value)
                    {
                        if (kartValue.ToString().ToLower() == "var")
                        {
                            kart.Enabled = true; // Kart
                        }
                        else
                        {
                            kart.Enabled = false;
                        }
                    }
                }
            
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            

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

        private void button8_Click_1(object sender, EventArgs e)
        {
            ParaYatirCek y2 = new ParaYatirCek();

            y2.Show();
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

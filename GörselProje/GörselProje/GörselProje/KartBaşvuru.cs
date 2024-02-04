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
    public partial class KartBaşvuru : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public KartBaşvuru()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string secilenMeslek = comboBox1.SelectedItem?.ToString();
            decimal maas;

            if (!decimal.TryParse(textBox1.Text, out maas))
            {
                MessageBox.Show("Geçerli bir maaş miktarı giriniz.");
                return;
            }

            DateTime katilimTarihi = GetMusteriKatilimTarihi();

            if ((DateTime.Now - katilimTarihi).TotalDays >= 365)
            {
                decimal limit = 0;
                switch (secilenMeslek)
                {
                    case "Öğrenci":
                        limit = maas * 1.5m;
                        textBox7.Text = "Öğrenci Kartı";
                        break;
                    case "İşçi":
                    case "Memur":
                    case "Doktor":
                    case "Polis":
                        limit = maas * 2m;
                        textBox7.Text = "İşçi  Kartı";
                        break;
                    case "Çiftçi":
                        limit = maas * 3m;
                        textBox7.Text = "Çiftçi Kartı";
                        break;
                    default:
                        MessageBox.Show("Geçerli bir meslek seçiniz.");
                        return;
                }

                string kartNumarasi = RandomKartNumarasiOlustur();
                int ccv = RandomCCVOlustur();
                DateTime kartAlmaTarihi = DateTime.Now;
                DateTime sonKullanmaTarihi = kartAlmaTarihi.AddYears(5);

                textBox3.Text = limit.ToString();
                textBox5.Text = sonKullanmaTarihi.Year.ToString();
                textBox4.Text = kartNumarasi;
                textBox6.Text = ccv.ToString();
                textBox2.Text = kartAlmaTarihi.AddMonths(1).ToString("dd.MM.yyyy");
            }
            else
            {
                int kalanGun = (int)(365 - (DateTime.Now - katilimTarihi).TotalDays);
                MessageBox.Show($"Kredi çekebilmek için en az 365 gün geçmesi gerekmektedir. {kalanGun} gün daha bekleyiniz.");
            }
            button2.Visible = true;
        }
        private DateTime GetMusteriKatilimTarihi()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT katilimtarih FROM musteri WHERE hno = @hno", baglanti);
            komut.Parameters.AddWithValue("@hno", giris.hno);
            DateTime katilimTarihi = (DateTime)komut.ExecuteScalar();
            baglanti.Close();

            return katilimTarihi;
        }
        private string RandomKartNumarasiOlustur()
        {
            Random random = new Random();
            long numara = ((long)random.Next(0, int.MaxValue) << 32) | (long)random.Next(0, int.MaxValue);
            return numara.ToString().Substring(0, 16);
        }

        private int RandomCCVOlustur()
        {
            Random random = new Random();
            return random.Next(100, 999);
        }

        private void KartBaşvuru_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            label2.Text = giris.ad;
            label4.Text = giris.hno.ToString();
            label20.Text = giris.ad;
            label22.Text= giris.hno.ToString();
            button2.Visible= false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

            try
            {
                baglanti.Open();

                
                SqlCommand kartKontrolKomut = new SqlCommand("SELECT COUNT(*) FROM kart WHERE mid = @mid", baglanti);
                kartKontrolKomut.Parameters.AddWithValue("@mid", giris.hno);
                int kartSayisi = (int)kartKontrolKomut.ExecuteScalar();

                if (kartSayisi > 0)
                {
                   
                    MessageBox.Show("Zaten bir kartınız bulunmaktadır.");
                }
                else
                {
                    
                    SqlCommand komut = new SqlCommand("INSERT INTO kart (mid, KartTuru, kartno, BorcDurumu, SonOdemeTarihi, KullanilabilirBakiye, CCV, SonKullanma) " +
                                                       "VALUES (@mid, @KartTuru, @kartno, @BorcDurumu, @SonOdemeTarihi, @KullanilabilirBakiye, @CCV, @SonKullanma)", baglanti);

                    komut.Parameters.AddWithValue("@mid", giris.hno);
                    komut.Parameters.AddWithValue("@KartTuru", textBox7.Text);
                    komut.Parameters.AddWithValue("@kartno", textBox4.Text);
                    komut.Parameters.AddWithValue("@BorcDurumu", 0);
                    komut.Parameters.AddWithValue("@SonOdemeTarihi", DateTime.Now.AddMonths(1)); 
                    komut.Parameters.AddWithValue("@KullanilabilirBakiye", decimal.Parse(textBox3.Text));
                    komut.Parameters.AddWithValue("@CCV", int.Parse(textBox6.Text));
                    komut.Parameters.AddWithValue("@SonKullanma", DateTime.Now.AddYears(5));

                    SqlCommand komut2 = new SqlCommand("UPDATE musteri SET kart = 'var' WHERE hno = @mid", baglanti);
                    komut2.Parameters.AddWithValue("@mid", giris.hno);
                    komut2.ExecuteNonQuery();


                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kart başvurunuz başarıyla tamamlandı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            HesapHareketleri y1 = new HesapHareketleri();

            y1.Show();
            this.Hide();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            ParaYatirCek y2 = new ParaYatirCek();

            y2.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KartBaşvuru y3 = new KartBaşvuru();

            y3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BorcOde y5 = new BorcOde();

            y5.Show();

            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ParaTransferi gonder = new ParaTransferi();

            gonder.Show();

            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hesap y7 = new Hesap();

            y7.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            giris y6 = new giris();

            y6.Show();
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

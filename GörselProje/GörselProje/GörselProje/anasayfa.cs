using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GörselProje
{
    public partial class anasayfa : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");
        public anasayfa()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string query = "SELECT kart FROM musteri WHERE hno = @hno";

            baglanti.Open();

            using (SqlCommand command = new SqlCommand(query, baglanti))
            {

                command.Parameters.AddWithValue("@hno", giris.hno);

                object kartValue = command.ExecuteScalar();

                if (kartValue != null && kartValue != DBNull.Value && kartValue.ToString().ToLower() == "var")
                {
                    panel3.Visible = false;
                }
                else
                {
                    panel3.Visible = true;
                }
            }
            baglanti.Close();

            groupBox1.Visible = false;
            label20.Text = giris.ad;
            label22.Text = giris.hno.ToString();
            label4.Text = giris.bakiye.ToString();


            baglanti.Open();
            if (int.TryParse(label22.Text, out int mid))
            {
                string komut = $"SELECT kartno, BorcDurumu, KullanilabilirBakiye, KartTuru FROM kart WHERE mid = {mid}";

                using (SqlCommand command = new SqlCommand(komut, baglanti))
                {
                    SqlDataReader oku = command.ExecuteReader();
                    if (oku.Read())
                    {
                        label5.Text = oku["KartTuru"].ToString();
                        label11.Text = oku["kartno"].ToString();
                        label14.Text = oku["BorcDurumu"].ToString();
                        label17.Text = oku["KullanilabilirBakiye"].ToString();
                    }

                    oku.Close();
                }
            }
            baglanti.Close();

            string kartNumarasi = randomnumara();

            label9.Text = kartNumarasi.ToString();
        }
        private string randomnumara()
        {
            Random random = new Random();
            long numara = ((long)random.Next(0, int.MaxValue) << 32) | (long)random.Next(0, int.MaxValue);
            return numara.ToString().Substring(0, 16);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParaTransferi gonder = new ParaTransferi();

            gonder.Show();

            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HesapHareketleri y1 = new HesapHareketleri();

            y1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BorcOde y5 = new BorcOde();

            y5.Show();

            this.Hide();
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
    }
}

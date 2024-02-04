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
    public partial class Hesap : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public Hesap()
        {
            InitializeComponent();
        }

        private void Hesap_Load(object sender, EventArgs e)
        {
            baglanti.Open();

            string sorgu = "SELECT * FROM musteri WHERE hno = @hno";
            using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@hno", giris.hno);

                using (SqlDataReader dr = komut.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        label8.Text = dr["adsoyad"].ToString();
                        label7.Text = dr["hno"].ToString();
                        label5.Text = dr["katilimtarih"].ToString();


                        string resimAdi = dr["resimad"].ToString();

                        if (!string.IsNullOrEmpty(resimAdi))
                        {
                            baglanti.Close();

                            using (SqlCommand resimKomut = new SqlCommand("SELECT resim FROM musteri WHERE resimad = @p1", baglanti))
                            {
                                baglanti.Open();
                                resimKomut.Parameters.AddWithValue("@p1", resimAdi);

                                using (SqlDataReader resimDr = resimKomut.ExecuteReader())
                                {
                                    if (resimDr.Read())
                                    {
                                        byte[] resimBytes = (byte[])resimDr["resim"];
                                        MemoryStream ms = new MemoryStream(resimBytes);
                                        pictureBox1.Image = Image.FromStream(ms);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            baglanti.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            anasayfa ana = new anasayfa();
            ana.Show();
            this.Hide();
        }
    }
}

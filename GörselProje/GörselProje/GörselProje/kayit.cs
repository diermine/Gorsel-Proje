using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GörselProje
{
    public partial class kayit : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public kayit()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            YöneticiMain yönet = new YöneticiMain();
            yönet.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False"))
            {
                
                    baglanti.Open();

                    SqlCommand komut = new SqlCommand("INSERT INTO musteri (tc, adsoyad, bakiye, yetki, kart, katilimtarih, parola, resimad, resim,mail) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9,@p10)", baglanti);

                    komut.Parameters.AddWithValue("@p1", textBox1.Text);
                    komut.Parameters.AddWithValue("@p2", textBox2.Text);
                    komut.Parameters.AddWithValue("@p3", textBox3.Text);
                    komut.Parameters.AddWithValue("@p7", textBox4.Text);
                    komut.Parameters.AddWithValue("@p5", "yok");
                    komut.Parameters.AddWithValue("@p6", DateTime.Now);
                    komut.Parameters.AddWithValue("@p10", textBox6.Text);


                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || pictureBox2.Image == null)
                    {
                        MessageBox.Show("Lütfen Tüm Alanları Doldurunuz ve Resim Ekleyiniz!", "Eksik Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (radioButton1.Checked)
                    {
                        komut.Parameters.AddWithValue("@p4", "admin");
                    }
                    else if (radioButton2.Checked)
                    {
                        komut.Parameters.AddWithValue("@p4", "musteri");
                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir yetki seçiniz!", "Yetki Seçimi Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    komut.Parameters.AddWithValue("@p8", textBox5.Text);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                        komut.Parameters.AddWithValue("@p9", ms.ToArray());
                    }

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Yeni Müşteri Eklendi Hayırlı Olsun", "Müşteri Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    string tcToDelete = textBox1.Text;

                    // İstek veritabanındaki ilgili satırı sil
                    string deleteQuery = "DELETE FROM istek WHERE tc = @tc";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, baglanti))
                    {
                        deleteCommand.Parameters.AddWithValue("@tc", tcToDelete);
                        deleteCommand.ExecuteNonQuery();
                    }

                    
                    LoadData();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    pictureBox2.Image = null;
                
            }
        }
        private void LoadData()
        {
            
            string sorgu = "SELECT * FROM istek";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sorgu, baglanti);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void kayit_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            string sorgu = "SELECT * FROM istek";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sorgu, baglanti);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
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
                MessageBox.Show("Bakiye sadece rakam içermelidir.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            int bakiye;
            if (int.TryParse(textBox3.Text + e.KeyChar, out bakiye) && bakiye > 15000)
            {
                e.Handled = true;
                MessageBox.Show("Bakiye 15000'den büyük olamaz.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

            if (textBox4.Text.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Parola 6 rakamdan fazla olamaz.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["adsoyad"].Value.ToString();
                textBox1.Text = row.Cells["tc"].Value.ToString();
                textBox4.Text = row.Cells["parola"].Value.ToString();
                textBox5.Text = row.Cells["resimad"].Value.ToString();
                textBox6.Text = row.Cells["mail"].Value.ToString();

                byte[] resimBytes = (byte[])row.Cells["resim"].Value;

                if (resimBytes != null && resimBytes.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(resimBytes);
                    pictureBox2.Image = Image.FromStream(ms);
                }
                else
                {
                    pictureBox2.Image = null;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

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
    }
}
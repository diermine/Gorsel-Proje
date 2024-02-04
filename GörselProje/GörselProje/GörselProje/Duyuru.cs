using System;
using System.Collections;
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
    public partial class Duyuru : Form
    {
        private SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public Duyuru()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged;
            textBox2.TextChanged += textBox2_TextChanged;
            this.Load += Duyuru_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            label4.ForeColor = (textBox1.Text.Length > 40 || ContainsSpecialCharacters(textBox1.Text)) ? System.Drawing.Color.Red : System.Drawing.Color.White;

            
            label6.ForeColor = (textBox2.Text.Length > 500) ? System.Drawing.Color.Red : System.Drawing.Color.Red;

            
            if (label4.ForeColor == System.Drawing.Color.White && label6.ForeColor == System.Drawing.Color.Red)
            {
                InsertDuyuru(textBox1.Text, textBox2.Text);
            }
            else
            {
                MessageBox.Show("Hatalı giriş! Lütfen kontrol ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            label4.ForeColor = (textBox1.Text.Length > 40 || ContainsSpecialCharacters(textBox1.Text)) ? System.Drawing.Color.Red : System.Drawing.Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 500)
            {
                textBox2.Text = textBox2.Text.Substring(0, 500);
                textBox2.SelectionStart = textBox2.Text.Length;
            }

            int remainingCharacters = 500 - textBox2.Text.Length;

            label6.Text = remainingCharacters.ToString();
            label5.ForeColor = (remainingCharacters == 0) ? Color.Red : Color.White;
        }

        private bool ContainsSpecialCharacters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void InsertDuyuru(string baslik, string icerik)
        {
            try
            {
                baglanti.Open();

                string query = "INSERT INTO duyuru (dbaslik, duyuru) VALUES (@Baslik, @Icerik)";
                using (SqlCommand komut = new SqlCommand(query, baglanti))
                {
                    komut.Parameters.AddWithValue("@Baslik", baslik);
                    komut.Parameters.AddWithValue("@Icerik", icerik);

                    komut.ExecuteNonQuery();

                    MessageBox.Show("Duyuru başarıyla eklendi!");
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

        private void Duyuru_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            YöneticiMain main = new YöneticiMain();
            main.Show();
            this.Hide();
        }
    }
}
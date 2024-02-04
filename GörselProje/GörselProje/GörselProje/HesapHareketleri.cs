using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProje
{
    public partial class HesapHareketleri : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

        public HesapHareketleri()
        {
            InitializeComponent();
        }


        private void HesapHareketleri_Load(object sender, EventArgs e)
        {
            label20.Text = giris.ad;
            label22.Text = giris.hno.ToString();
            label3.Text = giris.ad;
            label4.Text = giris.hno.ToString();

            SqlCommand komut = new SqlCommand("select * from hareketler where mid= @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", giris.hno);
            SqlDataAdapter dr = new SqlDataAdapter(komut);

            DataTable tablo = new DataTable();
            dr.Fill(tablo);
            dataGridView1.DataSource = tablo;

            groupBox1.Visible = false;


            dataGridView1.Columns["islemler"].Width = 280;
            dataGridView1.Columns["id"].Width = 120;
            dataGridView1.Columns["mid"].Width = 120;
            dataGridView1.Columns["tarih"].Width = 150;



            
            dataGridView1.Columns["id"].HeaderText = "İşlem Sırası";
            dataGridView1.Columns["mid"].HeaderText = "Hesap No";
            dataGridView1.Columns["islemler"].HeaderText = "İşlemler";
            dataGridView1.Columns["tarih"].HeaderText = "İşlem Tarihi";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
        }


        private void button5_Click_1(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                printDocument.Print();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            float startX = 10;
            float startY = 10;
            float offset = 10;
            int rowHeight = dataGridView1.RowTemplate.Height;
            int headerHeight = 30;

           
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                e.Graphics.DrawString(dataGridView1.Columns[i].HeaderText, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, startX, startY);
                startX += dataGridView1.Columns[i].Width;
            }

            startY += headerHeight;

           
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                startX = 10; 

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                      
                        string cellValue = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        if (!string.IsNullOrEmpty(cellValue.Trim()))
                        {
                            e.Graphics.DrawString(cellValue, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, startX, startY);
                        }
                    }

                    startX += dataGridView1.Columns[j].Width;
                }

             
                startY += rowHeight + offset;
            }


            startX = 10;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.SeaGreen, 2), startX, 10, startX, startY);
                startX += dataGridView1.Columns[i].Width;
            }

            startY = 10;
            for (int i = 0; i <= dataGridView1.Rows.Count; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.SeaGreen, 2), 10, startY, startX, startY);
                if (i < dataGridView1.Rows.Count)
                    startY += rowHeight + offset;
            }
        }
    }
}

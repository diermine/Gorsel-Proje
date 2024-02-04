using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProje
{
    public partial class YöneticiMain : Form
    {
        public YöneticiMain()
        {
            InitializeComponent();
        }

        private void YöneticiMain_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Duyuru y2 = new Duyuru();
            y2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kayit kay = new kayit();
            kay.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MüsteriAra ara = new MüsteriAra();
            ara.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MusteriGuncelle guncelle = new MusteriGuncelle();
            guncelle.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MusteriSil sil = new MusteriSil();
            sil.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            giris giris = new giris();
            giris.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MusteriListele listele = new MusteriListele();
            listele.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MailGonder mg = new MailGonder();
            mg.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }
    }
}

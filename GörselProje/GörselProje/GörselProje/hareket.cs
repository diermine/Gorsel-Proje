using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GörselProje
{
    internal class hareket
    {
        public static void kaydet(int mid, string islemler)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=BARAN;Initial Catalog=prbanka;Integrated Security=True;Encrypt=False");

            SqlCommand komut = new SqlCommand(" insert into hareketler (mid, islemler, tarih ) values (@p1, @p2, @p3)  ", baglanti);
            baglanti.Open();
            komut.Parameters.AddWithValue("@p1", mid);
            komut.Parameters.AddWithValue("@p2", islemler);
            komut.Parameters.AddWithValue("@p3", DateTime.Now);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}

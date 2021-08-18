using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplık
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        OleDbConnection bağlantı = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Metin\Desktop\Kitaplık.mdb");
        string durum = "";

        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * From Kitaplar", bağlantı);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void kaydet()
        {
            bağlantı.Open();
            OleDbCommand komut1 = new OleDbCommand("insert into kitaplar(KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", bağlantı);
            komut1.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut1.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komut1.Parameters.AddWithValue("@p3", CmdTür.Text);
            komut1.Parameters.AddWithValue("@p4", TxtSayfa.Text);
            komut1.Parameters.AddWithValue("@p5", durum);
            komut1.ExecuteNonQuery();
            bağlantı.Close();
            MessageBox.Show("Kitap Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        void sil()
        {
            bağlantı.Open();
            OleDbCommand komut2 = new OleDbCommand("Delete From Kitaplar Where Kitapİd=@p1", bağlantı);
            komut2.Parameters.AddWithValue("@p1", Txtİd.Text);
            komut2.ExecuteNonQuery();
            bağlantı.Close();
            MessageBox.Show("Kitap Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        void güncelle()
        {
            bağlantı.Open();
            OleDbCommand komut3 = new OleDbCommand("Update Kitaplar set KitapAd=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where Kitapİd=@p6", bağlantı);
            komut3.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut3.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komut3.Parameters.AddWithValue("@p3", CmdTür.Text);
            komut3.Parameters.AddWithValue("@p4", TxtSayfa.Text);
            if (radioButton1.Checked == true)
            {
                komut3.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked == true)
            {
                komut3.Parameters.AddWithValue("@p5", durum);
            }
            komut3.Parameters.AddWithValue("@p6", Txtİd.Text);
            komut3.ExecuteNonQuery();
            bağlantı.Close();
            MessageBox.Show("Kitap Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //listele();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            kaydet();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            sil();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            güncelle();
        }

        private void BtnÇıkış_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtİd.Text = dataGridView1.Rows[seçilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[seçilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[seçilen].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[seçilen].Cells[4].Value.ToString();
            CmdTür.Text = dataGridView1.Rows[seçilen].Cells[3].Value.ToString();
            if (dataGridView1.Rows[seçilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void BtnArama_Click(object sender, EventArgs e)
        {
            //Kitabun Tam adını yazıp arama yapma
            /*
            OleDbCommand komut4 = new OleDbCommand("Select * From Kitaplar Where KitapAd=@p1", bağlantı);
            komut4.Parameters.AddWithValue("@p1", TxtArama.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut4);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            */

            OleDbCommand komut4 = new OleDbCommand("Select * From Kitaplar Where KitapAd like '%" + TxtArama.Text + "%'", bağlantı);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut4);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            /*
            OleDbCommand komut4 = new OleDbCommand("Select * From Kitaplar Where KitapAd like '%" + TxtArama.Text + "%'", bağlantı);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut4);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            */
        }
    }
}

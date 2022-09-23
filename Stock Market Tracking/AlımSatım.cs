using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;

namespace Stock_Market_Tracking
{
    public partial class AlımSatım : Form
    {
        public MainMenü main;
        public AlımSatım()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Doğukan Durna\Desktop\YAZILIM\Windows Masaüstü Uygulamaları\Stock Market Tracking\packages\CüzdanVeAlımSatım.xlsx;Extended Properties='Excel 12.0 Xml; HDR = YES';");
        private void label11_Click(object sender, EventArgs e) // App kapatma
        {
            DialogResult sonuc = MessageBox.Show("Programı Kapatmak İstediğinizden Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e) // EKLEME İŞLEMİ
        {
            if ((comboBox1.Text == "") || (dateTimePicker1.Text == "") || (textBox1.Text == "") || (textBox2.Text == "") || (comboBox2.Text == "")) // EĞER TEXTBOXLAR BOŞSA UYARI VER
            {
                MessageBox.Show("Lütfen alanları eksiksiz doldurunuz! Alış Fiyatı ve Adetini lütfen sayı şekinde giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                baglanti.Open();
                OleDbCommand kaydetkomut = new OleDbCommand("INSERT INTO [Sayfa1$] (ID,COIN_ADI,TARIH,ADET,FIYAT,DURUM,TÜR,TOPLAM_TUTAR) values (@p6,@p1,@p2,@p3,@p4,@p5,@p7,@p8)", baglanti);
                int son = dataGridView1.Rows.Count;
                if (comboBox2.SelectedIndex == 1) // EĞER SATIM SEÇİLMİŞSE
                {
                    OleDbCommand kontrol = new OleDbCommand("SELECT [Miktar] FROM [Cüzdan$] where Ad='" + comboBox1.SelectedItem + "'", baglanti); // CÜZDANDA MİKTAR KONTROLÜ YAPIYORUM
                    OleDbDataReader reader = kontrol.ExecuteReader();
                    reader.Read(); // DEĞERİ OKUMASINI SAĞLA
                    float cüzdandakimiktar = float.Parse(reader.GetValue(0).ToString()); // İŞLEM KONTROLÜ YAPMAK İÇİN DEĞİŞKENE ATA
                    if (cüzdandakimiktar - float.Parse(textBox1.Text) < 0) // EĞER CÜZDANDAKİ MİKTARDAN FAZLA GİRİLDİYSE
                    {
                        MessageBox.Show("Cüzdanında bu kadar miktar bulunmamakta!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                    else // EĞER CÜZDANDAKİ MİKTARA UYGUN GİRİLDİYSE
                    {
                        if (son == 0)
                        {
                            kaydetkomut.Parameters.AddWithValue("@p6", 1);
                        }
                        else
                        {
                            kaydetkomut.Parameters.AddWithValue("@p6", Convert.ToInt32(dataGridView1.Rows[son - 1].Cells[0].Value) + 1);
                        }
                        kaydetkomut.Parameters.AddWithValue("@p1", comboBox1.SelectedItem);
                        kaydetkomut.Parameters.AddWithValue("@p2", dateTimePicker1.Text);
                        kaydetkomut.Parameters.AddWithValue("@p3", Convert.ToDouble(textBox1.Text) * -1);
                        kaydetkomut.Parameters.AddWithValue("@p4", Convert.ToDouble(textBox2.Text) * -1);
                        kaydetkomut.Parameters.AddWithValue("@p5", 1);
                        kaydetkomut.Parameters.AddWithValue("@p7", comboBox2.SelectedItem);
                        kaydetkomut.Parameters.AddWithValue("@p8", (float.Parse(textBox1.Text) * float.Parse(textBox2.Text)) * -1);
                        kaydetkomut.ExecuteNonQuery();
                        OleDbCommand CüzdanUpdate = new OleDbCommand("UPDATE [Cüzdan$] SET Miktar=Miktar-'" + float.Parse(textBox1.Text) + "' where Ad='" + comboBox1.SelectedItem + "' ", baglanti);
                        CüzdanUpdate.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);// BUNU YUKARIYA ALACAN
                        VeriListele();
                    }
                }
                else // EĞER ALIM SEÇİLMİŞSE
                {
                    if (son == 0) // EĞER HİÇ VERİ YOKSA
                    {
                        kaydetkomut.Parameters.AddWithValue("@p6", 1);
                    }
                    else // VERİ VARSA SON VERİNİN IDSİNE 1 EKLE
                    {
                        kaydetkomut.Parameters.AddWithValue("@p6", Convert.ToInt32(dataGridView1.Rows[son - 1].Cells[0].Value) + 1);
                    }
                    kaydetkomut.Parameters.AddWithValue("@p1", comboBox1.SelectedItem);
                    kaydetkomut.Parameters.AddWithValue("@p2", dateTimePicker1.Text);
                    kaydetkomut.Parameters.AddWithValue("@p3", Convert.ToDouble(textBox1.Text));
                    kaydetkomut.Parameters.AddWithValue("@p4", Convert.ToDouble(textBox2.Text));
                    kaydetkomut.Parameters.AddWithValue("@p5", 1);
                    kaydetkomut.Parameters.AddWithValue("@p7", comboBox2.SelectedItem);
                    kaydetkomut.Parameters.AddWithValue("@p8", (float.Parse(textBox1.Text) * float.Parse(textBox2.Text)));
                    kaydetkomut.ExecuteNonQuery();
                    OleDbCommand CüzdanUpdate = new OleDbCommand("UPDATE [Cüzdan$] SET Miktar=Miktar+'" + float.Parse(textBox1.Text) + "' where Ad='" + comboBox1.SelectedItem + "'", baglanti);
                    CüzdanUpdate.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);// BUNU YUKARIYA ALACAN
                    VeriListele();
                }
                Cüzdan.ortalamaad.Clear(); // TEMİZLİYORUM ÇÜNKÜ KRİPTOLARDAKİ SAYFAYI GÜNCELLEMEK İÇİN YOKSA GÜNCELLENMİYOR
                Cüzdan.ortalamalar.Clear();
                Cüzdan.adetmiktar.Clear();
            }
        }

        private void AlımSatım_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.FormBackgroundColor; // Form Yüklenirken Arkaplanı memory kısmından çekiyor
            foreach (var item in Kriptolar.isimler) // COMBOBAXA COİN İSİMLERİNİ GETİRİYOR
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
            DateTime bugüntarih = DateTime.Today;
            dateTimePicker1.Value = bugüntarih; // BUGÜNÜN TARİHİNİ DATETİMEPİCKERA ATIYORUM Kİ GÜNCEL GÖZÜKSÜN
            VeriListele();
            SıralamaKapat();
        } // SADECE COMBOXXA COİN İSİMLERİNİ ATMA VE BUGÜNÜN TARİHİNİ ALMA VAR

        void VeriListele() // EXCELDEN VERİLERİ ÇEKME
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT [ID],[COIN_ADI],[TARIH],[ADET],[FIYAT],[TÜR],[TOPLAM_TUTAR] FROM [Sayfa1$] where DURUM=1", baglanti);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) // ALINAN ADET KISMINA SADECE SAYI YAZMA
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) // FİYAT KISMINA SADECE SAYI YAZMA
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)  && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e) // SİLME BUTTONU
        {
            if ((comboBox1.Text == "") || (dateTimePicker1.Text == "") || (textBox1.Text == "") || (textBox2.Text == "") || (comboBox2.Text == "")) // EĞER TEXTBOXLAR BOŞSA UYARI VER
            {
                MessageBox.Show("Lütfen silinecek öğeyi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // DEĞİLSE SİL
            {
                baglanti.Open();
                OleDbCommand günkomut = new OleDbCommand("UPDATE [Sayfa1$] SET COIN_ADI=@p1,TARIH=@p2,ADET=@p3,FIYAT=@p4,DURUM=@p5 WHERE ID=@p6", baglanti);
                günkomut.Parameters.AddWithValue("@p1", comboBox1.SelectedItem);
                günkomut.Parameters.AddWithValue("@p2", dateTimePicker1.Text);
                günkomut.Parameters.AddWithValue("@p3", textBox1.Text);
                günkomut.Parameters.AddWithValue("@p4", textBox2.Text);
                günkomut.Parameters.AddWithValue("@p5", 0); // DURUMU SIFIR YAPIYORUM VE SORGUDA ÇEKERKEN GELMESİNİ ENGELLİYORUM
                günkomut.Parameters.AddWithValue("@p6", textBox3.Text);
                günkomut.ExecuteNonQuery();
                if (comboBox2.SelectedIndex==1)
                {
                    OleDbCommand CüzdanUpdate = new OleDbCommand("UPDATE [Cüzdan$] SET Miktar=Miktar+'" + float.Parse(textBox1.Text) + "' where Ad='" + comboBox1.SelectedItem + "'", baglanti);
                    CüzdanUpdate.ExecuteNonQuery(); // EĞER DEĞERLER YANLIŞ GİRİLİP SİLİNMEK İSTERSE O FAZLALIK KISMI SİLİYORUZ
                    baglanti.Close();
                }
                else
                {
                    OleDbCommand CüzdanUpdate = new OleDbCommand("UPDATE [Cüzdan$] SET Miktar=Miktar-'" + float.Parse(textBox1.Text) + "' where Ad='" + comboBox1.SelectedItem + "'", baglanti);
                    CüzdanUpdate.ExecuteNonQuery(); // EĞER DEĞERLER YANLIŞ GİRİLİP SİLİNMEK İSTERSE O FAZLALIK KISMI SİLİYORUZ
                    baglanti.Close();
                }
                MessageBox.Show("Başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VeriListele();
                Cüzdan.ortalamaad.Clear(); // TEMİZLİYORUM ÇÜNKÜ KRİPTOLARDAKİ SAYFAYI GÜNCELLEMEK İÇİN YOKSA GÜNCELLENMİYOR
                Cüzdan.ortalamalar.Clear();
                Cüzdan.adetmiktar.Clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // TIKLANILAN HERHANGİ SATIRIN DEĞERLERİNİ ÜST TARAFA AKTARMA
        {
            int row = dataGridView1.RowCount;
            if (row != 0)
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                comboBox1.SelectedItem = dataGridView1.Rows[secilen].Cells[1].Value;
                dateTimePicker1.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString().Replace("-", "");
                textBox2.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString().Replace("-", "");
                comboBox2.SelectedItem = dataGridView1.Rows[secilen].Cells[5].Value;
                textBox3.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            }
        }

        private void label5_Click(object sender, EventArgs e) // App alta atma
        {
            main.WindowState = FormWindowState.Minimized;
        }

        void SıralamaKapat()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        } // TABLONUN SIRALAMASINI KAPATIYOR
    }
}
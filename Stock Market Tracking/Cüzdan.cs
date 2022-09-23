using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Market_Tracking
{
    public partial class Cüzdan : Form
    {
        public MainMenü main;
        static public List<string> ortalamaad = new List<string>();
        static public List<string> ortalamalar = new List<string>();
        static public List<string> adetmiktar = new List<string>();

        public Cüzdan()
        {
            InitializeComponent();
        }
        // oledb bir system özelliğidir bağlantı kurmak için bağlantı objesi oluşturma ve dosyaya bağlanma
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Doğukan Durna\Desktop\YAZILIM\Windows Masaüstü Uygulamaları\Stock Market Tracking\packages\CüzdanVeAlımSatım.xlsx;Extended Properties='Excel 12.0 Xml; HDR = YES';");
        private void label11_Click(object sender, EventArgs e) // X basınca app kapanma
        {
            DialogResult sonuc = MessageBox.Show("Programı Kapatmak İstediğinizden Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        void VeriListele() // Bağlantı kurup verileri tabloya listeleme
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT COIN_ADI,(SUM(TOPLAM_TUTAR) / SUM(ADET)) as Ortalama,SUM(ADET) as Miktar FROM [Sayfa1$] where DURUM=1 GROUP BY COIN_ADI", baglanti); // Tablodaki AD sütunu ve Adet toplamını adların hepsini çekmek için kullan
            DataTable dt = new DataTable(); // veri öğesi oluşturma
            dataAdapter.Fill(dt); // veri öğesini aktarma
            dataGridView1.DataSource = dt; // listeleme
        }

        private void Cüzdan_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.FormBackgroundColor; // Form Yüklenirken Arkaplanı memory kısmından çekiyor
            VeriListele();
            SıralamaKapat();
            NotListele();
            ListeyeAktar();
        }

        void NotListele() // Notu Listeleme Fonksiyonu
        {
            string[] Başlangıç = new string[richTextBox1.Lines.Length];
            Başlangıç = System.IO.File.ReadAllLines(@"C:\Users\Doğukan Durna\Desktop\YAZILIM\Windows Masaüstü Uygulamaları\Stock Market Tracking\packages\Notes.dat");
            richTextBox1.Lines = Başlangıç;
        }

        void NotKaydet() // Not Kaydetme Fonksiyonu
        {
            string Dosya_Yolu = @"C:\Users\Doğukan Durna\Desktop\YAZILIM\Windows Masaüstü Uygulamaları\Stock Market Tracking\packages\Notes.dat"; // Dosya Yoluunu belirleme
            string[] Kayıt_Dizisi = new string[richTextBox1.Lines.Length]; // Verileri diziye atma
            richTextBox1.Lines.CopyTo(Kayıt_Dizisi, 0); // Verileri richten kopyalama
            System.IO.File.WriteAllLines(Dosya_Yolu,Kayıt_Dizisi); // Verileri Dosyaya yazma
        }

        void ListeyeAktar() // Değerleri tabloya aktarma
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ortalamaad.Add(dataGridView1.Rows[i].Cells[0].Value.ToString()); // HER ADDAN 1 TANE GELİYOR ÇÜNKÜ TABLO ZATEN ÖYLE OLUŞUYOR
                ortalamalar.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                adetmiktar.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e) // Notu kaydetme
        {
            NotKaydet();
        }

        private void button2_Click(object sender, EventArgs e) // Not temizlemeye basınca emin misin diye sorma ve ona göre temizleyip kaydetme
        {
            DialogResult result=MessageBox.Show("Notu tamamen temizlemek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                richTextBox1.Clear();
                NotKaydet();
            }
        }

        private void label3_Click(object sender, EventArgs e) // App alta atma
        {
            main.WindowState = FormWindowState.Minimized;
        }

        void SıralamaKapat() // Tabloda sıralamayı kapatmayı sağlıyor
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
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
using System.Runtime.InteropServices;
using System.Net;
using HtmlAgilityPack;
using System.Threading;
using System.IO;

namespace Stock_Market_Tracking
{
    public partial class Kriptolar : Form
    {
        public MainMenü main;
        static public List<string> isimler = new List<string>();
        List<string> sabitparalar = new List<string>();
        List<string> hareketliparalar = new List<string>();
        public Kriptolar()
        {
            InitializeComponent();
        }

        private void Kriptolar_Load(object sender, EventArgs e) // İSİMLERİN ÇEKİLMESİ VE DEĞERLERİN ÇEKİLMESİ VE BUNLARIN COMBABOXA AKTARILMASI
        {
            Cüzdan cüzdan = new Cüzdan();
            cüzdan.Visible = false;
            cüzdan.Show();
            cüzdan.Close();
            this.BackColor = Properties.Settings.Default.FormBackgroundColor; // Form Yüklenirken Arkaplanı memory kısmından çekiyor
            label1.ForeColor = Properties.Settings.Default.TextColor;
            label2.ForeColor = Properties.Settings.Default.TextColor;
            label3.ForeColor = Properties.Settings.Default.TextColor;
            label4.ForeColor = Properties.Settings.Default.TextColor;
            label5.ForeColor = Properties.Settings.Default.TextColor;
            label6.ForeColor = Properties.Settings.Default.TextColor;
            label7.ForeColor = Properties.Settings.Default.TextColor;
            label8.ForeColor = Properties.Settings.Default.TextColor;
            label9.ForeColor = Properties.Settings.Default.TextColor;
            label10.ForeColor = Properties.Settings.Default.TextColor;
            //Bağlantı kurma ve çekme
            timer1.Start();
            SabitCekListEkle();

            foreach (var item in isimler) // GELEN COİN İSİMLERİNİ COMBOBOXLARA AKTARMA
            {
                comboBox1.Items.Add(item);
                comboBox2.Items.Add(item);
                comboBox3.Items.Add(item);
                comboBox4.Items.Add(item);
                comboBox5.Items.Add(item);
                comboBox6.Items.Add(item);
                comboBox7.Items.Add(item);
                comboBox8.Items.Add(item);
                comboBox9.Items.Add(item);
                comboBox10.Items.Add(item);
            }

            for (int i = 0; i < 10; i++) // TABLOYA İSİMLERİ VE SABİT PARAYI EKLEME
            {
                bunifuDataGridView1.Rows.Add(isimler[i]);
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 3;
            comboBox5.SelectedIndex = 4;
            comboBox6.SelectedIndex = 5;
            comboBox7.SelectedIndex = 6;
            comboBox8.SelectedIndex = 7;
            comboBox9.SelectedIndex = 8;
            comboBox10.SelectedIndex = 9;
            label1.Text = sabitparalar[0];
            label2.Text = sabitparalar[1];
            label3.Text = sabitparalar[2];
            label4.Text = sabitparalar[3];
            label5.Text = sabitparalar[4];
            label6.Text = sabitparalar[5];
            label7.Text = sabitparalar[6];
            label8.Text = sabitparalar[7];
            label9.Text = sabitparalar[8];
            label10.Text = sabitparalar[9];
        }

        void SabitCekListEkle() // SABİT İSİM VE PARALARIN ÇEKİLMESİ FONKSİYONU
        {
            WebClient client = new WebClient();
            string htmlstr = client.DownloadString("https://www.coinmarketcal.com/en/coin-ranking?page=1&orderBy=&exchanges%5B%5D=binance&show_all=false");

            HtmlAgilityPack.HtmlDocument htmlbelgesi = new HtmlAgilityPack.HtmlDocument();

            htmlbelgesi.OptionFixNestedTags = true;

            htmlbelgesi.LoadHtml(htmlstr);

            HtmlAgilityPack.HtmlNodeCollection sayfa1isimler = htmlbelgesi.DocumentNode.SelectNodes("//*[@id='coin-list-wrapper']/tbody/tr[*]/td[3]/div/a");
            HtmlAgilityPack.HtmlNodeCollection sayfa1değerler = htmlbelgesi.DocumentNode.SelectNodes("//*[@id='coin-list-wrapper']/tbody/tr[*]/td[4]/div[1]");

            for (int i = 0; i < sayfa1isimler.Count; i++)
            {
                isimler.Add(sayfa1isimler[i].InnerText);
                sabitparalar.Add(sayfa1değerler[i].InnerText);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // HAREKETLİ PARALARIN ÇEKİLMESİ
        {
            timer1.Interval = 5000; //5sn
            WebClient client = new WebClient();
            string htmlstr = client.DownloadString("https://www.coinmarketcal.com/en/coin-ranking?page=1&orderBy=&exchanges%5B%5D=binance&show_all=false");
            HtmlAgilityPack.HtmlDocument htmlbelgesi = new HtmlAgilityPack.HtmlDocument();
            htmlbelgesi.OptionFixNestedTags = true;
            htmlbelgesi.LoadHtml(htmlstr);
            HtmlAgilityPack.HtmlNodeCollection hareketliparalar1html = htmlbelgesi.DocumentNode.SelectNodes("//*[@id='coin-list-wrapper']/tbody/tr[*]/td[4]/div[1]");
            for (int i = 0; i < hareketliparalar1html.Count; i++)
            {
                hareketliparalar.Add(hareketliparalar1html[i].InnerText);
            }

            int selected1 = comboBox1.SelectedIndex;
            int selected2 = comboBox2.SelectedIndex;
            int selected3 = comboBox3.SelectedIndex;
            int selected4 = comboBox4.SelectedIndex;
            int selected5 = comboBox5.SelectedIndex;
            int selected6 = comboBox6.SelectedIndex;
            int selected7 = comboBox7.SelectedIndex;
            int selected8 = comboBox8.SelectedIndex;
            int selected9 = comboBox9.SelectedIndex;
            int selected10 = comboBox10.SelectedIndex;
            bunifuDataGridView1.Rows[0].Cells[3].Value = hareketliparalar[selected1];
            bunifuDataGridView1.Rows[1].Cells[3].Value = hareketliparalar[selected2];
            bunifuDataGridView1.Rows[2].Cells[3].Value = hareketliparalar[selected3];
            bunifuDataGridView1.Rows[3].Cells[3].Value = hareketliparalar[selected4];
            bunifuDataGridView1.Rows[4].Cells[3].Value = hareketliparalar[selected5];
            bunifuDataGridView1.Rows[5].Cells[3].Value = hareketliparalar[selected6];
            bunifuDataGridView1.Rows[6].Cells[3].Value = hareketliparalar[selected7];
            bunifuDataGridView1.Rows[7].Cells[3].Value = hareketliparalar[selected8];
            bunifuDataGridView1.Rows[8].Cells[3].Value = hareketliparalar[selected9];
            bunifuDataGridView1.Rows[9].Cells[3].Value = hareketliparalar[selected10];

            foreach (DataGridViewRow row in bunifuDataGridView1.Rows)
            {
                for (int i = 0; i < 10; i++) // YATIRIM TUTARI KISMI
                {
                    string sonuc = bunifuDataGridView1.Rows[i].Cells[1].Value.ToString().Replace(".", ",");
                    string sonuc2 = bunifuDataGridView1.Rows[i].Cells[2].Value.ToString().Replace(".", ",");
                    string sonuc3 = (float.Parse(sonuc) * float.Parse(sonuc2)).ToString();
                    bunifuDataGridView1.Rows[i].Cells[4].Value = sonuc3;
                }

                for (int k = 0; k < 10; k++) // KAR ZARAR HESAPLAMA KISMI
                {
                    string alışf = bunifuDataGridView1.Rows[k].Cells[1].Value.ToString().Replace(".", ",");
                    string alışa = bunifuDataGridView1.Rows[k].Cells[2].Value.ToString().Replace(".", ",");
                    string anlıkf = bunifuDataGridView1.Rows[k].Cells[3].Value.ToString().Remove(0, 2).Replace(" ", "").Replace(".", ",");
                    string karzarar = (-((float.Parse(alışf) * float.Parse(alışa)) - (float.Parse(alışa) * float.Parse(anlıkf)))).ToString();
                    bunifuDataGridView1.Rows[k].Cells[5].Value = karzarar;
                }
            }
            hareketliparalar.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // COMBOBOXTAN COİN SEÇME
        {
            int box = 1;
            int selected = comboBox1.SelectedIndex;
            ComboBoxChecker(box, selected);
            label1.Text = sabitparalar[selected];
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 2;
            int selected = comboBox2.SelectedIndex;
            ComboBoxChecker(box, selected);
            label2.Text = sabitparalar[selected];
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 3;
            int selected = comboBox3.SelectedIndex;
            ComboBoxChecker(box, selected);
            label3.Text = sabitparalar[selected];
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 4;
            int selected = comboBox4.SelectedIndex;
            ComboBoxChecker(box, selected);
            label4.Text = sabitparalar[selected];
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 5;
            int selected = comboBox5.SelectedIndex;
            ComboBoxChecker(box, selected);
            label5.Text = sabitparalar[selected];
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 6;
            int selected = comboBox6.SelectedIndex;
            ComboBoxChecker(box, selected);
            label6.Text = sabitparalar[selected];
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 7;
            int selected = comboBox7.SelectedIndex;
            ComboBoxChecker(box, selected);
            label7.Text = sabitparalar[selected];
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 8;
            int selected = comboBox8.SelectedIndex;
            ComboBoxChecker(box, selected);
            label8.Text = sabitparalar[selected];
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 9;
            int selected = comboBox9.SelectedIndex;
            ComboBoxChecker(box, selected);
            label9.Text = sabitparalar[selected];
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            int box = 10;
            int selected = comboBox10.SelectedIndex;
            ComboBoxChecker(box, selected);
            label10.Text = sabitparalar[selected];
        }

        private void ComboBoxChecker(int box, int selected) // COMBOBOXTAN SEÇİLEN COİNİN TABLODA GÖSTERİLMESİ YANİ TABLONUN DEĞİŞMESİ
        {
            switch (box)
            {
                case 1:
                    bunifuDataGridView1.Rows[0].Cells[0].Value = comboBox1.Items[selected].ToString();
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[0].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[0].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[0].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[0].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[0].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else // EĞER YOKSA 0 YAZMASINI SAĞLIYOR
                    {
                        bunifuDataGridView1.Rows[0].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[0].Cells[2].Value = 0;
                    }
                    break;
                case 2:
                    bunifuDataGridView1.Rows[1].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[1].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[1].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[1].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[1].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[1].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[1].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[1].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[1].Cells[2].Value = 0;
                    }
                    break;
                case 3:
                    bunifuDataGridView1.Rows[2].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[2].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[2].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[2].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[2].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[2].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[2].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[2].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[2].Cells[2].Value = 0;
                    }
                    break;
                case 4:
                    bunifuDataGridView1.Rows[3].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[3].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[3].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[3].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[3].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[3].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[3].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else // YOKSA 0 KOYMASINI SAĞLIYOR
                    {
                        bunifuDataGridView1.Rows[3].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[3].Cells[2].Value = 0;
                    }
                    break;
                case 5:
                    bunifuDataGridView1.Rows[4].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[4].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[4].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[4].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[4].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[4].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[4].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[4].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[4].Cells[2].Value = 0;
                    }
                    break;
                case 6:
                    bunifuDataGridView1.Rows[5].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[5].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[5].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[5].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[5].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[5].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[5].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[5].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[5].Cells[2].Value = 0;
                    }
                    break;
                case 7:
                    bunifuDataGridView1.Rows[6].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[6].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[6].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[6].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[6].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[6].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[6].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[6].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[6].Cells[2].Value = 0;
                    }
                    break;
                case 8:
                    bunifuDataGridView1.Rows[7].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[7].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[7].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[7].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[7].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[7].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[7].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[7].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[7].Cells[2].Value = 0;
                    }
                    break;
                case 9:
                    bunifuDataGridView1.Rows[8].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[8].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[8].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[8].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[8].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[8].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[8].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[8].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[8].Cells[2].Value = 0;
                    }
                    break;
                case 10:
                    bunifuDataGridView1.Rows[9].Cells[0].Value = comboBox1.Items[selected].ToString();
                    bunifuDataGridView1.Rows[9].Cells[1].Value = sabitparalar[selected].Replace(" ", "").Replace("$", "");
                    if (Cüzdan.ortalamaad.Contains(bunifuDataGridView1.Rows[9].Cells[0].Value)) // seçip aynı indexten değiştirme
                    {
                        //int arananindex = Cüzdan.ortalamaad.BinarySearch(bunifuDataGridView1.Rows[9].Cells[0].Value.ToString());
                        int arananindex = Cüzdan.ortalamaad.IndexOf(bunifuDataGridView1.Rows[9].Cells[0].Value.ToString());
                        bunifuDataGridView1.Rows[9].Cells[1].Value = Cüzdan.ortalamalar[arananindex];
                        bunifuDataGridView1.Rows[9].Cells[2].Value = Cüzdan.adetmiktar[arananindex];
                    }
                    else
                    {
                        bunifuDataGridView1.Rows[9].Cells[1].Value = 0;
                        bunifuDataGridView1.Rows[9].Cells[2].Value = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        private void label11_Click(object sender, EventArgs e) // X e basınca app kapanıyor
        {
            DialogResult sonuc = MessageBox.Show("Programı Kapatmak İstediğinizden Emin misiniz?","Uyarı",MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Kriptolar_Leave(object sender, EventArgs e) // Bu form kapatılıp başka form açıldığında çekmeyi durdurmak için
        {
            this.timer1.Stop();
        }

        private void label12_Click(object sender, EventArgs e) // _ BASINCA APP AŞŞA ALMA
        {
            main.WindowState = FormWindowState.Minimized;
        }
    }
}
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

namespace Stock_Market_Tracking
{
    public partial class Haberler : Form
    {
        public MainMenü main;
        public List<string> haberurl = new List<string>();
        public string olusturulanurl;
        public string url;
        public string[] haberurldizi;
        public Haberler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Hableri çekme düğmesi
        {
            richTextBox1.Clear();
            bunifuDataGridView1.Rows.Clear();
            haberurl.Clear();
            haberurlolustrma();
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            if (listBox1.Items.Count == 0) // eğer seçilmiş coin yoksa anasayfa haberleri
            {
                url ="https://www.coinmarketcal.com/tr/";
            }
            else // eğer varsa alt taraftan url oluştur ve kullan
            {
                url = olusturulanurl;
            }
            string htmlstr = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument htmlbelgesi = new HtmlAgilityPack.HtmlDocument();
            htmlbelgesi.OptionFixNestedTags = true;
            htmlbelgesi.LoadHtml(htmlstr);
            HtmlAgilityPack.HtmlNodeCollection başlıklarhtml = htmlbelgesi.DocumentNode.SelectNodes("/html/body/main/section[1]/div[2]/div[3]/article[*]/div/div/h5/a");
            HtmlAgilityPack.HtmlNodeCollection habertarihleri = htmlbelgesi.DocumentNode.SelectNodes("/html[1]/body[1]/main[1]/section[1]/div[2]/div[3]/article[*]/div[1]/div[1]/a[1]/h5[1]");
            HtmlAgilityPack.HtmlNodeCollection haberbaşlıklar = htmlbelgesi.DocumentNode.SelectNodes("/html[1]/body[1]/main[1]/section[1]/div[2]/div[3]/article[*]/div[1]/div[1]/a[1]/h5[2]");
            HtmlAgilityPack.HtmlNodeCollection haberiçerikleri = htmlbelgesi.DocumentNode.SelectNodes("/html[1]/body[1]/main[1]/section[1]/div[2]/div[3]/article[*]/div[1]/div[1]/div[2]/p[1]");
            foreach (HtmlAgilityPack.HtmlNode link in htmlbelgesi.DocumentNode.SelectNodes("//article[*]/div/div/a[@href]")) // haberlerin urllerindeki haberin detay linkini listeye atıyor
            {
                HtmlAgilityPack.HtmlAttribute att = link.Attributes["href"];
                if (att.Value.Contains('t'))
                {
                    haberurl.Add("https://www.coinmarketcal.com" + att.Value);
                }
            }
            haberurldizi = haberurl.ToArray();
            for (int i = 0; i < haberurl.Count; i++) // TABLOYA HABERLERİ AKTARMA
            {
                bunifuDataGridView1.Rows.Add(başlıklarhtml[i].InnerText);
                bunifuDataGridView1.Rows[i].Cells[1].Value = habertarihleri[i].InnerText;
                bunifuDataGridView1.Rows[i].Cells[2].Value = haberbaşlıklar[i].InnerText.Replace("&#039;", "'").Replace("&quot;", "");
                bunifuDataGridView1.Rows[i].Cells[3].Value = haberiçerikleri[i].InnerText.Remove(haberiçerikleri[i].InnerText.Length-36,36).Remove(0,45).Replace("&#039;", "'").Replace("&quot;","");
                bunifuDataGridView1.Rows[i].Cells[4].Value = haberurldizi[i];
            }

        }

        private void button2_Click(object sender, EventArgs e) // Haberleri gelicek olan coinleri seçme
        {
            listBox1.Items.Add(comboBox1.SelectedItem);
        }

        private void label11_Click(object sender, EventArgs e) // App kapatma
        {
            DialogResult sonuc = MessageBox.Show("Programı Kapatmak İstediğinizden Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e) // App alta alma
        {
            main.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e) // Tablo ve Listeyi Temizleme
        {
            listBox1.Items.Clear();
            richTextBox1.Clear();
            bunifuDataGridView1.Rows.Clear();
            haberurl.Clear();
            richTextBox2.Clear();
        }

        private void haberurlolustrma()  // Listboxa gelen coin değerlerinden url oluşturma
        {
            int gün = DateTime.Today.Day;
            int ay = DateTime.Today.Month;
            int yil = DateTime.Today.Year;
            int gelecekyil = yil + 2;
            string olusturtarih = gün + "%2F" + ay + "%2F" + yil + "+-+" + gün + "%2F" + ay + "%2F" + gelecekyil;
            string tarihsonrasikisim = "&form%5Bkeyword%5D=&form%5Bcoin%5D%5B%5D=";
            string olusancoinadi = "";
            string ekkısım = "&form%5Bcoin%5D%5B%5D=";
            //"%5Bcoin%5D%5B%5D=&form" bunu son şeye koymuyor ondan öncekilere koyuyor
            string sonkısım = "&form%5Bsort_by%5D=&form%5Bsubmit%5D=";
            string url = "https://www.coinmarketcal.com/tr/?form%5Bdate_range%5D="+olusturtarih+tarihsonrasikisim;
            if (listBox1.Items.Count == 1) // Tek Bir Coin Seçildiyse
            {
                switch (listBox1.Items[0].ToString())
                {
                    case "BNB (BNB)":
                        listBox1.Items[0] = "binance-coin";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Polkadot (DOT)":
                        listBox1.Items[0] = "polkadot-new";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Polygon (MATIC)":
                        listBox1.Items[0] = "matic-network";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "NEAR Protocol (NEAR)":
                        listBox1.Items[0] = "near";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Cosmos Hub (ATOM)":
                        listBox1.Items[0] = "cosmos";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Elrond (EGLD)":
                        listBox1.Items[0] = "elrond-egld";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Hedera (HBAR)":
                        listBox1.Items[0] = "hedera-hashgraph";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Theta Network (THETA)":
                        listBox1.Items[0] = "theta";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Terra (LUNA)":
                        listBox1.Items[0] = "theta";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Pax Dollar (USDP)":
                        listBox1.Items[0] = "paxos-standard";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Gnosis (GNO)":
                        listBox1.Items[0] = "gnosis-gno";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Stacks (STX)":
                        listBox1.Items[0] = "stack";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "yearn.finance(YFI)":
                        listBox1.Items[0] = "yearn-finance";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "Bancor Network Token (BNT)":
                        listBox1.Items[0] = "bancor";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    case "OMG Network(OMG)":
                        listBox1.Items[0] = "bancor";
                        url += listBox1.Items[0].ToString();
                        olusturulanurl = url;
                        break;
                    default:
                        string secilencoin = listBox1.Items[0].ToString();
                        int index = secilencoin.IndexOf("(");
                        int uzunluk = listBox1.Items[0].ToString().Length;
                        int silinecek = (listBox1.Items[0].ToString().Length) - index;// stringin sonundan parantezin indexini çıkarıyorum ki parantezden sonra kaç karakter var o kadar sileyim
                        olusancoinadi = listBox1.Items[0].ToString().ToLower().Remove(index, silinecek);
                        olusancoinadi = olusancoinadi.Remove(olusancoinadi.Length - 1, 1);
                        olusancoinadi = olusancoinadi.Replace(" ", "-").Replace("ı", "i");
                        url += olusancoinadi;
                        olusturulanurl = url;
                        break;
                } // Bazı Coinlerin adı ve urldeki adı farklı olduğu için adları url adına çeviriyorum
            }
            else // Birden fazla coin seçildiyse
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (i == listBox1.Items.Count - 1)
                    {
                        switch (listBox1.Items[i].ToString())
                        {
                            case "BNB (BNB)":
                                listBox1.Items[i] = "binance-coin";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Polkadot (DOT)":
                                listBox1.Items[i] = "polkadot-new";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Polygon (MATIC)":
                                listBox1.Items[i] = "matic-network";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "NEAR Protocol (NEAR)":
                                listBox1.Items[i] = "near";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Cosmos Hub (ATOM)":
                                listBox1.Items[i] = "cosmos";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Elrond (EGLD)":
                                listBox1.Items[i] = "elrond-egld";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Hedera (HBAR)":
                                listBox1.Items[i] = "hedera-hashgraph";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Theta Network (THETA)":
                                listBox1.Items[i] = "theta";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Terra (LUNA)":
                                listBox1.Items[i] = "theta";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Pax Dollar (USDP)":
                                listBox1.Items[i] = "paxos-standard";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Gnosis (GNO)":
                                listBox1.Items[i] = "gnosis-gno";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Stacks (STX)":
                                listBox1.Items[i] = "stack";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "yearn.finance(YFI)":
                                listBox1.Items[i] = "yearn-finance";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "Bancor Network Token (BNT)":
                                listBox1.Items[i] = "bancor";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            case "OMG Network(OMG)":
                                listBox1.Items[i] = "bancor";
                                url += listBox1.Items[i].ToString() + sonkısım;
                                olusturulanurl = url;
                                break;
                            default:
                                string secilencoin = listBox1.Items[i].ToString();
                                int index = secilencoin.IndexOf("(");
                                int uzunluk = listBox1.Items[i].ToString().Length;
                                int silinecek = (listBox1.Items[i].ToString().Length) - index;// stringin sonundan parantezin indexini çıkarıyorum ki parantezden sonra kaç karakter var o kadar sileyim
                                olusancoinadi = listBox1.Items[i].ToString().ToLower().Remove(index, silinecek);
                                olusancoinadi = olusancoinadi.Remove(olusancoinadi.Length - 1, 1);
                                olusancoinadi = olusancoinadi.Replace(" ", "-").Replace("ı", "i");
                                url += olusancoinadi + sonkısım;
                                olusturulanurl = url;
                                break;
                        }
                    }
                    else
                    {
                        switch (listBox1.Items[i].ToString())
                        {
                            case "BNB (BNB)":
                                listBox1.Items[i] = "binance-coin";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Polkadot (DOT)":
                                listBox1.Items[i] = "polkadot-new";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Polygon (MATIC)":
                                listBox1.Items[i] = "matic-network";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "NEAR Protocol (NEAR)":
                                listBox1.Items[i] = "near";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Cosmos Hub (ATOM)":
                                listBox1.Items[i] = "cosmos";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Elrond (EGLD)":
                                listBox1.Items[i] = "elrond-egld";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Hedera (HBAR)":
                                listBox1.Items[i] = "hedera-hashgraph";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Theta Network (THETA)":
                                listBox1.Items[i] = "theta";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Terra (LUNA)":
                                listBox1.Items[i] = "theta";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Pax Dollar (USDP)":
                                listBox1.Items[i] = "paxos-standard";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Gnosis (GNO)":
                                listBox1.Items[i] = "gnosis-gno";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Stacks (STX)":
                                listBox1.Items[i] = "stack";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "yearn.finance(YFI)":
                                listBox1.Items[i] = "yearn-finance";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "Bancor Network Token (BNT)":
                                listBox1.Items[i] = "bancor";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            case "OMG Network(OMG)":
                                listBox1.Items[i] = "bancor";
                                url += listBox1.Items[i].ToString() + ekkısım;
                                break;
                            default:
                                string secilencoin = listBox1.Items[i].ToString();
                                int index = secilencoin.IndexOf("(");
                                int uzunluk = listBox1.Items[i].ToString().Length;
                                int silinecek = (listBox1.Items[i].ToString().Length) - index;// stringin sonundan parantezin indexini çıkarıyorum ki parantezden sonra kaç karakter var o kadar sileyim
                                olusancoinadi = listBox1.Items[i].ToString().ToLower().Remove(index, silinecek);
                                olusancoinadi = olusancoinadi.Remove(olusancoinadi.Length - 1, 1);
                                olusancoinadi = olusancoinadi.Replace(" ", "-").Replace("ı", "i");
                                url += olusancoinadi + ekkısım;
                                break;
                        }
                    }
                }
            }
            richTextBox1.Text = url;
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) // Linke Tıklayınca Browserda açılması
        {
            if (e.ColumnIndex == 4)
            {
                System.Diagnostics.Process.Start(bunifuDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void Haberler_Load(object sender, EventArgs e) // Form Yüklenirken Kriptolardaki isimler listesinin elemanlarının buranın comboboxa atıyorum
        {
            foreach (var item in Kriptolar.isimler)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // Okunmak istenen haberi alt tarafa aktarıyor ve rahat okuma sağlanıyor
        {
            int row = bunifuDataGridView1.RowCount;
            if (row != 0)
            {
                int secilen = bunifuDataGridView1.SelectedCells[0].RowIndex;
                richTextBox2.Text = bunifuDataGridView1.Rows[secilen].Cells[0].Value + " " + bunifuDataGridView1.Rows[secilen].Cells[1].Value + "\n" + bunifuDataGridView1.Rows[secilen].Cells[2].Value.ToString() + "\n" + bunifuDataGridView1.Rows[secilen].Cells[3].Value;
            }
        }
    }
}
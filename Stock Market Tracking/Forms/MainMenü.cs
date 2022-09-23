using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class MainMenü : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
                int left,
                int top,
                int right,
                int buttom,
                int width,
                int height
            );

        public MainMenü()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.FormBackgroundColor; // Form Yüklenirken Arkaplanı memory kısmından çekiyor
            bunifuGradientPanel1.GradientBottomLeft = Properties.Settings.Default.PanelColor; ; // Form Yüklenirken Menü rengini memory kısmından çekiyor
            bunifuGradientPanel1.GradientBottomRight = Properties.Settings.Default.PanelColor; ;
            bunifuGradientPanel1.GradientTopLeft = Properties.Settings.Default.PanelColor; ;
            bunifuGradientPanel1.GradientTopRight = Properties.Settings.Default.PanelColor;;
            bunifuGradientPanel4.GradientBottomLeft = Properties.Settings.Default.PanelColor;;
            bunifuGradientPanel4.GradientBottomRight = Properties.Settings.Default.PanelColor;;
            bunifuGradientPanel4.GradientTopLeft = Properties.Settings.Default.PanelColor;;
            bunifuGradientPanel4.GradientTopRight = Properties.Settings.Default.PanelColor;;
            //KriptoParalarButton.ForeColor = Properties.Settings.Default.TextColor; ; // Form Yüklenirken text rengini memory kısmından çekiyor
            //CüzdanButton.ForeColor = Properties.Settings.Default.TextColor;;
            //AlımSatımButton.ForeColor = Properties.Settings.Default.TextColor;;
            //HaberlerButton.ForeColor = Properties.Settings.Default.TextColor;;
            //SettingsButton.ForeColor = Properties.Settings.Default.TextColor;;
            //CüzdanButton_Click(sender, e);
            KriptoParalarButton_Click(sender,e);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            bunifuGradientPanel3.Controls.Clear();
            Settings ayarlarsayfa = new Settings();
            ayarlarsayfa.main = this; // oradaki maine atıyorum bu formu
            ayarlarsayfa.MdiParent = this;
            ayarlarsayfa.Name = "acıkmı1";
            bunifuGradientPanel3.Controls.Add(ayarlarsayfa);
            ayarlarsayfa.Show();
        }

        private void KriptoParalarButton_Click(object sender, EventArgs e)
        {
            bunifuGradientPanel3.Controls.Clear();
            Kriptolar kriptosayfa = new Kriptolar();
            kriptosayfa.main = this;
            kriptosayfa.MdiParent = this;
            kriptosayfa.Name = "acıkmı";
            bunifuGradientPanel3.Controls.Add(kriptosayfa);
            kriptosayfa.Show();
        }

        private void label11_Click_1(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Programı Kapatmak İstediğinizden Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void CüzdanButton_Click(object sender, EventArgs e)
        {
            bunifuGradientPanel3.Controls.Clear();
            Cüzdan cüzdansayfa = new Cüzdan();
            cüzdansayfa.main = this;
            cüzdansayfa.MdiParent = this;
            cüzdansayfa.Name = "Cüzdan1";
            bunifuGradientPanel3.Controls.Add(cüzdansayfa);
            cüzdansayfa.Show();
        }

        private void AlımSatımButton_Click(object sender, EventArgs e)
        {
            bunifuGradientPanel3.Controls.Clear();
            AlımSatım alımSatımsayfa = new AlımSatım();
            alımSatımsayfa.main = this;
            alımSatımsayfa.MdiParent = this;
            alımSatımsayfa.Name = "Alımsatım1";
            bunifuGradientPanel3.Controls.Add(alımSatımsayfa);
            alımSatımsayfa.Show();
        }

        private void HaberlerButton_Click(object sender, EventArgs e)
        {
            bunifuGradientPanel3.Controls.Clear();
            Haberler haberler = new Haberler();
            haberler.main = this;
            haberler.MdiParent = this;
            haberler.Name = "haberler1";
            bunifuGradientPanel3.Controls.Add(haberler);
            haberler.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

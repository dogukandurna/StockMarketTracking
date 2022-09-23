using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Market_Tracking
{
    public partial class Settings : Form
    {
        public MainMenü main; // MainMenü formunu main adlı objeye atıyorum ki objelerine ulaşabileyim
        public Settings()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e) // X e basınca app kapanıyor
        {
            DialogResult sonuc = MessageBox.Show("Programı Kapatmak İstediğinizden Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Colordlg kullanarak app in memory kısmına form arkaplan renkleri kaydediyor Properties kısmında kaydediliyor
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.FormBackgroundColor = colorDlg.Color;
                Properties.Settings.Default.Save();
                this.BackColor = colorDlg.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e) // Colordlg kullanarak app in memory kısmına menü renkleri kaydediyor Properties kısmında kaydediliyor
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PanelColor = colorDlg.Color;
                Properties.Settings.Default.Save();
                main.bunifuGradientPanel1.GradientBottomLeft = colorDlg.Color;
                main.bunifuGradientPanel1.GradientBottomRight = colorDlg.Color;
                main.bunifuGradientPanel1.GradientTopLeft = colorDlg.Color;
                main.bunifuGradientPanel1.GradientTopRight = colorDlg.Color;
                main.bunifuGradientPanel4.GradientBottomLeft = colorDlg.Color;
                main.bunifuGradientPanel4.GradientBottomRight = colorDlg.Color;
                main.bunifuGradientPanel4.GradientTopLeft = colorDlg.Color;
                main.bunifuGradientPanel4.GradientTopRight = colorDlg.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e) // Colordlg kullanarak app in memory kısmına metin renkleri kaydediyor Properties kısmında kaydediliyor
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.TextColor = colorDlg.Color;
                Properties.Settings.Default.Save();
                main.KriptoParalarButton.ForeColor = colorDlg.Color;
                main.CüzdanButton.ForeColor = colorDlg.Color;
                main.AlımSatımButton.ForeColor = colorDlg.Color;
                main.HaberlerButton.ForeColor = colorDlg.Color;
                main.SettingsButton.ForeColor = colorDlg.Color;
            }
        }

        private void Settings_Load(object sender, EventArgs e) // Form Yüklenirken Arkaplanı memory kısmından çekiyor
        {
            this.BackColor = Properties.Settings.Default.FormBackgroundColor;
        }

        private void label2_Click(object sender, EventArgs e) // _ basınca app alta atma
        {
            main.WindowState = FormWindowState.Minimized;
        }
    }
}
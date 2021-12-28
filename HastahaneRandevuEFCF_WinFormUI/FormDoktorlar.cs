using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HastaneRandevuEFCF_Entities;
using HastahaneRandevuEFCF_BLL;

namespace HastahaneRandevuEFCF_WinFormUI
{
    public partial class FormDoktorlar : Form
    {
        public FormDoktorlar()
        {
            InitializeComponent();           
        }
        //Global Alan
        DoktorManager drManager = new DoktorManager();
              
        private void FormDoktorlar_Load(object sender, EventArgs e)
        {
            //datGrid
            DrGrideVerileriGetir();
            comboBoxDrBranslar.DropDownStyle = ComboBoxStyle.DropDownList;
            //comboBoxDrBranslar.DisplayMember = "TheValue";
            //comboBoxDrBranslar.ValueMember = "TheKey";
            //comboBoxDrBranslar.DataSource = EnumManager.TumBranslariGetir();
            //comboBoxDrBranslar.SelectedValue = (int)Branslar.BransYok;
            comboBoxDrBranslar.DataSource = Enum.GetValues(typeof(Branslar));
            //maskedtextbox maske ayarlaması
            maskedTextBoxDrTel.Mask = @"\0\5##-###-##-##"; //11 hane
        }
        private void btnDrEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDrAd.Text.Trim().Length<2) //Su
                {
                    throw new Exception("Lütfen doktor adını düzgün giriniz!");
                }
                if (txtDrSoyad.Text.Trim().Length < 2) //Su
                {
                    throw new Exception("Lütfen doktor soyadını düzgün giriniz!");
                }
                if (txtDrTC.Text.Trim().Length!=11)
                {
                    throw new Exception("TC kimlik numarası on bir haneli olmalıdır!");
                }
                if (txtDrTC.Text.Any(x => !char.IsDigit(x)))
                {
                    throw new Exception("TC kimlik numarası rakamlardan oluşacak şekilde on bir haneli olmalıdır!");
                }

                Branslar drBransi;
                Enum.TryParse(comboBoxDrBranslar.SelectedValue.ToString(), out drBransi);
                Doktor yeniDoktor = new Doktor()
                {
                    DoktorAdi = txtDrAd.Text.Trim(),
                    DoktorSoyadi=txtDrSoyad.Text.Trim(),
                    TCNumarasi=txtDrTC.Text,
                    Email=txtDrEmail.Text,
                    Telefon=maskedTextBoxDrTel.Text.Replace("-",""),
                    Brans=drBransi
                };
                //Sisteme yani veritabanına ekleme yapması gerekiyor.
                if (drManager.YeniDoktorEkle(yeniDoktor))
                {
                    MessageBox.Show($"{txtDrAd.Text.Trim()} {txtDrSoyad.Text.Trim()} sisteme yeni doktorumuz olarak eklenmiştir.");
                    //Temizlik
                    DrEkleSayfasiTemizle();
                    DrGrideVerileriGetir();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA!" + ex.Message);
            }
        }

        private void DrGrideVerileriGetir()
        {
            dataGridViewDoktor.DataSource=drManager.TumAktifDoktorlariGetir();
                     
        }

        private void DrEkleSayfasiTemizle()
        {
            txtDrAd.Clear();
            txtDrSoyad.Clear();
            txtDrTC.Clear();
            txtDrEmail.Clear();
            comboBoxDrBranslar.SelectedIndex = 0;
            maskedTextBoxDrTel.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxDrBranslar_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}

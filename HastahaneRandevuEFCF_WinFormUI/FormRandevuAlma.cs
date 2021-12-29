using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HastahaneRandevuEFCF_BLL;
using HastaneRandevuEFCF_Entities;

namespace HastahaneRandevuEFCF_WinFormUI
{
    public partial class FormRandevuAlma : Form
    {
        public FormRandevuAlma()
        {
            InitializeComponent();
        }
        //Global Alan
        HastaManager hastaManagerim = new HastaManager();
        DoktorManager doktorManagerim = new DoktorManager();
        RandevuManager rndManager = new RandevuManager();

        private void FormRandevuAlma_Load(object sender, EventArgs e)
        {
            //Servis groupBox ve TarihSaat groupBoxları pasif olmalı.
            ServisGroupBoxiniPasiflestir();
            RandevuTarihVeSaatGroupBoxiniPasiflestir();
            HastaListeBoxiniDoldur();
            //datetimepicker'ı formatla.
            DateTimePickeriAyarla(DateTime.Now);
        }

        private void DateTimePickeriAyarla(DateTime trh)
        {
            dateTimePickerRandevuTarihi.Format = DateTimePickerFormat.Custom;
            dateTimePickerRandevuTarihi.CustomFormat = "dd.MM.yyyy";
            dateTimePickerRandevuTarihi.MinDate =trh;
            dateTimePickerRandevuTarihi.MaxDate = dateTimePickerRandevuTarihi.MinDate.AddDays(15);
            dateTimePickerRandevuTarihi.Value = trh;
        }

        private void HastaListeBoxiniDoldur()
        {
            listBoxHastalar.DataSource = hastaManagerim.TumHastalariGetir();
            listBoxHastalar.SelectedIndex = -1;
        }

        private void RandevuTarihVeSaatGroupBoxiniPasiflestir()
        {
            groupBoxRandevu.Enabled = false;
        }
        private void RandevuTarihVeSaatGroupBoxiniAktiflestir()
        {
            groupBoxRandevu.Enabled = true;
        }
        private void ServisGroupBoxiniPasiflestir()
        {
            groupBoxServis.Enabled = false;
        }

        private void txtHastaTCArama_TextChanged(object sender, EventArgs e)
        {
            if (txtHastaTCArama.Text.Trim().Length>=2)
            {
                listBoxHastalar.DataSource = hastaManagerim.TcNumarasinaGoreHastayiBul(txtHastaTCArama.Text);
            }
            else
            {
                HastaListeBoxiniDoldur();
            }
        }

        private void listBoxHastalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxHastalar.SelectedIndex>=0)
            {
                ServisGroupBoxiniAktiflestir();
            }
            else
            {
                ServisGroupBoxiniPasiflestir();
                RandevuTarihVeSaatGroupBoxiniPasiflestir();
                UC_RandevuSaat1.Doktorum = null;
            }
            DateTimePickeriAyarla(DateTime.Now);
            UC_RandevuSaat1.Temizle();
        }
        private void ServisGroupBoxiniAktiflestir()
        {
            groupBoxServis.Enabled = true;
            //servis combo dolmalı.
            comboBoxServisiDoldur();
        }
        private void comboBoxServisiDoldur()
        {
            comboBoxServis.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxServis.DataSource = Enum.GetValues(typeof(Branslar));
            comboBoxServis.SelectedIndex = -1;

        }

        private void comboBoxServis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxServis.SelectedIndex>=0)
            {
                Branslar drBransi;
                Enum.TryParse(comboBoxServis.SelectedValue.ToString(), out drBransi);
                listBoxDoktorlar.DataSource = doktorManagerim.BransaGoreDoktorlariGetir(drBransi);
            }
            else
            {
                listBoxDoktorlar.DataSource = null;
            }
            listBoxDoktorlar.SelectedIndex = -1;
            DateTimePickeriAyarla(DateTime.Now);
            UC_RandevuSaat1.Temizle();
        }

        private void listBoxDoktorlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTimePickeriAyarla(DateTime.Now);
            if (listBoxDoktorlar.SelectedIndex>=0)
            {
                RandevuTarihVeSaatGroupBoxiniAktiflestir();
                Doktor seciliDr = listBoxDoktorlar.SelectedItem as Doktor;
                UC_RandevuSaat1.Doktorum = seciliDr;
            }
            else
            {               
                RandevuTarihVeSaatGroupBoxiniPasiflestir();
                UC_RandevuSaat1.Doktorum = null;
            }          
        }

        private void dateTimePickerRandevuTarihi_ValueChanged(object sender, EventArgs e)
        {
            DateTimePickeriAyarla(dateTimePickerRandevuTarihi.Value);
            UC_RandevuSaat1.DisaridanGelenTarih = dateTimePickerRandevuTarihi.Value;
            UC_RandevuSaat1.Temizle();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxHastalar.SelectedIndex<0)
                {
                    MessageBox.Show("Hasta seçmeden randevu işlemleri yapılamaz!");
                    return;
                }
                if (listBoxDoktorlar.SelectedIndex<0)
                {
                    MessageBox.Show("Doktor seçmeden randevu işlemleri yapılamaz!");
                }
                if (!UC_RandevuSaat1.RandevuAlmaAktifMi)
                {
                    MessageBox.Show("Randevu alabilmeniz için yukarıdaki randevu saati butonlarına tıklayarak saat seçmelisiniz","UYARI!",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
                //Hastanın o tarihe ait o saatte başka bir randevusu varsa alamaz.
                Hasta secilenHasta = listBoxHastalar.SelectedItem as Hasta;
                if (rndManager.HastaninSecilenTarihveSaatteRandevusuVarMi(secilenHasta,UC_RandevuSaat1.SecilenRandevuTarihi))
                {
                    MessageBox.Show($"DİKKAT: {UC_RandevuSaat1.SecilenRandevuTarihi.ToString("dd.MM.yyyy HH:mm:ss")} tarihinde başka bir randevunuz vardır. Bu nedenle aynı saate randevu alamazsınız!","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
                //Hasta randevu alabilir.
                Doktor secilenDoktor = listBoxDoktorlar.SelectedItem as Doktor;
                RandevuBilgileri yeniRandevu = new RandevuBilgileri()
                {
                    HastaId=secilenHasta.HastaId,
                    DoktorId=secilenDoktor.DoktorId,
                    RandevuTarihi=UC_RandevuSaat1.SecilenRandevuTarihi
                };
                bool rndAlindiMi = false;
                rndAlindiMi = rndManager.RandevuyuAl(yeniRandevu);
                if (rndAlindiMi)
                {
                    MessageBox.Show("Randevunuz alınmıştır!","BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //temizlik
                    UC_RandevuSaat1.Temizle();
                    dateTimePickerRandevuTarihi.Value = DateTime.Now;
                    RandevuTarihVeSaatGroupBoxiniPasiflestir();
                    ServisGroupBoxiniPasiflestir();
                    listBoxHastalar.SelectedIndex = -1;
                }
                else
                {
                    throw new Exception("Randevuyu alma işleminde beklenmedik bir hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA!" + ex.Message);
            }
        }
    }
}

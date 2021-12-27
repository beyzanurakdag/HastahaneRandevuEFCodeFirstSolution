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
              
        private void FormDoktorlar_Load(object sender, EventArgs e)
        {
            comboBoxDrBranslar.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDrBranslar.DisplayMember = "TheValue";
            comboBoxDrBranslar.ValueMember = "TheKey";
            comboBoxDrBranslar.DataSource = EnumManager.TumBranslariGetir();
            comboBoxDrBranslar.SelectedValue = (int)Branslar.BransYok;
        }
        private void btnDrEkle_Click(object sender, EventArgs e)
        {
            Doktor yeniDoktor = new Doktor()
            {
                DoktorAdi=txtDrAd.Text.Trim(),
                DoktorSoyadi=txtDrSoyad.Text.Trim(),
                //Brans=
                Email=txtDrEmail.Text,
                Telefon=maskedTextBoxDrTel.Text,
                TCNumarasi=txtDrTC.Text
            };
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxDrBranslar_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}

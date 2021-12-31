using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastahaneRandevuEFCF_WinFormUI
{
    public partial class FormGiris : Form
    {
        public FormGiris()
        {
            InitializeComponent();
        }

        private void btnDoktorIslemleri_Click(object sender, EventArgs e)
        {
            //this.Hide();
            FormDoktorlar frmDr = new FormDoktorlar();
            frmDr.ShowDialog();
        }

        private void btnRandevuIslemleri_Click(object sender, EventArgs e)
        {
            //this.Hide();
            FormRandevuAlma frmRandevu = new FormRandevuAlma();
            frmRandevu.ShowDialog();
        }

        private void btnHastaCagir_Click(object sender, EventArgs e)
        {
            FormHastaCagir frmHastaCagir = new FormHastaCagir();
            frmHastaCagir.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Shown(object sender, EventArgs e)
        {
            txtChanceOfLife.Value = ((frmMain)this.Owner).ChanceOfLife;
            txtInterval.Value = ((frmMain)this.Owner).TimeInterval / 100;
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            ((frmMain)this.Owner).ChanceOfLife = (int)txtChanceOfLife.Value;
            ((frmMain)this.Owner).TimeInterval = (int)txtInterval.Value * 100;
        }
    }
}

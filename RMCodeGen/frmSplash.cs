using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataTierGenerator
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Version : " + Application.ProductVersion;
            lblAnio.Text = DateTime.Now.Year + "";//'DateTime.Today.ToShortDateString;
        }
      
    }
}
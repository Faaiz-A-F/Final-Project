using RepairMe.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepairMe.View
{
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new Dashboard());
        }

        private void btnWorkshopList_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new WorkshopList());
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new HistoryPemesanan());
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new AboutUs());
        }

        private void btnexit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnWorkshopList_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkshopList workshopList = new WorkshopList();
            workshopList.Show();
            workshopList.FormClosed += (s, args) => this.Show();
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserProfil userProfil = new UserProfil();
            userProfil.Show();
            userProfil.FormClosed += (s, args) => this.Show();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            this.Hide();
            HistoryPemesanan historyPemesanan = new HistoryPemesanan();
            historyPemesanan.Show();
            historyPemesanan.FormClosed += (s, args) => this.Show();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            aboutUs.FormClosed += (s, args) => this.Show();
        }
    }
}

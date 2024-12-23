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

        private void btnPemesanan_Click(object sender, EventArgs e)
        {
            
        }

        private void btnWorkshopList_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkshopList workshopList = new WorkshopList();
            workshopList.Show();
            workshopList.FormClosed += (s, args) => this.Show();
        }
    }
}

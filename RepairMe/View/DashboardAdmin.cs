using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RepairMe.Controller;
using RepairMe.Model.Entity;
using RepairMe.Model.Context;
using RepairMe.View;

namespace RepairMe
{
    public partial class DashboardAdmin : Form
    {
        public DashboardAdmin()
        {
            InitializeComponent();
        }

        private void btnJasa_Click(object sender, EventArgs e)
        {
            new PenambahanJasa().Show();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

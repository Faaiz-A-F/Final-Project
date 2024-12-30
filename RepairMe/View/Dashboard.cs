using RepairMe.Model.Entity;
using RepairMe.Model.Repository;
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
using RepairMe.Model.Context;
using RepairMe.View;
using Guna.UI2.WinForms;

namespace RepairMe.View
{
    public partial class Dashboard : Form
    {
        private readonly DbContext dbContext;
        private readonly WorkshopController _workshopController;

        public Dashboard()
        {
            InitializeComponent();

            // Initialize dbContext and WorkshopController in the constructor
            dbContext = new DbContext();
            _workshopController = new WorkshopController(dbContext);

            // Load the best workshop data when the dashboard loads
            LoadBestWorkshop();
        }

        private void LoadBestWorkshop()
        {
            try
            {
                // Get the best workshop using your existing method
                Workshop bestWorkshop = _workshopController.GetBestWorkshop(); // Replace `_workshopRepository` with your actual repository/controller instance

                if (bestWorkshop != null)
                {
                    // Set the workshop name in tbNamaBengkel (TextBox)
                    tbNamaBengkel.Text = bestWorkshop.Name;

                    // Set the average rating in rsRating (Rating Star control)
                    rsRating.Value = (float)bestWorkshop.Rating; // Assuming rsRating is a Guna2RatingStar
                }
                else
                {
                    // No workshops found or no ratings available
                    tbNamaBengkel.Text = "No workshop available";
                    rsRating.Value = 0; // Set rating to 0 if no valid data is found
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load the best workshop.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnWorkshopList_Click(object sender, EventArgs e)
        {
            this.Close();
            WorkshopList workshopList = new WorkshopList();
            workshopList.Show();
            workshopList.FormClosed += (s, args) => this.Show();
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            this.Close();
            UserProfil userProfil = new UserProfil();
            userProfil.Show();
            userProfil.FormClosed += (s, args) => this.Show();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            this.Close();
            HistoryPemesanan historyPemesanan = new HistoryPemesanan();
            historyPemesanan.Show();
            historyPemesanan.FormClosed += (s, args) => this.Show();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            this.Close();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            aboutUs.FormClosed += (s, args) => this.Show();
        }

        private void btnPesan_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the best workshop
                Workshop bestWorkshop = _workshopController.GetBestWorkshop();

                if (bestWorkshop != null)
                {
                    // Open the DashboardPemesanan form and pass the workshop ID
                    DashboardPemesanan dashboardPemesanan = new DashboardPemesanan(bestWorkshop.Id);
                    this.Close();
                    dashboardPemesanan.Show();
                    dashboardPemesanan.FormClosed += (s, args) => this.Show(); // Show this form again when the other form is closed
                }
                else
                {
                    MessageBox.Show("No workshop available to order from.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load the workshop for ordering.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

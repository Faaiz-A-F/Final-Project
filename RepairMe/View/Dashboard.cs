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
using RepairMe.Utils;

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
            FormManager.ShowForm(new WorkshopList());
            this.Close();
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new UserProfil());
            this.Close();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new HistoryPemesanan());
            this.Close();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new AboutUs());
            this.Close();
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
                    FormManager.ShowForm(new DashboardPemesanan(bestWorkshop.Id));
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

        private void btnHome_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new Dashboard());
            this.Close();
        }
    }
}

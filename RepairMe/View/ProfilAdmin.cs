using RepairMe.Model.Context;
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
using RepairMe.Model.Entity;
using RepairMe.View;
using Guna.UI2.WinForms;
using RepairMe.Utils;

namespace RepairMe.View
{
    public partial class ProfilAdmin : Form
    {
        private readonly UsersController _usersController;

        public ProfilAdmin()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            using (var dbContext = new DbContext())
            {
                var userController = new UsersController(dbContext);
                var currentUser = userController.GetCurrentUserOrAdmin();

                if (currentUser != null)
                {
                    tbUsername.Text = currentUser.Username;
                    tbEmail.Text = currentUser.Email;
                    tbAge.Text = currentUser.Age.ToString(); // Null-safe
                    tbPhone.Text = currentUser.Phone;
                    tbAddress.Text = currentUser.Address;
                    tbPassword.Text = currentUser.Password;
                }
                else
                {
                    MessageBox.Show("Failed to load user data. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close(); // Close the form if no user is logged in
                }
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new AboutUs());
        }

        private void btnJasa_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new PenambahanJasa());
        }

        private void btnPesanan_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm(new DashboardAdmin());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Update user details
                    var username = tbUsername.Text.Trim();
                    var age = int.Parse(tbAge.Text.Trim());
                    var email = tbEmail.Text.Trim();
                    var phone = tbPhone.Text.Trim();
                    var address = tbAddress.Text.Trim();
                    var password = tbPassword.Text.Trim();

                    // Call the controller to update the user in the repository
                    var usersController = new UsersController(dbContext);
                    usersController.UpdateAdmin(username, password, age, email, phone, address);

                    // Inform the user about the success
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadUserData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to update profile.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}


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

namespace RepairMe.View
{
    public partial class ProfilAdmin : Form
    {
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
            this.Hide();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            aboutUs.FormClosed += (s, args) => this.Close();
        }

        private void btnJasa_Click(object sender, EventArgs e)
        {
            this.Hide();
            PenambahanJasa penambahanJasa = new PenambahanJasa();
            penambahanJasa.Show();
            penambahanJasa.FormClosed += (s, args) => this.Close();
        }

        private void btnPesanan_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

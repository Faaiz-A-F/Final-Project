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
    public partial class SignIn : Form
    {
        private readonly UsersController _usersController;

        public SignIn()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                // Pass DbContext to UsersController
                var _usersController = new UsersController(dbContext);

                // Get username and password from text boxes
                string username = tbuserin.Text.Trim();
                string password = tbpassin.Text.Trim();

                // Validate input
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // Call UsersController to validate user
                    Users user = _usersController.GetUser(username, password);

                    if (user != null)
                    {
                        // Sign-in successful
                        MessageBox.Show($"Welcome, {user.Username}!", "Sign-In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // TODO: Open main application window or perform any other post-login actions
                    }
                    else
                    {
                        // Invalid credentials
                        MessageBox.Show("Invalid username or password.", "Sign-In Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Handle unexpected errors
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsignup_Click(object sender, EventArgs e)
        {
            // Membuat instance form SignUp
            SignUp signUp = new SignUp();

            // Menyembunyikan form saat ini
            this.Hide();

            // Menampilkan form SignUp
            signUp.Show();

            // Menambahkan event untuk menangani ketika form SignUp ditutup
            signUp.FormClosed += (s, args) => this.Show();
        }

        private void signupbengkel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MakeBengkel makeBengkel = new MakeBengkel();
            this.Hide();
            makeBengkel.Show();
            makeBengkel.FormClosed += (s, args) => this.Show();
        }
    }
}

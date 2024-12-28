using RepairMe.Controller;
using RepairMe.Model.Context;
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
    public partial class MakeBengkel : Form
    {
        public MakeBengkel()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            SignIn signIn = new SignIn();
            signIn.Show();
            this.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Pass DbContext to UsersController
                    var usersController = new UsersController(dbContext);

                    // Collect input data
                    var username = tbUserBengkel.Text.Trim();
                    var password = tbPassUp.Text.Trim();
                    var age = int.Parse(tbAgeUp.Text.Trim());
                    var email = tbEmailUp.Text.Trim();
                    var phone = tbPhoneUp.Text.Trim();
                    var address = tbAddressUp.Text.Trim();
                    var role = "admin";
                    usersController.AddAdmin(username, password, email, phone, address, role, age);
                }
                catch (Exception ex)
                {
                    // Display error message if connection fails
                    MessageBox.Show($"Failed to connect to the database.\n\nError: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Display success message
                MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form fields after successful registration
                tbUserBengkel.Clear();
                tbPassUp.Clear();
                tbEmailUp.Clear();
                tbPhoneUp.Clear();
                tbAddressUp.Clear();
            }
        }
    }
}

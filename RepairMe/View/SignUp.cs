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
using RepairMe.Model.Entity;

namespace RepairMe
{
    public partial class SignUp : Form
    {
        private readonly UsersController _usersController;

        public SignUp()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click_1(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Pass DbContext to UsersController
                    var usersController = new UsersController(dbContext);

                    // Collect input data
                    var username = tbUserUp.Text.Trim();
                    var password = tbPassUp.Text.Trim();
                    var age = int.Parse(tbAgeUp.Text.Trim());
                    var email = tbEmailUp.Text.Trim();
                    var phone = int.Parse(tbPhoneUp.Text.Trim());
                    var address = tbAddressUp.Text.Trim();
                    var role = "user";
                    usersController.AddUser(username, password, age, email, phone, address, role);

                    // Display success message
                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the form fields after successful registration
                    tbUserUp.Clear();
                    tbPassUp.Clear();
                    tbAgeUp.Clear();
                    tbEmailUp.Clear();
                    tbPhoneUp.Clear();
                    tbAddressUp.Clear();
                }
                catch (Exception ex)
                {
                    // Display error message if connection fails
                    MessageBox.Show($"Failed to connect to the database.\n\nError: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            // Membuat instance form SignIn
            SignIn signIn = new SignIn();

            // Menampilkan form SignIn
            signIn.Show();

            // Menutup form saat ini
            this.Close();
        }
    }
}

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

namespace RepairMe
{
    public partial class SignUp : Form
    {
        private readonly UsersController _usersController;

        public SignUp()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
          // Collect input data
          var username = tbUserUp.Text.Trim();
          var password = tbPassUp.Text.Trim();
          var age = int.Parse(tbAgeUp.Text.Trim());
          var email = tbEmailUp.Text.Trim();
          var phone = tbPhoneUp.Text.Trim();
          var address = tbAddressUp.Text.Trim();
          var role = "user";

          // Add user through controller
          var usersController = new UsersController();
          usersController.AddUser(username, password, age, email, phone, address, role);

          // Clear the form fields after successful registration
          tbUserUp.Clear();
          tbPassUp.Clear();
          tbAgeUp.Clear();
          tbEmailUp.Clear();
          tbPhoneUp.Clear();
          tbAddressUp.Clear();
        }
    }
}

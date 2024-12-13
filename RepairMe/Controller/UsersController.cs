using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using RepairMe.Model.Entity;
using RepairMe.Model.Repository;
using RepairMe.Model.Context;

namespace RepairMe.Controller
{
    internal class UsersController
    {
        private readonly UsersRepository _usersRepository;

        // Default constructor
        public UsersController(DbContext context)
        {
            _usersRepository = new UsersRepository(context);
        }

        // Constructor for dependency injection
        public UsersController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void AddUser(string username, string password, int age, string email, string phone, string address, string role)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (age <= 0)
                {
                    MessageBox.Show("Age must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create user object
                var user = new Users
                {
                    Username = username,
                    Password = password,
                    Age = age,
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    Role = role
                };

                // Add user to the repository
                _usersRepository.AddUser(user);

                // Show success message
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Users GetUser(string username, string password)
        {
            try
            {
                return _usersRepository.GetUser(username, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}

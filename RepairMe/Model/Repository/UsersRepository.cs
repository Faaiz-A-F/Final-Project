﻿using RepairMe.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RepairMe.Model.Entity;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace RepairMe.Model.Repository
{
    internal class UsersRepository
    {
        private readonly DbContext _dbContext;

        public UsersRepository(DbContext context)
        {
            _dbContext = new DbContext();
        }

        public void AddUser(Users user)
        {
            try
            {
                _dbContext.OpenConnection();

                var query = "INSERT INTO users (name, password, age, email, phone, address, role) " +
                    "VALUES (@name, @password, @age, @email, @phone, @address, @role)";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@name", user.Username);    // Ensure 'Username' matches your database schema
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@age", user.Age);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@phone", user.Phone);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@role", user.Role);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Display error message if connection fails
                MessageBox.Show($"Failed to connect to the database.\n\nError: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public void AddAdmin(Users user)
        {
            try
            {
                _dbContext.OpenConnection();

                var query = "INSERT INTO admin (name, password, email, phone, address, role) " +
                            "VALUES (@name, @password, @email, @phone, @address, @role)";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@name", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@phone", user.Phone);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@role", user.Role);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Display error message if connection fails
                MessageBox.Show($"Failed to connect to the database.\n\nError: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public Users GetUserOrAdmin(string username, string password)
        {
            try
            {
                _dbContext.OpenConnection();

                // Cek di tabel admin dulu
                var query = "SELECT 'admin' AS role, admin_id AS id, name, password, email, phone, address " +
                            "FROM admin WHERE name = @username AND password = @password " +
                            "UNION " +
                            "SELECT 'user' AS role, user_id AS id, name, password, email, phone, address " +
                            "FROM users WHERE name = @username AND password = @password";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Users
                            {
                                Id = reader.GetInt32("id"),
                                Username = reader.GetString("name"),
                                Password = reader.GetString("password"),
                                Email = reader.GetString("email"),
                                Phone = reader.GetString("phone"),
                                Address = reader.GetString("address"),
                                Role = reader.GetString("role")  // Role akan menunjukkan 'admin' atau 'user'
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to the database.\n\nError: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                _dbContext.CloseConnection();
            }

            return null;
        }
    }
}

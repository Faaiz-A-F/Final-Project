﻿using RepairMe.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RepairMe.Model.Entity;
using MySql.Data.MySqlClient;

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
                            "VALUES (@username, @password, @name, @age, @email, @phone, @address, @role)";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
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
                Console.WriteLine($"Error adding user: {ex.Message}");
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

                var query = "INSERT INTO users (name, password, age, email, phone, address, role) " +
                            "VALUES (@username, @password, @name, @age, @email, @phone, @address, admin)";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@age", user.Age);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@phone", user.Phone);
                    cmd.Parameters.AddWithValue("@address", user.Address);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                throw;
            }
            finally
            {
                _dbContext.CloseConnection();
            }
        }

        public Users GetUser(string username, string password)
        {
            try
            {
                _dbContext.OpenConnection();

                var query = "SELECT * FROM users WHERE name = @username AND password = @password";

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
                                Username = reader.GetString("username"),
                                Password = reader.GetString("password"),
                                Email = reader.GetString("email"),
                                Phone = reader.GetString("phone"),
                                Address = reader.GetString("address"),
                                Role = reader.GetString("role")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user: {ex.Message}");
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

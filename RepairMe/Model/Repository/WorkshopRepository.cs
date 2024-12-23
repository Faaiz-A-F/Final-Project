using MySql.Data.MySqlClient;
using RepairMe.Model.Context;
using RepairMe.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairMe.Model.Repository
{
    internal class WorkshopRepository
    {
        private readonly DbContext _dbContext;

        public WorkshopRepository(DbContext context)
        {
            _dbContext = new DbContext();
        }

        public List<Workshop> SearchWorkshops(string keyword)
        {
            var workshops = new List<Workshop>();

            try
            {
                _dbContext.OpenConnection();

                var query = "SELECT * FROM admin WHERE name LIKE @keyword OR address LIKE @keyword";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var workshop = new Workshop
                            {
                                Id = reader.GetInt32("admin_id"),
                                Name = reader.GetString("name"),
                                Address = reader.GetString("address"),
                                Phone = reader.GetString("phone"),
                                Email = reader.GetString("email")
                            };

                            workshops.Add(workshop);
                        }
                    }
                }
            }
            finally
            {
                _dbContext.CloseConnection();
            }

            return workshops;
        }

        public List<Workshop> GetAllWorkshop()
        {
            var workshops = new List<Workshop>();

            try
            {
                _dbContext.OpenConnection();

                var query = "SELECT * FROM admin";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var workshop = new Workshop
                            {
                                Id = reader.GetInt32("admin_id"),
                                Name = reader.GetString("name"),
                                Address = reader.GetString("address"),
                                Phone = reader.GetString("phone"),
                                Email = reader.GetString("email")
                            };

                            workshops.Add(workshop);
                        }
                    }
                }
            }
            finally
            {
                _dbContext.CloseConnection();
            }

            return workshops;
        }
    }
}

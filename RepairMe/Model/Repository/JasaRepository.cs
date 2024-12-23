using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RepairMe.Model.Context;
using RepairMe.Model.Entity;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace RepairMe.Model.Repository
{
    internal class JasaRepository
    {
        private readonly DbContext _dbContext;

        public JasaRepository(DbContext context)
        {
            _dbContext = new DbContext();
        }

        public void AddJasa(Jasa jasa)
        {
            _dbContext.OpenConnection();

            var query = "INSERT INTO jasa_bengkel (name, price, description, admin_id) " +
                "VALUES (@name, @price, @description), @adminId";

            using (var cmd = new MySqlCommand(query, _dbContext.Connection))
            {
                cmd.Parameters.AddWithValue("@name", jasa.Name);    // Ensure 'Name' matches your database schema
                cmd.Parameters.AddWithValue("@price", jasa.Price);
                cmd.Parameters.AddWithValue("@description", jasa.Description);
                cmd.Parameters.AddWithValue("@admin_id", jasa.AdminId);

                cmd.ExecuteNonQuery();
            }

            _dbContext.CloseConnection();
        }

        public void DeleteJasa(int id)
        {
            _dbContext.OpenConnection();

            var query = "DELETE FROM jasa_bengkel WHERE id = @id";

            using (var cmd = new MySqlCommand(query, _dbContext.Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            _dbContext.CloseConnection();
        }

        public List<Jasa> GetAllJasa(int adminId)
        {
            _dbContext.OpenConnection();

            var query = "SELECT * FROM jasa_bengkel WHERE admin_id = @adminId";
            var jasaList = new List<Jasa>();

            using (var cmd = new MySqlCommand(query, _dbContext.Connection))
            {
                cmd.Parameters.AddWithValue("@adminId", adminId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var jasa = new Jasa
                        {
                            Id = reader.GetInt32("jasa_id"), // kalo read harus sesuai nama di kolom tabel nya, tapi kok iki ra muncul ya id ne, opo tak hide ya lali, mbuh lah rapenting juga
                            Name = reader.GetString("name"),
                            Price = reader.GetFloat("price"),
                            Description = reader.GetString("description"),
                            AdminId = reader.GetInt32("admin_id") // kayake ra dinggo tur wait, iseh bingung
                        };

                        jasaList.Add(jasa);
                    }
                }
            }

            _dbContext.CloseConnection();

            return jasaList;
        }

        public List<Jasa> GetJasaByWorkshopId(int workshopId)
        {
            var jasaList = new List<Jasa>();

            try
            {
                _dbContext.OpenConnection();

                var query = "SELECT * FROM jasa_bengkel WHERE admin_id = @adminId";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@adminId", workshopId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var jasa = new Jasa
                            {
                                Id = reader.GetInt32("jasa_id"),
                                Name = reader.GetString("name"),
                                Price = (float)reader.GetDecimal("price"),
                                Description = reader.GetString("description"),
                                AdminId = reader.GetInt32("admin_id"),
                            };

                            jasaList.Add(jasa);
                        }
                    }
                }
            }
            finally
            {
                _dbContext.CloseConnection();
            }

            return jasaList;
        }
    }
}

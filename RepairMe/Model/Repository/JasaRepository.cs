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
            try
            {
                _dbContext.OpenConnection();

                var query = "INSERT INTO jasa_bengkel (name, price, description) " +
                    "VALUES (@name, @price, @description)";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@name", jasa.Name);    // Ensure 'Name' matches your database schema
                    cmd.Parameters.AddWithValue("@price", jasa.Price);
                    cmd.Parameters.AddWithValue("@description", jasa.Description);
                    cmd.Parameters.AddWithValue("@admin_id", jasa.AdminId);

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
    }
}

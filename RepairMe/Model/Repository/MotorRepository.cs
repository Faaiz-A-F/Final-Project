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
    internal class MotorRepository
    {
        private readonly DbContext _dbContext;

        public MotorRepository(DbContext context)
        {
            _dbContext = new DbContext();
        }

        public void AddMotor(Motor motor)
        {
            var dbContext = new DbContext();
            dbContext.OpenConnection();

            var query = "INSERT INTO motor (name, brand, engine, type, color, year, plate, user_id) " +
                "VALUES (@name, @brand, @engine, @type, @color, @year, @plate, @userId)";

            using (var cmd = new MySqlCommand(query, dbContext.Connection))
            {
                cmd.Parameters.AddWithValue("@name", motor.Name);    // Ensure 'Name' matches your database schema
                cmd.Parameters.AddWithValue("@brand", motor.Brand);
                cmd.Parameters.AddWithValue("@engine", motor.Engine);
                cmd.Parameters.AddWithValue("@type", motor.Type);
                cmd.Parameters.AddWithValue("@color", motor.Color);
                cmd.Parameters.AddWithValue("@year", motor.Year);
                cmd.Parameters.AddWithValue("@plate", motor.Plate);
                cmd.Parameters.AddWithValue("@userId", motor.UserId);

                cmd.ExecuteNonQuery();
            }

            dbContext.CloseConnection();
        }

        public void DeleteMotor(int id)
        {
            var dbContext = new DbContext();
            dbContext.OpenConnection();

            var query = "DELETE FROM motor WHERE id = @id";

            using (var cmd = new MySqlCommand(query, dbContext.Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            dbContext.CloseConnection();
        }
    }
}

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
            _dbContext.OpenConnection();

            var query = "INSERT INTO motor (brand, engine, type, color, year, plate, user_id, name) " +
                "VALUES (@brand, @engine, @type, @color, @year, @plate, @userId, @name)";

            using (var cmd = new MySqlCommand(query, _dbContext.Connection))
            {
                cmd.Parameters.AddWithValue("@brand", motor.Brand);
                cmd.Parameters.AddWithValue("@engine", motor.Engine);
                cmd.Parameters.AddWithValue("@type", motor.Type);
                cmd.Parameters.AddWithValue("@color", motor.Color);
                cmd.Parameters.AddWithValue("@year", motor.Year);
                cmd.Parameters.AddWithValue("@plate", motor.Plate);
                cmd.Parameters.AddWithValue("@userId", motor.UserId);
                cmd.Parameters.AddWithValue("@name", motor.Name);

                cmd.ExecuteNonQuery();
            }

            _dbContext.CloseConnection();
        }

        public void DeleteMotor(int id)
        {
            _dbContext.OpenConnection();

            var query = "DELETE FROM motor WHERE motor_id = @id";

            using (var cmd = new MySqlCommand(query, _dbContext.Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            _dbContext.CloseConnection();
        }

        public List<Motor> GetMotorByUserId(int userId)
        {
            var motorList = new List<Motor>();

            try
            {
                _dbContext.OpenConnection();
                var query = "SELECT * FROM motor WHERE user_id = @userId";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var motor = new Motor
                            {
                                Id = reader.GetInt32("motor_id"),
                                Brand = reader.GetString("brand"),
                                Engine = reader.GetString("engine"),
                                Plate = reader.GetString("plate"),
                                Type = reader.GetString("type"),
                                Color = reader.GetString("color"),
                                Year = reader.GetString("year"),
                                UserId = reader.GetInt32("user_id"),
                                Name = reader.GetString("name")
                            };

                            motorList.Add(motor);
                        }
                    }
                }

                _dbContext.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading motors: " + ex.Message);
            }

            return motorList;
        }
    }
}

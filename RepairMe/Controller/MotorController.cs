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
    internal class MotorController
    {
        private readonly MotorRepository _motorRepository;

        // Default constructor
        public MotorController(DbContext context)
        {
            _motorRepository = new MotorRepository(context);
        }

        // Constructor for dependency injection
        public MotorController(MotorRepository motorRepository)
        {
            _motorRepository = motorRepository;
        }

        public void AddMotor(string name, string engine, string brand, string type, string color, string plate, string year, int userId)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(engine))
                {
                    MessageBox.Show("Engine cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(brand))
                {
                    MessageBox.Show("Brand cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(type))
                {
                    MessageBox.Show("Type cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(color))
                {
                    MessageBox.Show("Color cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(plate))
                {
                    MessageBox.Show("Plate cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(year))
                {
                    MessageBox.Show("Year cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create motor object
                var motor = new Motor
                {
                    Engine = engine,
                    Brand = brand,
                    Type = type,
                    Color = color,
                    Plate = plate,
                    Year = year,
                    UserId = userId
                };

                // Add motor to database
                _motorRepository.AddMotor(motor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteMotor(int id)
        {
            try
            {
                // Delete motor from database
                _motorRepository.DeleteMotor(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Motor> GetMotorByUserId(int userId)
        {
            try
            {
                // Get motor by user ID
                return _motorRepository.GetMotorByUserId(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}

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
    internal class JasaController
    {
        private readonly JasaRepository _jasaRepository;

        // Default constructor
        public JasaController(DbContext context)
        {
            _jasaRepository = new JasaRepository(context);
        }

        // Constructor for dependency injection
        public JasaController(JasaRepository jasaRepository)
        {
            _jasaRepository = jasaRepository;
        }

        public void AddJasa(string name, float price, string description, int id)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (price <= 0)
                {
                    MessageBox.Show("Price must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(description))
                {
                    MessageBox.Show("Description cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create jasa object
                var jasa = new Jasa
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    AdminId = id
                };

                // Add jasa to database
                _jasaRepository.AddJasa(jasa);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding jasa.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteJasa(int id)
        {
            try
            {
                // Delete jasa from database
                _jasaRepository.DeleteJasa(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting jasa.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Jasa> GetAllJasa(int adminId)
        {
            try
            {
                // Get all jasa for the specified admin from the repository
                return _jasaRepository.GetAllJasa(adminId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while getting jasa.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Jasa>(); // Return an empty list if an error occurs
            }
        }

        public List<Jasa> GetJasaByWorkshopId(int workshopId)
        {
           return _jasaRepository.GetJasaByWorkshopId(workshopId);
        }
    }
}

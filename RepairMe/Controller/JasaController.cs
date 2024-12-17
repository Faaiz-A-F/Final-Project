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

        public void AddJasa(string name, float price, string description)
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
                    Description = description
                };

                // Add jasa to database
                _jasaRepository.AddJasa(jasa);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding jasa.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

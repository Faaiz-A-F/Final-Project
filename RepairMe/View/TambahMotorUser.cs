using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RepairMe.Controller;
using RepairMe.Model.Entity;
using RepairMe.Model.Context;
using RepairMe.View;

namespace RepairMe.View
{
    public partial class TambahMotorUser : Form
    {
        public TambahMotorUser()
        {
            InitializeComponent();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate CurrentUserId
                if (Users.CurrentUserId == null)
                {
                    MessageBox.Show("User ID is not available. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get input values
                string name = tbName.Text.Trim();
                string engine = tbEngine.Text.Trim();
                string brand = tbBrand.Text.Trim();
                string type = cbType.SelectedItem?.ToString(); // Use selected value
                string color = tbColor.Text.Trim();
                string plate = tbPlate.Text.Trim();
                string year = tbYear.Text.Trim();

                // Call the controller to add the motor
                using (var dbContext = new DbContext())
                {
                    var motorController = new MotorController(dbContext);

                    motorController.AddMotor(name, engine, brand, type, color, plate, year, Users.CurrentUserId.Value);
                }

                // Notify success and clear fields
                MessageBox.Show("Motor added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tbName.Clear();
                tbEngine.Clear();
                tbBrand.Clear();
                cbType.SelectedIndex = -1;
                tbColor.Clear();
                tbPlate.Clear();
                tbYear.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add motor.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

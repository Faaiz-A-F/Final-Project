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
using Guna.UI2.WinForms;
using RepairMe.Model.Repository;

namespace RepairMe.View
{
    public partial class UserProfil : Form
    {
        private int _userId;

        public UserProfil()
        {
            InitializeComponent();
            _userId = Users.CurrentUserId.Value;
            LoadUserData();
            LoadMotorData();
        }

        private void LoadUserData()
        {
            using (var dbContext = new DbContext())
            {
                var userRepository = new UsersRepository(dbContext);
                var currentUser = userRepository.GetCurrentUserOrAdmin();

                if (currentUser != null)
                {
                    tbUsername.Text = currentUser.Username;
                    tbEmail.Text = currentUser.Email;
                    tbAge.Text = currentUser.Age.ToString(); // Null-safe
                    tbPhone.Text = currentUser.Phone;
                    tbAddress.Text = currentUser.Address;
                }
                else
                {
                    MessageBox.Show("Failed to load user data. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close(); // Close the form if no user is logged in
                }
            }
        }
        private void AddCheckboxColumn()
        {
            if (!dtgMotor.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                dtgMotor.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
            }
        }

        private void CustomizeGunaDataGridView()
        {
            // Ensure column headers are visible
            dtgMotor.ColumnHeadersVisible = true;

            // Styling for the header
            dtgMotor.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgMotor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgMotor.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgMotor.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Styling for rows
            dtgMotor.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgMotor.DefaultCellStyle.ForeColor = Color.Black;
            dtgMotor.DefaultCellStyle.BackColor = Color.White;
            dtgMotor.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Grid appearance
            dtgMotor.EnableHeadersVisualStyles = false; // Disable default theme styles
            dtgMotor.GridColor = Color.Gray;
            dtgMotor.RowHeadersVisible = false; // Hides row headers (optional)

            // Auto-resize and fill columns
            dtgMotor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgMotor.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void LoadMotorData()
        {
            using (var dbContext = new DbContext())
            {
                var motorController = new MotorController(dbContext);
                var motorList = motorController.GetMotorByUserId(_userId);

                dtgMotor.AutoGenerateColumns = true; // Ensure columns are auto-generated
                dtgMotor.DataSource = null;         // Clear existing binding
                dtgMotor.DataSource = motorList;

                // Add a checkbox column
                AddCheckboxColumn();

                // Customize DataGridView for jasa
                CustomizeGunaDataGridView();
            }
        }

        private void btnTambahData_Click(object sender, EventArgs e)
        {
            TambahMotorUser tambahMotorUser = new TambahMotorUser();
            tambahMotorUser.Show();
            // Subscribe to the FormClosed event
            tambahMotorUser.FormClosed += (s, args) =>
            {
                // Reload the data after the form is closed
                LoadMotorData();
            };
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHapusData_Click(object sender, EventArgs e)
        {

            if (dtgMotor.SelectedRows.Count > 0)
            {
                // Confirm deletion
                var confirmation = MessageBox.Show("Are you sure you want to delete the selected motor(s)?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    // Get the ID of the selected row
                    var selectedRow = dtgMotor.SelectedRows[0];
                    int id = Convert.ToInt32(selectedRow.Cells["id"].Value);

                    using (var dbContext = new DbContext())
                    {
                        MotorController motorController = new MotorController(dbContext);

                        // Delete the data
                        motorController.DeleteMotor(id);
                    }

                    // Refresh data in the DataGridView
                    LoadMotorData();

                    // Show success message
                    MessageBox.Show("Item successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete item.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            aboutUs.FormClosed += (s, args) => this.Show();
        }
    }
}

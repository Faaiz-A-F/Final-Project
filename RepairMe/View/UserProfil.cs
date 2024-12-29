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
            // Styling
            dtgMotor.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgMotor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgMotor.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            dtgMotor.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgMotor.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgMotor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            dtgMotor.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            dtgMotor.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            dtgMotor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            dtgMotor.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            dtgMotor.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in dtgMotor.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in dtgMotor.Rows)
            {
                // Set minimum height for readability
                row.MinimumHeight = 20;
            }
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

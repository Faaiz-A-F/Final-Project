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
using RepairMe.Model.Context;
using RepairMe.Model.Entity;

namespace RepairMe.View
{
    public partial class PenambahanJasa : Form
    {
        private void CustomizeGunaDataGridView()
        {
            // Styling
            dtgJasa.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgJasa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgJasa.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            dtgJasa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgJasa.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgJasa.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            dtgJasa.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            dtgJasa.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            dtgJasa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            dtgJasa.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            dtgJasa.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in dtgJasa.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in dtgJasa.Rows)
            {
                // Set minimum height for readability
                row.MinimumHeight = 20;
            }
        }

        private void AddCheckboxColumn()
        {
            if (!dtgJasa.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                dtgJasa.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
            }
        }

        private void LoadData()
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    if (Users.CurrentAdminId == null)
                    {
                        MessageBox.Show("Admin ID is not available. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    JasaController jasaController = new JasaController(dbContext);

                    // Use the currently logged-in admin's ID
                    var adminId = Users.CurrentAdminId.Value; // Convert nullable int to non-nullable

                    // Fetch data for the current admin
                    var jasaData = jasaController.GetAllJasa(adminId);

                    // Auto-generate columns
                    dtgJasa.AutoGenerateColumns = true;

                    // Bind to Guna2DataGridView
                    dtgJasa.DataSource = null; // Clear existing binding
                    dtgJasa.DataSource = jasaData;

                    // Customize the appearance
                    CustomizeGunaDataGridView();
                    AddCheckboxColumn();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load data.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public PenambahanJasa()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    if (Users.CurrentAdminId == null)
                    {
                        MessageBox.Show("Admin ID is not available. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Pass DbContext to UsersController
                    var usersController = new UsersController(dbContext);

                    // Collect input data
                    var nama = tbNama.Text.Trim();
                    var harga = int.Parse(tbHarga.Text.Trim());
                    var deskripsi = rbDeskripsi.Text.Trim();
                    var id = Users.CurrentAdminId.Value; // Convert nullable int to non-nullable

                    JasaController jasaController = new JasaController(dbContext);
                    jasaController.AddJasa(nama, harga, deskripsi, id);

                    // Display success message if items are added successfully
                    MessageBox.Show("Items successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear input fields
                    tbNama.Clear();
                    tbHarga.Clear();
                    rbDeskripsi.Clear();
                }
                catch (Exception ex)
                {
                    // Display error message if connection fails
                    MessageBox.Show($"Failed to connect to the database.\n\nError: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dtgJasa.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the ID of the selected row
                    var selectedRow = dtgJasa.SelectedRows[0];
                    int id = Convert.ToInt32(selectedRow.Cells["id"].Value);

                    using (var dbContext = new DbContext())
                    {
                        JasaController jasaController = new JasaController(dbContext);

                        // Delete the data
                        jasaController.DeleteJasa(id);
                    }

                    // Refresh data in the DataGridView
                    LoadData();

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

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            this.Close();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            aboutUs.FormClosed += (s, args) => this.Show();
        }
    }
}


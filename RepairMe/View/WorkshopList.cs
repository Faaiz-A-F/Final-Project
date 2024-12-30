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

namespace RepairMe.View
{
    public partial class WorkshopList : Form
    {
        private void CustomizeGunaDataGridView()
        {
            // Styling
            dtgBengkel.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgBengkel.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgBengkel.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            dtgBengkel.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgBengkel.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgBengkel.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            dtgBengkel.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            dtgBengkel.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            dtgBengkel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            dtgBengkel.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            dtgBengkel.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in dtgBengkel.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in dtgBengkel.Rows)
            {
                // Set minimum height for readability
                row.MinimumHeight = 20;
            }
        }

        private void AddCheckboxColumn()
        {
            if (!dtgBengkel.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Select",
                    Width = 50
                };

                dtgBengkel.Columns.Insert(0, checkboxColumn); // Add as the first column
            }
        }

        private void LoadData(string keyword = "")
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Initialize WorkshopController
                    var workshopController = new WorkshopController(dbContext);

                    // Fetch workshops
                    var workshops = workshopController.SearchWorkshops(keyword);

                    // Bind workshops to DataGridView
                    dtgBengkel.DataSource = workshops;

                    // Add a checkbox column if it doesn't exist
                    if (!dtgBengkel.Columns.Contains("Select"))
                    {
                        DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn
                        {
                            Name = "Select",
                            HeaderText = "Pilih",
                            Width = 50,
                            ReadOnly = false,
                        };
                        dtgBengkel.Columns.Insert(0, selectColumn);
                    }

                    // Customize the DataGridView
                    CustomizeGunaDataGridView();
                    AddCheckboxColumn();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load data.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<int> GetSelectedWorkshopIds()
        {
            var selectedIds = new List<int>();

            foreach (DataGridViewRow row in dtgBengkel.Rows)
            {
                // Check if the checkbox is selected
                if (row.Cells["Select"] is DataGridViewCheckBoxCell checkboxCell &&
                    checkboxCell.Value is bool isChecked &&
                    isChecked)
                {
                    // Get the ID of the selected workshop
                    int id = Convert.ToInt32(row.Cells["Id"].Value);
                    selectedIds.Add(id);
                }
            }

            return selectedIds;
        }

        public WorkshopList()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPilih_Click(object sender, EventArgs e)
        {
            var selectedIds = GetSelectedWorkshopIds();

            if (selectedIds.Count == 0)
            {
                MessageBox.Show("Please select at least one workshop.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pass the selected IDs to the next step (e.g., view selected workshop's jasa)
            var selectedId = selectedIds.First(); // For single selection, take the first ID

            // Example: Open a new form with the selected workshop ID
            var jasaForm = new DashboardPemesanan(selectedId);
            this.Hide();
            jasaForm.Show();
            jasaForm.FormClosed += (s, args) => this.Show();
        }

        private void btnCariBengkel_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Initialize WorkshopController
                    var workshopController = new WorkshopController(dbContext);

                    // Get the search keyword
                    var keyword = tbCariBengkel.Text.Trim();

                    // Validate input
                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        MessageBox.Show("Please enter a keyword to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Fetch matching workshops
                    var workshops = workshopController.SearchWorkshops(keyword);

                    // Bind results to DataGridView
                    dtgBengkel.DataSource = workshops;

                    // Customize the DataGridView if needed
                    CustomizeGunaDataGridView();

                    // Show a message if no results are found
                    if (workshops.Count == 0)
                    {
                        MessageBox.Show("No workshops found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while searching for workshops.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

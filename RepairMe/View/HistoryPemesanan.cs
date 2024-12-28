using RepairMe.Controller;
using RepairMe.Model.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RepairMe.Model.Entity;
using RepairMe.View;
using Guna.UI2.WinForms;

namespace RepairMe.View
{
    public partial class HistoryPemesanan : Form
    {
        private int _userId;

        public HistoryPemesanan()
        {
            InitializeComponent();
            _userId = Users.CurrentUserId.Value;
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new DbContext())
            {
                var transactionController = new TransactionController(dbContext);
                var transactionList = transactionController.GetTransactionUser(_userId);

                dtgHistory.DataSource = null;
                dtgHistory.DataSource = transactionList;

                // Add a checkbox column
                AddCheckboxColumn();

                // Customize DataGridView for jasa
                CustomizeGunaDataGridView();
            }
        }

        private void AddCheckboxColumn()
        {
            if (!dtgHistory.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                dtgHistory.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
            }
        }

        private void CustomizeGunaDataGridView()
        {
            dtgHistory.AutoGenerateColumns = true;

            // Styling
            dtgHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            dtgHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgHistory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            dtgHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            dtgHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            dtgHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            dtgHistory.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            dtgHistory.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in dtgHistory.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in dtgHistory.Rows)
            {
                // Set minimum height for readability
                row.MinimumHeight = 20;
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKirimUlasan_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected transaction ID
                if (dtgHistory.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a transaction to review.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = dtgHistory.SelectedRows[0];
                int transactionId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                // Get the review and rating inputs
                string review = rtbUlasan.Text.Trim();
                int rating = (int)rsUlasan.Value;
                rsUlasan.Value = 0; // Reset the rating

                // Validate inputs
                if (string.IsNullOrWhiteSpace(review))
                {
                    MessageBox.Show("Please enter a review.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rating <= 0)
                {
                    MessageBox.Show("Please select a valid rating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var dbContext = new DbContext())
                {
                    // Call the controller to add the review
                    var transactionController = new TransactionController(dbContext);
                    transactionController.AddReview(transactionId, review, rating);
                }

                // Show success message
                MessageBox.Show("Review and rating submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the data grid view to show the updated review
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to submit review.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

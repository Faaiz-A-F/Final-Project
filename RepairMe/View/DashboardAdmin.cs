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

namespace RepairMe
{
    public partial class DashboardAdmin : Form
    {
        private int _adminId;

        public DashboardAdmin()
        {
            InitializeComponent();
            _adminId = Users.CurrentAdminId.Value;
            LoadData();
            LoadDataSelesai();
        }

        public void LoadData()
        {
            using (var dbContext = new DbContext())
            {
                var transactionController = new TransactionController(dbContext);
                var transactionList = transactionController.GetPendingTransactionAdmin(_adminId);

                dtgPesananMasuk.DataSource = null;
                dtgPesananMasuk.DataSource = transactionList;

                // Add a checkbox column
                AddCheckboxColumn();

                // Customize DataGridView for jasa
                CustomizeGunaDataGridView();
            }
        }

        public void LoadDataSelesai()
        {
            using (var dbContext = new DbContext())
            {
                var transactionController = new TransactionController(dbContext);
                var transactionListDone = transactionController.GetDoneTransactionAdmin(_adminId);

                dtgPesananSelesai.DataSource = null;
                dtgPesananSelesai.DataSource = transactionListDone;

                // Add a checkbox column
                AddCheckboxColumn_2();

                // Customize DataGridView for jasa
                CustomizeGunaDataGridView_2();

                // Calculate total pemasukan
                CalculateTotalPemasukan();
            }
        }

        private void CalculateTotalPemasukan()
        {
            try
            {
                // Ensure there is data in the DataGridView
                if (dtgPesananSelesai.Rows.Count > 0)
                {
                    decimal totalPemasukan = 0;

                    foreach (DataGridViewRow row in dtgPesananSelesai.Rows)
                    {
                        // Check if the row contains a valid value for "total"
                        if (row.Cells["total"]?.Value != null && decimal.TryParse(row.Cells["total"].Value.ToString(), out decimal total))
                        {
                            totalPemasukan += total;
                        }
                    }

                    // Display the total pemasukan in tbPemasukan
                    tbPemasukan.Text = totalPemasukan.ToString("N2"); // Format as currency or number with 2 decimal places
                }
                else
                {
                    tbPemasukan.Text = "0.00"; // No rows, so the total pemasukan is 0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating pemasukan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCheckboxColumn()
        {
            if (!dtgPesananMasuk.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                dtgPesananMasuk.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
            }
        }

        private void AddCheckboxColumn_2()
        {
            if (!dtgPesananSelesai.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                dtgPesananSelesai.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
            }
        }

        private void CustomizeGunaDataGridView()
        {
            // Styling
            dtgPesananMasuk.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgPesananMasuk.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgPesananMasuk.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            dtgPesananMasuk.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgPesananMasuk.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgPesananMasuk.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            dtgPesananMasuk.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            dtgPesananMasuk.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            dtgPesananMasuk.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            dtgPesananMasuk.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            dtgPesananMasuk.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in dtgPesananMasuk.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in dtgPesananMasuk.Rows)
            {
                // Set minimum height for readability
                row.MinimumHeight = 20;
            }
        }

        private void CustomizeGunaDataGridView_2()
        {
            // Styling
            dtgPesananSelesai.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgPesananSelesai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgPesananSelesai.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            dtgPesananSelesai.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgPesananSelesai.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgPesananSelesai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            dtgPesananSelesai.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            dtgPesananSelesai.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            dtgPesananSelesai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            dtgPesananSelesai.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            dtgPesananSelesai.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in dtgPesananSelesai.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in dtgPesananSelesai.Rows)
            {
                // Set minimum height for readability
                row.MinimumHeight = 20;
            }
        }

        private void btnJasa_Click(object sender, EventArgs e)
        {
            PenambahanJasa penambahanJasa = new PenambahanJasa();
            penambahanJasa.Show();
            this.Hide();
            penambahanJasa.FormClosed += (s, args) => this.Show();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                var transactionController = new TransactionController(dbContext);
                //bool updated = false;

                foreach (DataGridViewRow row in dtgPesananMasuk.Rows)
                {
                    // Check if the row has been selected via the checkbox
                    if (row.Cells["Select"] is DataGridViewCheckBoxCell checkboxCell
                        && checkboxCell.Value != null
                        && (bool)checkboxCell.Value)
                    {
                        try
                        {
                            // Retrieve the transaction ID (check the exact column name)
                            int transactionId = Convert.ToInt32(row.Cells["id"].Value);

                            // Update the transaction status to "Done"
                            transactionController.UpdateTransaction(transactionId);
                            //updated = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating transaction. \n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                MessageBox.Show("Selected transactions updated to 'Done'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload data to reflect changes
                LoadData();
                LoadDataSelesai();
            }
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            aboutUs.FormClosed += (s, args) => this.Show();
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProfilAdmin profilAdmin = new ProfilAdmin();
            profilAdmin.Show();
            profilAdmin.FormClosed += (s, args) => this.Show();
        }
    }
}

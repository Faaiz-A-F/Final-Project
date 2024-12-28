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

                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = transactionListDone;

                // Add a checkbox column
                AddCheckboxColumn_2();

                // Customize DataGridView for jasa
                CustomizeGunaDataGridView_2();
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
            if (!guna2DataGridView1.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                guna2DataGridView1.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
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
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            guna2DataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            guna2DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            guna2DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            guna2DataGridView1.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in guna2DataGridView1.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
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
    }
}

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



        private void btnJasa_Click(object sender, EventArgs e)
        {
            new PenambahanJasa().Show();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

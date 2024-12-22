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
            // Clear existing columns
            guna2DataGridView1.Columns.Clear();

            // Add 'Nama Jasa' column
            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nama",
                DataPropertyName = "Name", // Matches the Jasa property
                HeaderText = "Nama Jasa"
            });

            // Add 'Harga' column
            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "harga",
                DataPropertyName = "Price", // Matches the Jasa property
                HeaderText = "Harga (Rp)"
            });

            // Add 'Deskripsi' column
            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "deskripsi",
                DataPropertyName = "Description", // Matches the Jasa property
                HeaderText = "Deskripsi"
            });

            // Styling
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadData()
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    JasaController jasaController = new JasaController(dbContext);

                    // Use the currently logged-in admin's ID
                    var adminId = Users.CurrentAdminId;

                    // Fetch data for the current admin
                    var jasaData = jasaController.GetAllJasa(adminId);

                    // Auto-generate columns
                    guna2DataGridView1.AutoGenerateColumns = true;

                    // Bind to Guna2DataGridView
                    guna2DataGridView1.DataSource = null; // Clear existing binding
                    guna2DataGridView1.DataSource = jasaData;

                    // Customize the appearance
                    CustomizeGunaDataGridView();
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
                    // Pass DbContext to UsersController
                    var usersController = new UsersController(dbContext);

                    // Collect input data
                    var nama = tbNama.Text.Trim();
                    var harga = int.Parse(tbHarga.Text.Trim());
                    var deskripsi = rbDeskripsi.Text.Trim();
                    var id = Users.CurrentAdminId;
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
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the ID of the selected row
                    var selectedRow = guna2DataGridView1.SelectedRows[0];
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
    }
}


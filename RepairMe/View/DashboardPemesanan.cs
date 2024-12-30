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
    public partial class DashboardPemesanan : Form
    {
        private int _workshopId;

        public DashboardPemesanan(int workshopId)
        {
            InitializeComponent();
            _workshopId = workshopId;
            LoadWorkshopJasa(); // Load jasa based on the workshop ID
            LoadUserMotors();   // Load user motors
        }

        private void LoadWorkshopJasa()
        {
            using (var dbContext = new DbContext())
            {
                var jasaController = new JasaController(dbContext);
                var jasaList = jasaController.GetJasaByWorkshopId(_workshopId);

                dtgJasaPilih.DataSource = jasaList;

                // Add a checkbox column
                AddCheckboxColumn();

                // Customize DataGridView for jasa
                CustomizeGunaDataGridView();
            }
        }

        private void LoadUserMotors()
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    MotorController motorController = new MotorController(dbContext);

                    // Get the list of motors for the current user
                    var motor = motorController.GetMotorByUserId(Users.CurrentUserId.Value);

                    // Bind to the ComboBox
                    cbPilihMotor.DataSource = motor;
                    cbPilihMotor.DisplayMember = "Name"; // Assuming the motor has a "Name" property
                    cbPilihMotor.ValueMember = "Id";    // Assuming the motor has an "Id" property
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load user motors.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void AddCheckboxColumn()
        {
            if (!dtgJasaPilih.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                dtgJasaPilih.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
            }
        }

        private void CustomizeGunaDataGridView()
        {
            // Styling
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Center the text in the column headers
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgJasaPilih.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgJasaPilih.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center the text in all cells
            dtgJasaPilih.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            // Auto-size rows for multi-line text
            dtgJasaPilih.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set columns to auto-fit their content
            dtgJasaPilih.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Wrap text for cells to prevent clipping
            dtgJasaPilih.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Allow scrollbars to appear when needed
            dtgJasaPilih.ScrollBars = ScrollBars.Both;

            // Adjust column width dynamically after auto-sizing
            foreach (DataGridViewColumn column in dtgJasaPilih.Columns)
            {
                // Set minimum width for readability
                column.MinimumWidth = 80;

                // Distribute extra space proportionally (Fill)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewRow row in dtgJasaPilih.Rows)
            {
                // Set minimum height for readability
                row.MinimumHeight = 20;
            }
        }

        private List<Jasa> GetSelectedJasa()
        {
            var selectedJasa = new List<Jasa>();

            foreach (DataGridViewRow row in dtgJasaPilih.Rows)
            {
                if (row.Cells["Select"] is DataGridViewCheckBoxCell checkboxCell &&
                    checkboxCell.Value is bool isChecked &&
                    isChecked)
                {
                    selectedJasa.Add(new Jasa
                    {
                        Id = Convert.ToInt32(row.Cells["Id"].Value), // Use "Id" instead of "jasa_id"
                        Name = row.Cells["Name"].Value.ToString(),
                        Price = Convert.ToDecimal(row.Cells["Price"].Value),
                        Description = row.Cells["Description"].Value.ToString()
                    });
                }
            }

            return selectedJasa;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPilih_Click(object sender, EventArgs e)
        {
            var selectedJasa = GetSelectedJasa();

            if (selectedJasa.Count == 0)
            {
                MessageBox.Show("Silakan pilih jasa terlebih dahulu.", "Tidak ada jasa yang dipilih", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Display selected jasa in a new DataGridView below
            dtgSelectedJasa.DataSource = selectedJasa;

            // Calculate total price
            decimal totalPrice = selectedJasa.Sum(jasa => jasa.Price);

            // Display total price in tbTotalHarga
            tbTotalHarga.Text = totalPrice.ToString("N0"); // Format as currency (e.g., "1,000")

            MessageBox.Show("Jasa berhasil dipilih!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCo_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Ensure the user selects a motor and a service
                    if (cbPilihMotor.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a motor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dtgSelectedJasa.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("Please select a service.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Get the selected motor ID
                    var selectedMotorId = (int)cbPilihMotor.SelectedValue;

                    // Get the selected service ID (assuming the ID column is in the DataGridView)
                    var selectedRow = dtgSelectedJasa.SelectedRows[0];

                    // Get the current user ID
                    var userId = Users.CurrentUserId.Value;

                    // Calculate total price
                    decimal totalPrice = 0;
                    foreach (DataGridViewRow row in dtgSelectedJasa.Rows)
                    {
                        totalPrice += Convert.ToDecimal(row.Cells["Price"].Value);
                    }

                    // Add the transaction
                    var transactionController = new TransactionController(dbContext);
                    transactionController.AddTransaction(userId, selectedMotorId, _workshopId, "Pending", totalPrice);

                    MessageBox.Show("Transaction successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally clear or refresh UI
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create transaction.\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

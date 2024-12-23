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
            // Styling and auto-resize
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgJasaPilih.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgJasaPilih.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dtgJasaPilih.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            var selectedJasa = GetSelectedJasa();
        }
    }
}

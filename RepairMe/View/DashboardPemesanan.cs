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

                // Customize DataGridView for jasa (implement this method if required)
                CustomizeGunaDataGridView();
            }
        }

        private void CustomizeGunaDataGridView()
        {
            // Clear existing columns
            dtgJasaPilih.Columns.Clear();

            // Add 'Nama Jasa' column
            dtgJasaPilih.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nama",
                DataPropertyName = "Name", // Matches the Jasa property
                HeaderText = "Nama Jasa"
            });

            // Add 'Harga' column
            dtgJasaPilih.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "harga",
                DataPropertyName = "Price", // Matches the Jasa property
                HeaderText = "Harga (Rp)"
            });

            // Add 'Deskripsi' column
            dtgJasaPilih.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "deskripsi",
                DataPropertyName = "Description", // Matches the Jasa property
                HeaderText = "Deskripsi"
            });

            // Styling
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgJasaPilih.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgJasaPilih.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgJasaPilih.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dtgJasaPilih.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

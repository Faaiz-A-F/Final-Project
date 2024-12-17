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
        public PenambahanJasa()
        {
            InitializeComponent();
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
    }
}


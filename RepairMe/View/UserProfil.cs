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
using RepairMe.Model.Repository;

namespace RepairMe.View
{
    public partial class UserProfil : Form
    {
        private int _userId;

        public UserProfil()
        {
            InitializeComponent();
            _userId = Users.CurrentUserId.Value;
            LoadUserData();
            LoadMotorData();
        }

        private void LoadUserData()
        {
            using (var dbContext = new DbContext())
            {
                var userRepository = new UsersRepository(dbContext);
                var currentUser = userRepository.GetCurrentUserOrAdmin();

                if (currentUser != null)
                {
                    tbUsername.Text = currentUser.Username;
                    tbEmail.Text = currentUser.Email;
                    tbAge.Text = currentUser.Age.ToString(); // Null-safe
                    tbPhone.Text = currentUser.Phone;
                    tbAddress.Text = currentUser.Address;
                }
                else
                {
                    MessageBox.Show("Failed to load user data. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close(); // Close the form if no user is logged in
                }
            }
        }
        private void AddCheckboxColumn()
        {
            if (!dtgMotor.Columns.Contains("Select"))
            {
                var checkboxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "Pilih",
                    Width = 50
                };

                dtgMotor.Columns.Insert(0, checkboxColumn); // Add checkbox as the first column
            }
        }

        private void CustomizeGunaDataGridView()
        {
            // Styling and auto-resize
            dtgMotor.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dtgMotor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgMotor.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgMotor.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dtgMotor.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dtgMotor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadMotorData()
        {
            using (var dbContext = new DbContext())
            {
                var motorController = new MotorController(dbContext);
                var motorList = motorController.GetMotorByUserId(_userId);

                dtgMotor.DataSource = motorList;

                // Add a checkbox column
                AddCheckboxColumn();

                // Customize DataGridView for jasa
                CustomizeGunaDataGridView();
            }
        }

        private void btnTambahData_Click(object sender, EventArgs e)
        {
            this.Hide();
            TambahMotorUser tambahMotorUser = new TambahMotorUser();
            tambahMotorUser.Show();
            tambahMotorUser.FormClosed += (s, args) => this.Show();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHapusData_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RepairMe.Model.Context; // Namespace where DbContext is defined

namespace RepairMe
{
    public partial class TestPage : Form
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void btnTestConnextion_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Attempt to open the connection
                    dbContext.OpenConnection();
                    MessageBox.Show("Connection successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
   


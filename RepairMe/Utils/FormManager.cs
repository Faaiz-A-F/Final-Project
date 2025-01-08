using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RepairMe.Utils
{
    internal class FormManager
    {
        private static Form _currentForm;

        public static void ShowForm(Form newForm)
        {
            // Check if the new form is the same as the current form
            if (_currentForm == newForm)
            {
                // If already displayed, just bring it to the front
                _currentForm.BringToFront();
                return;
            }

            // Close and dispose of the current form if it exists
            if (_currentForm != null && !_currentForm.IsDisposed)
            {
                _currentForm.Close(); // Properly close the form
                _currentForm.Dispose();
                _currentForm = null; // Avoid lingering references
            }

            // Update the current form reference
            _currentForm = newForm;

            // Show the new form
            _currentForm.Show();
        }

        public static void CloseCurrentForm()
        {
            if (_currentForm != null && !_currentForm.IsDisposed)
            {
                _currentForm.Close();
                _currentForm.Dispose();
                _currentForm = null; // Reset the reference
            }
        }
    }
}

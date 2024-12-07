using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RepairMe.Model.Context
{
    internal class DbContext : IDisposable
    {
        // Connection string for MySQL database
        private readonly string _connString = "Server=localhost;Database=repairme;Uid=root;Pwd=;";
        private MySqlConnection _conn;

        // Property to access the MySQL connection
        public MySqlConnection Connection
        {
            get
            {
                if (_conn == null)
                {
                    _conn = new MySqlConnection(_connString);
                }
                return _conn;
            }
        }

        // Method to open the connection
        public void OpenConnection()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                {
                    Connection.Open();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error opening connection: {ex.Message}");
                throw;
            }
        }

        // Method to close the connection
        public void CloseConnection()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
                throw;
            }
        }

        // IDisposable implementation to properly release resources
        public void Dispose()
        {
            if (_conn != null)
            {
                try
                {
                    CloseConnection();
                }
                finally
                {
                    _conn.Dispose();
                    _conn = null;
                }
            }
            GC.SuppressFinalize(this);
        }

    }
}

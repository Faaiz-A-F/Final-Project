using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RepairMe.Model.Context;
using RepairMe.Model.Entity;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace RepairMe.Model.Repository
{
    internal class TransactionRepository
    {
        private readonly DbContext _dbContext;

        public TransactionRepository(DbContext context)
        {
            _dbContext = new DbContext();
        }

        public void AddTransaction(Transaction transaction)
        {
            try
            {
                var dbContext = new DbContext();
                dbContext.OpenConnection();

                var query = "INSERT INTO transaction (user_id, motor_id, admin_id, status, total) " +
                            "VALUES (@userId, @motorId, @adminId, @status, @total)";

                using (var cmd = new MySqlCommand(query, dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@userId", transaction.UserId);
                    cmd.Parameters.AddWithValue("@motorId", transaction.MotorId);
                    cmd.Parameters.AddWithValue("@adminId", transaction.AdminId);
                    cmd.Parameters.AddWithValue("@status", transaction.Status);
                    cmd.Parameters.AddWithValue("@total", transaction.Total);

                    cmd.ExecuteNonQuery();
                }

                dbContext.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add transaction: " + ex.Message);
            }
        }

        public void UpdateTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public int GetTransactionUser(int id)
        {
            throw new NotImplementedException();
        }

        public void GetTransactionAdmin(int id)
        {
            throw new NotImplementedException();
        }
    }
}

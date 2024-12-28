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
            try
            {
                var dbContext = new DbContext();
                dbContext.OpenConnection();

                var query = "UPDATE transaction SET status = 'done' WHERE transaction_id = @id";

                using (var cmd = new MySqlCommand(query, dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update transaction: " + ex.Message);
            }
        }

        public List<Transaction> GetTransactionUser(int id)
        {
            var transactionsList = new List<Transaction>();

            try
            {
                _dbContext.OpenConnection();

                var query = "SELECT * FROM transaction WHERE user_id = @id;";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var transaction = new Transaction
                            {
                                Id = reader.GetInt32("transaction_id"),
                                UserId = reader.GetInt32("user_id"),
                                MotorId = reader.GetInt32("motor_id"),
                                AdminId = reader.GetInt32("admin_id"),
                                Status = reader.GetString("status"),
                                Rating = reader.GetInt32("rating"),
                                Review = reader.GetString("review"),
                                Total = reader.GetInt32("total"),
                                Date = reader.GetDateTime("transaction_date")
                            };

                            transactionsList.Add(transaction);
                        }
                    }
                }
                _dbContext.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get transaction: " + ex.Message);
            }

            return transactionsList;
        }

        public List<Transaction> GetPendingTransactionAdmin(int adminId)
        {
            var transactionsList = new List<Transaction>();

            try
            {
                _dbContext.OpenConnection();

                var query = "SELECT * FROM transaction WHERE admin_id = @id AND status = 'pending';";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@id", adminId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var transaction = new Transaction
                            {
                                Id = reader.GetInt32("transaction_id"),
                                UserId = reader.GetInt32("user_id"),
                                MotorId = reader.GetInt32("motor_id"),
                                AdminId = reader.GetInt32("admin_id"),
                                Status = reader.GetString("status"),
                                Total = reader.GetInt32("total"),
                                Date = reader.GetDateTime("transaction_date")
                            };

                            transactionsList.Add(transaction);
                        }
                    }
                }
                _dbContext.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get transaction: " + ex.Message);
            }

            return transactionsList;
        }

        public List<Transaction> GetDoneTransactionAdmin(int adminId)
        {
            var transactionsListDone = new List<Transaction>();

            try
            {
                _dbContext.OpenConnection();

                var query = "SELECT * FROM transaction WHERE admin_id = @id AND status = 'done';";

                using (var cmd = new MySqlCommand(query, _dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@id", adminId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var transaction = new Transaction
                            {
                                Id = reader.GetInt32("transaction_id"),
                                UserId = reader.GetInt32("user_id"),
                                MotorId = reader.GetInt32("motor_id"),
                                AdminId = reader.GetInt32("admin_id"),
                                Status = reader.GetString("status"),
                                Total = reader.GetInt32("total"),
                                Rating = reader.GetInt32("rating"),
                                Review = reader.GetString("review"),
                                Date = reader.GetDateTime("transaction_date")
                            };

                            transactionsListDone.Add(transaction);
                        }
                    }
                }
                _dbContext.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get done transaction: " + ex.Message);
            }

            return transactionsListDone;
        }

        public void AddReview(int id, string review, int rating)
        {
            try
            {
                var dbContext = new DbContext();
                dbContext.OpenConnection();

                var query = "UPDATE transaction SET review = @review, rating = @rating WHERE transaction_id = @id";

                using (var cmd = new MySqlCommand(query, dbContext.Connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@review", review);
                    cmd.Parameters.AddWithValue("@rating", rating);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add review: " + ex.Message);
            }
        }
    }
}

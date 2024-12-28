using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using RepairMe.Model.Entity;
using RepairMe.Model.Repository;
using RepairMe.Model.Context;

namespace RepairMe.Controller
{
    internal class TransactionController
    {
        private readonly TransactionRepository _transactionRepository;

        // Default constructor
        public TransactionController(DbContext context)
        {
            _transactionRepository = new TransactionRepository(context);
        }

        // Constructor for dependency injection
        public TransactionController(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public void AddTransaction(int userId, int motorId, int adminId, string status, decimal total)
        {
            try
            {
                // Validate inputs
                if (userId <= 0)
                {
                    MessageBox.Show("User ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (motorId <= 0)
                {
                    MessageBox.Show("Motor ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    MessageBox.Show("Status cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (total <= 0)
                {
                    MessageBox.Show("Total must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //if (string.IsNullOrWhiteSpace(review))
                //{
                //    MessageBox.Show("Review cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                //if (rating <= 0)
                //{
                //    MessageBox.Show("Rating cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                // Create new transaction object
                Transaction transaction = new Transaction
                {
                    UserId = userId,
                    MotorId = motorId,
                    AdminId = adminId,
                    Status = status,
                    Total = total
                };

                // Add transaction to database
                _transactionRepository.AddTransaction(transaction);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Transaction> GetPendingTransactionAdmin(int adminId)
        {
            try
            {
                // Get transaction by admin ID
                return _transactionRepository.GetPendingTransactionAdmin(adminId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<Transaction> GetDoneTransactionAdmin(int adminId)
        {
            try
            {
                // Get transaction by admin ID
                return _transactionRepository.GetDoneTransactionAdmin(adminId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<Transaction> GetTransactionUser(int userId)
        {
            try
            {
                // Get transaction by admin ID
                return _transactionRepository.GetTransactionUser(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void UpdateTransaction(int id)
        {
            try
            {
                // Validate input
                if (id <= 0)
                {
                    MessageBox.Show("Transaction ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update transaction status
                _transactionRepository.UpdateTransaction(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddReview(int transactionId, string review, int rating)
        {
            try
            {
                // Validate inputs
                if (transactionId <= 0)
                {
                    MessageBox.Show("Transaction ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(review))
                {
                    MessageBox.Show("Review cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rating <= 0)
                {
                    MessageBox.Show("Rating cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create new review object
                Transaction transaction = new Transaction
                {
                    Id = transactionId,
                    Review = review,
                    Rating = rating
                };

                // Add review to database
                _transactionRepository.AddReview(transactionId, review, rating);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

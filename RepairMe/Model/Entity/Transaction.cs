using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairMe.Model.Entity
{
    internal class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MotorId { get; set; }
        public int AdminId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public decimal Total { get; set; }

        // Additional properties
        public string Username { get; set; } // For the user's name
        public string Motorname { get; set; } // For the motor's name
    }
}

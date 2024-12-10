using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairMe.Model.Entity
{
    internal class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}

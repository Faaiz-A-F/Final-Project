using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairMe.Model.Entity
{
    internal class Motor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Engine { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public string Year { get; set; }
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Dominiotic.Entities.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Orders = new HashSet<Orders>();
            Receivables = new HashSet<Receivables>();
        }

        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Registered { get; set; }
        public bool? Active { get; set; }
        public Guid? Identifier { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public ICollection<Receivables> Receivables { get; set; }
    }
}

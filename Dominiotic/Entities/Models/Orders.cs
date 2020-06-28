using System;
using System.Collections.Generic;

namespace Dominiotic.Entities.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrdersItems = new HashSet<OrdersItems>();
        }

        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Registered { get; set; }
        public bool? Active { get; set; }
        public Guid? Identifier { get; set; }

        public Clients Client { get; set; }
        public ICollection<OrdersItems> OrdersItems { get; set; }
    }
}

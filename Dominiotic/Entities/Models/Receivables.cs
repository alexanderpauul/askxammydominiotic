using System;
using System.Collections.Generic;

namespace Dominiotic.Entities.Models
{
    public partial class Receivables
    {
        public int ReceivableId { get; set; }
        public int ClientId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Registered { get; set; }
        public Guid? Identifier { get; set; }

        public Clients Client { get; set; }
    }
}

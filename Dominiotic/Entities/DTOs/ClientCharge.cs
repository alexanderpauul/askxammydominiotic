using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominiotic.Entities.DTOs
{
    public class ClientCharge
    {
        public int ClientId { get; set; }

        public decimal Amount { get; set; }
    }
}

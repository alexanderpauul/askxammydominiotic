using System;
using System.Collections.Generic;

namespace Dominiotic.Entities.Models
{
    public partial class Plates
    {
        public Plates()
        {
            PlatesItems = new HashSet<PlatesItems>();
        }

        public int PlateId { get; set; }
        public string PlateName { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Registered { get; set; }
        public Guid? Identifier { get; set; }

        public ICollection<PlatesItems> PlatesItems { get; set; }
    }
}

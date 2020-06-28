using System;
using System.Collections.Generic;

namespace Dominiotic.Entities.Models
{
    public partial class Ingredients
    {
        public Ingredients()
        {
            OrdersItems = new HashSet<OrdersItems>();
            PlatesItems = new HashSet<PlatesItems>();
        }

        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Registered { get; set; }
        public Guid? Identifier { get; set; }

        public ICollection<OrdersItems> OrdersItems { get; set; }
        public ICollection<PlatesItems> PlatesItems { get; set; }
    }
}

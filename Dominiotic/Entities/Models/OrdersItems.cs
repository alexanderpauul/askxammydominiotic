﻿using System;
using System.Collections.Generic;

namespace Dominiotic.Entities.Models
{
    public partial class OrdersItems
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int IngredientId { get; set; }
        public DateTime? Registered { get; set; }
        public Guid? Identifier { get; set; }

        public Ingredients Ingredient { get; set; }
        public Orders Order { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Dominiotic.Entities.Models
{
    public partial class Establishment
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string CoAddress { get; set; }
        public DateTime? Registered { get; set; }
        public Guid? Identifier { get; set; }
    }
}

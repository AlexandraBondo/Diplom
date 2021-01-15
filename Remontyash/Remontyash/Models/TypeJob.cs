using System;
using System.Collections.Generic;

#nullable disable

namespace Remontyash.Models
{
    public partial class TypeJob
    {
        public TypeJob()
        {
            Orders = new HashSet<Order>();
        }

        public Guid TypeJobId { get; set; }
        public string Description { get; set; }
        public Guid TypeTechnicId { get; set; }
        public decimal Cost { get; set; }

        public virtual TypeTechnic TypeTechnic { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

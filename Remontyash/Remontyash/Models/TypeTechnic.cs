using System;
using System.Collections.Generic;

#nullable disable

namespace Remontyash.Models
{
    public partial class TypeTechnic
    {
        public TypeTechnic()
        {
            TypeJobs = new HashSet<TypeJob>();
        }

        public Guid TypeTechnicId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TypeJob> TypeJobs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Remontyash.Models
{
    public partial class Emp
    {
        public Emp()
        {
            Orders = new HashSet<Order>();
            Users = new HashSet<User>();
        }

        public Guid Empid { get; set; }
        [Display(Name = "ФИО")]
        public string Fio { get; set; }
        [Display(Name = "Должность")]
        public string Position { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Remontyash.Models
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public Guid ClientId { get; set; }
        [Display(Name = "ФИО")]
        public string Fio { get; set; }
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

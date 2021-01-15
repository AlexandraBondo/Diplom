using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Remontyash.Models
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        [Display(Name = "Тип работ")]
        public Guid TypeJobId { get; set; }
        [Display(Name = "Клиент")]
        public Guid ClientId { get; set; }
        [Display(Name = "Сотрудник")]
        public Guid Empid { get; set; }
        [Display(Name = "Принято в работу")]
        public DateTime Accepted { get; set; }
        [Display(Name = "Завершено")]
        public DateTime? Completed { get; set; }
        [Display(Name = "Завершена работа?")]
        public bool IsCompleted { get; set; }

        public virtual Client Client { get; set; }
        public virtual Emp Emp { get; set; }
        public virtual TypeJob TypeJob { get; set; }
    }
}

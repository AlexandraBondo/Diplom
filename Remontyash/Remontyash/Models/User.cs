using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Remontyash.Models
{
    public partial class User
    {
        public Guid Userid { get; set; }
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Сотрудник")]
        public Guid Empid { get; set; }
        [Display(Name = "Роль")]
        public Guid Roleid { get; set; }

        public virtual Emp Emp { get; set; }
        public virtual Role Role { get; set; }
    }
}

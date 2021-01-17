using Remontyash.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Remontyash.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Роль не выбрана")]
        public Guid Roleid { get; set; }

        public virtual Role Roles { get; set; }
    }
}

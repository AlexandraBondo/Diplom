﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Remontyash.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public Guid RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

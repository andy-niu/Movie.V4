﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserRoleRelation = new HashSet<UserRoleRelation>();
        }

        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<UserRoleRelation> UserRoleRelation { get; set; }
    }
}

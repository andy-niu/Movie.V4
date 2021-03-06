﻿using System;
using System.Collections.Generic;

namespace M.Repository.Entity
{
    public partial class UserRoleRelation : ITrackable
    {
        public Guid UserRoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual User User { get; set; }
    }
}

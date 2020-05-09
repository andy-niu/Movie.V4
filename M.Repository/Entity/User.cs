using System;
using System.Collections.Generic;

namespace M.Repository.Entity
{
    public partial class User
    {
        public User()
        {
            UserRoleRelation = new HashSet<UserRoleRelation>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }

        public virtual ICollection<UserRoleRelation> UserRoleRelation { get; set; }
    }
}

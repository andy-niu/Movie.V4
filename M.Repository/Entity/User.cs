using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace M.Repository.Entity
{
    public partial class User : ITrackable
    {
        //public User()
        //{
        //    UserRoleRelation = new HashSet<UserRoleRelation>();
        //}

        public Guid UserId { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }

        [JsonIgnore]
        public ICollection<UserRoleRelation> UserRoleRelation { get; set; }

        [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}

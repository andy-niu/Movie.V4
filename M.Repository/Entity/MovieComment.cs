using System;
using System.Collections.Generic;

namespace M.Repository.Entity
{
    public partial class MovieComment
    {
        public int CommentId { get; set; }
        public int? Parentid { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string ToUserName { get; set; }
        public int? ToUserId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserIp { get; set; }
        public string PcitureUrl { get; set; }
        public int? MovieId { get; set; }

        public virtual MovieBase Movie { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace M.Repository.Entity
{
    public partial class MovieAttributes
    {
        public int AttributesId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int? ParentId { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

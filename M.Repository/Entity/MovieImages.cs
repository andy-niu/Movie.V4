using System;
using System.Collections.Generic;

namespace M.Repository.Entity
{
    public partial class MovieImages : ITrackable
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int? Status { get; set; }
        public string Name { get; set; }
        public string VirtualPath { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? OldId { get; set; }
        public string Domain { get; set; }
    }
}

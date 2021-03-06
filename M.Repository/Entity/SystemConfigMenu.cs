﻿using System;
using System.Collections.Generic;

namespace M.Repository.Entity
{
    public partial class SystemConfigMenu : ITrackable
    {
        public int Id { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public int? Parentid { get; set; }
        public string Link { get; set; }
    }
}

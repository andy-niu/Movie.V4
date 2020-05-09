using System;
using System.Collections.Generic;
using System.Text;

namespace M.Model.ViewModels
{
    public class MovieBaseViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string AliasTitle { get; set; }
        public decimal? Score { get; set; }
        public string Summary { get; set; }
        public string Actor { get; set; }
        public int? Time { get; set; }
        public string Resolution { get; set; }
        public string Type { get; set; }
        public string TypeAttributes { get; set; }
        public string TypeAttributesDisplay { get; set; }
        public string Region { get; set; }
        public string RegionAttributes { get; set; }
        public string RegionAttributesDisplay { get; set; }
        public string DownloadUri { get; set; }
        public string ThumbUri { get; set; }
        public string MovieUri { get; set; }
        public int? OldId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Source { get; set; }
        public int? Views { get; set; }
        public int? Status { get; set; }
        public decimal? DoubanScore { get; set; }
    }
}

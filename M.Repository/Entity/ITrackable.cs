using System;
using System.Collections.Generic;
using System.Text;

namespace M.Repository.Entity
{
    public interface ITrackable
    {
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }

    public enum OrderBy
    {
        Asc,
        Desc
    }
}

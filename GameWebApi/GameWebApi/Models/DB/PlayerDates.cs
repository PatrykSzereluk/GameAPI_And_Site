using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class PlayerDates
    {
        public int PlayerId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime? BanDate { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }

        public virtual PlayerIdentity Player { get; set; }
    }
}

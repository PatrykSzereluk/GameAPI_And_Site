using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class PlayerBans
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public byte BanReason { get; set; }
        public string BanMessage { get; set; }
        public DateTime BeginBanDate { get; set; }
        public DateTime EndBanDate { get; set; }
        public bool IsActive { get; set; }

        public virtual PlayerIdentity Player { get; set; }
    }
}

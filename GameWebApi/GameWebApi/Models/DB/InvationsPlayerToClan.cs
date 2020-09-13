using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class InvationsPlayerToClan
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int ClanId { get; set; }

        public virtual Clans Clan { get; set; }
        public virtual PlayerIdentity Player { get; set; }
    }
}

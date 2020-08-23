using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class ClanMembers
    {
        public int ClanId { get; set; }
        public int PlayerId { get; set; }
        public byte Function { get; set; }
        public DateTime DateOfJoin { get; set; }

        public virtual Clans Clan { get; set; }
    }
}

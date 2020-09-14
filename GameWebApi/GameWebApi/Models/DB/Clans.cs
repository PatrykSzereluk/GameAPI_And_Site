using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class Clans
    {
        public Clans()
        {
            ClanMembers = new HashSet<ClanMembers>();
            InvationsPlayerToClan = new HashSet<InvationsPlayerToClan>();
        }

        public int Id { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public byte? AvatarId { get; set; }
        public string AvatarUrl { get; set; }

        public virtual ClanStatistics ClanStatistics { get; set; }
        public virtual ICollection<ClanMembers> ClanMembers { get; set; }
        public virtual ICollection<InvationsPlayerToClan> InvationsPlayerToClan { get; set; }
    }
}

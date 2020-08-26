using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class ClanStatistics
    {
        public int ClanId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public virtual Clans Clan { get; set; }
    }
}

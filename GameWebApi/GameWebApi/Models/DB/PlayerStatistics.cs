using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class PlayerStatistics
    {
        public int PlayerId { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GameLose { get; set; }

        public virtual PlayerIdentity Player { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Ranking.Models
{
    public class ClanRankingResponseModel
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int Experience { get; set; }
    }
}

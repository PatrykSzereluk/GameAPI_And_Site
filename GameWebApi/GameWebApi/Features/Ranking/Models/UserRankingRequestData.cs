namespace GameWebApi.Features.Ranking.Models
{
    public class UserRankingRequestData
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int RankingCategory { get; set; } // TODO ENUM
        public bool Order { get; set; }
    }
}

namespace GameWebApi.Features.Ranking.Models
{
    public class UserRankingResponseData
    {
        public int Place { get; set; }
        public string NickName { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public double KD { get; set; }
        public int Assists { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLose { get; set; }
    }
}

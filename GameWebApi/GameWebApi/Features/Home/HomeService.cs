namespace GameWebApi.Features.Home
{
    using System.Threading.Tasks;
    using Models;
    using GameWebApi.Models.DB;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;

    public class HomeService : IHomeService
    {
        private readonly GameDBContext _context;

        public HomeService(GameDBContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<InitialResponseData> GetInitialDate(InitialRequestData requestData)
        {
            var result = new InitialResponseData();

            var res = await _context.PlayerStatistics.FirstOrDefaultAsync(t => t.PlayerId == requestData.PlayerId);

            if (res == null) return result;

            result.Kills = res.Kills;
            result.Deaths = res.Deaths;
            result.Assists = res.Assists;
            result.GamesPlayed = res.GamesPlayed;
            result.GamesWon = res.GamesWon;
            result.GameLose = res.GameLose;
            result.PlayerId = res.PlayerId;

            return result;
        }
    }
}

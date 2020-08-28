using Microsoft.AspNetCore.Authorization;

namespace GameWebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GameWebApi.Features.Ranking.Models;
    using Features.Ranking;
    using Microsoft.AspNetCore.Mvc;

    public class RankingController : ApiController
    {

        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            this._rankingService = rankingService;
        }
        [Authorize]
        [Route(nameof(GetUserRanking))]
        [HttpPost]
        public async Task<IEnumerable<UserRankingResponseData>>  GetUserRanking(RankingRequestData rankingModel)
        {
            var z = Request;
            return await _rankingService.GetUserRanking(rankingModel);
        }

        [Route(nameof(GetClanRanking))]
        [HttpPost]
        public async Task<IEnumerable<ClanRankingResponseModel>> GetClanRanking(RankingRequestData rankingModel)
        {
            return await _rankingService.GetClanRanking(rankingModel);
        }
    }
}

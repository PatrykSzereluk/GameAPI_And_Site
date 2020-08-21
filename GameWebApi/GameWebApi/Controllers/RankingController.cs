
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GameWebApi.Features.Ranking.Models;
    using Features.Ranking;

    public class RankingController : ApiController
    {

        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            this._rankingService = rankingService;
        }
        [Route(nameof(GetUserRanking))]
        [HttpPost]
        public async Task<IEnumerable<UserRankingResponseData>>  GetUserRanking(UserRankingRequestData rankingModel)
        {
            var z = Request;
            return await _rankingService.GetUserRanking(rankingModel);
        }


    }
}

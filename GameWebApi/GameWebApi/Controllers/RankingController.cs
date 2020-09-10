using GameWebApi.Features.Email;
using Microsoft.AspNetCore.Authorization;
using NLog;

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
        private readonly IEmailService _emailService;
        private static Logger _logger = LogManager.GetLogger("loggerRole"); 

        public RankingController(IRankingService rankingService, IEmailService emailService)
        {
            this._rankingService = rankingService;
            this._emailService = emailService;
        }

        [Route(nameof(GetUserRanking))]
        [HttpPost]
        public async Task<IEnumerable<UserRankingResponseData>>  GetUserRanking(RankingRequestData rankingModel)
        {
            var z = Request;
            _logger.Info("Start GetUserRanking");
            // _emailService.SendEmailToUser("zczc", "xcv"); // test
            _logger.Info("End GetUserRanking");
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

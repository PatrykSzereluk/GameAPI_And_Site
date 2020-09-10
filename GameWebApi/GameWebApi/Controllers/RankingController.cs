﻿using GameWebApi.Features.Email;
using GameWebApi.Features.Email.Models;
using GameWebApi.Features.Utility;
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
        private readonly IEmailService _emailService;

        public RankingController(IRankingService rankingService, IEmailService emailService)
        {
            this._rankingService = rankingService;
            this._emailService = emailService;
        }

        [Route(nameof(GetUserRanking))]
        [HttpPost]
        public async Task<IEnumerable<UserRankingResponseData>>  GetUserRanking(RankingRequestData rankingModel)
        {
            //SendEmailToUser(string userEmail, string message, EmailType emailType, EmailData data = null)
            await _emailService.SendEmailToUser("asdsd","asdsaasd",EmailType.Welcome,new EmailData(){NickName = "Jakiś tam user", PlayerId = 5}); // test

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

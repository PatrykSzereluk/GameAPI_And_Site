using System;
using System.Threading.Tasks;
using GameWebApi.Features.Home;
using GameWebApi.Features.Home.Models;
using GameWebApi.Features.Identity;

namespace GameWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RankingController : ApiController
    {

        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            this._rankingService = rankingService;
        }



    }
}

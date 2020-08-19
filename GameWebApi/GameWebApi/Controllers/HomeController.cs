using System;
using System.Threading.Tasks;
using GameWebApi.Features.Home;
using GameWebApi.Features.Home.Models;
using GameWebApi.Features.Identity;

namespace GameWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {

        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            this._homeService = homeService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return Ok("Works");
        }

        [Route(nameof(GetInitialData))]
        public async Task<InitialResponseData> GetInitialData(InitialRequestData data)
        {
            return await _homeService.GetInitialDate(data);
        }
        

    }
}

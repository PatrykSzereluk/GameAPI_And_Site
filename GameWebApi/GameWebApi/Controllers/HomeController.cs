namespace GameWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Features.Home;
    using GameWebApi.Features.Home.Models;
    using Features.User;
    using Features.User.Model;

    public class HomeController : ApiController
    {

        private readonly IHomeService _homeService;
        private readonly IUserService _userService;

        public HomeController(IHomeService homeService, IUserService userService)
        {
            this._homeService = homeService;
            this._userService = userService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return Ok("Works");
        }
        [Authorize]
        [HttpPost]
        [Route(nameof(GetInitialData))]
        public async Task<InitialResponseData> GetInitialData(InitialRequestData model)
        {
            return await _homeService.GetInitialDate(model);
        }

    }
}

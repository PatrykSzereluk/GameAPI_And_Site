namespace GameWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {
        [Authorize]
        public IActionResult Index()
        {
            return Ok("Works");
        }
    }
}

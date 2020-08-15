using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi.Controllers
{
    public class HomeController : ApiController
    {
        [Authorize]
        public IActionResult Index()
        {
            return Ok("Works");
        }
    }
}

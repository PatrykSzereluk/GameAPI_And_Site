using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Models.DB;
using GameWebApi.Models.Features.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GameWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IIdentityService identityService;
        

        public WeatherForecastController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerIdentity>> Get()
        {
            identityService.Login();
            return await identityService.Register();
        }
    }
}


//  Scaffold-DbContext "Server=.;Database=GameDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB 
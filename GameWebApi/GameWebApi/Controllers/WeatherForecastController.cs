using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Models;
using GameWebApi.Models.DB;
using GameWebApi.Models.Features.Identity;
using GameWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GameWebApi.Controllers
{

    public class WeatherForecastController : ApiController
    {

        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;

        public WeatherForecastController(UserManager<User> userManager,IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
        }

        [Route(nameof(Register))]
        [HttpGet]
        public async Task<IEnumerable<PlayerIdentity>> Register(RegisterRequestModel model)
        {
            return await identityService.Register(model);
        }
        [Route(nameof(Login))]
        [HttpPost]
       // public async Task<PlayerIdentity> Login(UserInfo userInfo)
        public async Task<UserLoginResponse> Login(UserInfo userInfo)
       {
           identityService.Login1();
             return await identityService.Login(userInfo);
        }


    }
}


//  Scaffold-DbContext "Server=.;Database=GameDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Models;
using GameWebApi.Models.DB;
using GameWebApi.Models.Features.Identity;
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

        [HttpGet]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
            //identityService.Login();
           // return await identityService.Register();
        }

        [HttpPost]
       // public async Task<PlayerIdentity> Login(UserInfo userInfo)
        public async Task<UserLoginResponse> Login(UserInfo userInfo)
        {
             return await identityService.Login(userInfo);
        }


    }
}


//  Scaffold-DbContext "Server=.;Database=GameDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB 
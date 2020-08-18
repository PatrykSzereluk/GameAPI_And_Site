namespace GameWebApi.Controllers
{
    using System.Threading.Tasks;
    using Models.DB;
    using Models.Features.Identity;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class IdentityController : ApiController
    {

        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [Route(nameof(Register))]
        [HttpGet]
        public async Task<bool> Register(RegisterRequestModel model)
        {
            return await _identityService.Register(model);
        }
        [Route(nameof(Login))]
        [HttpPost]
        public async Task<UserLoginResponse> Login(UserInfo userInfo)
       {
           _identityService.Login1();

          return await _identityService.Login(userInfo);
        }


    }
}


//  Scaffold-DbContext "Server=.;Database=GameDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB 
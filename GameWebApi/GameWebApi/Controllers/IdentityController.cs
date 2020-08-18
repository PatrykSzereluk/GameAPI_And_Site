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

        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
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
        public async Task<UserLoginResponse> Login(UserInfo userInfo)
       {
           identityService.Login1();
             return await identityService.Login(userInfo);
        }


    }
}


//  Scaffold-DbContext "Server=.;Database=GameDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB 
using GameWebApi.Features.User.Model;

namespace GameWebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using GameWebApi.Features.Identity.Models;
    using Features.Identity;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [Route(nameof(Register))]
        [HttpPost]
        public async Task<UserRegisterResponseModel> Register(UserRegisterRequestModel model)
        {
            return await _identityService.Register(model);
        }

        [Route(nameof(Login))]
        [HttpPost]
        public async Task<UserLoginResponse> Login(UserLoginRequest userInfo)
        {
           return await _identityService.Login(userInfo);
        }

        [Route(nameof(ChangePassword))]
        [HttpPost]
        public async Task<bool> ChangePassword(ChangePasswordRequestModel model)
        {
            return await _identityService.ChangePassword(model);
        }
    }
}


//  Scaffold-DbContext "Server=.;Database=GameDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB 
//  Scaffold-DbContext "Server=.;Database=Test;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB -context GameDBContext -Project GameWebApi -force


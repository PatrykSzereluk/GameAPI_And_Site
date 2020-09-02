using GameWebApi.Features;
using Microsoft.AspNetCore.Authorization;

namespace GameWebApi.Controllers
{
    using System.Threading.Tasks;
    using Features.User;
    using Features.User.Model;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [Route(nameof(ChangeNickName))]
        [HttpPost]
        public async Task<bool> ChangeNickName(ChangeNickNameRequestModel model)
        {
            return await _userService.ChangeNickName(model);
        }

        [Authorize]
        [Route(nameof(ChangePassword))]
        [HttpPost]
        public async Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel model)
        {
            return await _userService.ChangePassword(model);
        }

        [Route(nameof(GetUserDetails))]
        [HttpPost]
        public async Task<UserDetailsResponseModel> GetUserDetails(BaseRequestData data)
        {
            return await _userService.GetUserDetails(data);
        }

    }
}

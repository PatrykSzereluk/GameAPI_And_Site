using GameWebApi.Features;
using GameWebApi.Features.Email.Models;
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

        [Route(nameof(ConfirmUserEmail))]
        [HttpPost]
        public async Task<ConfirmEmailResponseModel> ConfirmUserEmail(ChangeUserParamRequestModel data)
        {
            return await _userService.ConfirmUserEmail(data.PlayerId, data.PlayerHash);
        }

        [Route(nameof(ChangePasswordByEmailFirstStep))]
        [HttpPost]
        public async Task<bool> ChangePasswordByEmailFirstStep(ChangePasswordByEmailRequestModel model)
        {
            return await _userService.ChangePasswordByEmailFirstStep(model);
        }

        [Route(nameof(CanChangePasswordByEmail))]
        [HttpPost]
        public async Task<bool> CanChangePasswordByEmail(ChangeUserParamRequestModel model)
        {
            return await _userService.CanChangePasswordByEmail(model);
        }

        [Route(nameof(ChangePasswordByEmailSecondStep))]
        [HttpPost]
        public async Task<bool> ChangePasswordByEmailSecondStep(ChangePasswordSStepRequestModel model)
        {
            return await _userService.ChangePasswordByEmailSecondStep(model);
        }
    }
}

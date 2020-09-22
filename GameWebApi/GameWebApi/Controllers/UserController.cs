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

        [HttpDelete]
        [Route(nameof(DeletePlayerAccount))]
        public async Task<bool> DeletePlayerAccount(BaseRequestData model)
        {
            return await _userService.DeletePlayerAccount(model);
        }

        [HttpPost]
        [Route(nameof(CheckLoginExists))]
        public async Task<bool> CheckLoginExists(CheckLoginRequestModel model)
        {
            return await _userService.CheckLogin(model);
        }

        [HttpPost]
        [Route(nameof(CheckNickNameExists))]
        public async Task<bool> CheckNickNameExists(CheckNickNameRequestModel model)
        {
            return await _userService.CheckNickName(model);
        }

        [HttpPost]
        [Route(nameof(CheckEmailExists))]
        public async Task<bool> CheckEmailExists(CheckEmailRequestModel model)
        {
            return await _userService.CheckEmail(model);
        }
    }
}

﻿namespace GameWebApi.Controllers
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

        [Route(nameof(ChangeNickName))]
        [HttpPost]
        public async Task<bool> ChangeNickName(ChangeNickNameRequestModel model)
        {
            return await _userService.ChangeNickName(model);
        }

        [Route(nameof(ChangePassword))]
        [HttpPost]
        public async Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel model)
        {
            return await _userService.ChangePassword(model);
        }

    }
}

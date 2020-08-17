using GameWebApi.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Models.Features.Identity;

namespace GameWebApi.Services.Interfaces
{
    public interface IIdentityService
    {
        void Login1();
        Task<IEnumerable<PlayerIdentity>> Register(RegisterRequestModel newPlayer);

        Task<UserLoginResponse> Login(UserInfo userInfo);

    }
}

using GameWebApi.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Models.Features.Identity
{
    public interface IIdentityService
    {

        Task<IEnumerable<PlayerIdentity>> Register(RegisterRequestModel newPlayer);

        Task<UserLoginResponse> Login(UserInfo userInfo);

    }
}

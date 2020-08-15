using GameWebApi.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Models.Features.Identity
{
    public interface IIdentityService
    {

        Task<IEnumerable<PlayerIdentity>> Register();

        Task<UserLoginResponse> Login(UserInfo userInfo);

    }
}

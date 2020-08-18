namespace GameWebApi.Services.Interfaces
{
    using Models.DB;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Features.Identity;

    public interface IIdentityService
    {
        void Login1();
        Task<bool> Register(RegisterRequestModel newPlayer);

        Task<UserLoginResponse> Login(UserInfo userInfo);

    }
}

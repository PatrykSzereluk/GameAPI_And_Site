namespace GameWebApi.Features.Identity
{
    using System.Threading.Tasks;
    using Models;

    public interface IIdentityService
    {
        Task<UserRegisterResponseModel> Register(UserRegisterRequestModel newPlayer);

        Task<UserLoginResponse> Login(UserLoginRequest userInfo);

    }
}

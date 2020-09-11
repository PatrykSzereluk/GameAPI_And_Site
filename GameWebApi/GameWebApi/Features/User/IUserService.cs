namespace GameWebApi.Features.User
{
    using System.Threading.Tasks;
    using Model;
    using GameWebApi.Features.Email.Models;
    public interface IUserService
    {
        Task<bool> ChangeNickName(ChangeNickNameRequestModel model);
        Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel model);
        Task<UserDetailsResponseModel> GetUserDetails(BaseRequestData data);
        Task<ConfirmEmailResponseModel> ConfirmUserEmail(int id, string playerHash);
    }
}

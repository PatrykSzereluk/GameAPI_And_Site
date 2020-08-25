namespace GameWebApi.Features.User
{
    using System.Threading.Tasks;
    using Model;
    public interface IUserService
    {
        Task<bool> CheckUserBan(int playerId);
        Task<bool> BanPlayer(BanPlayerRequestModel model);
        Task<bool> ChangeNickName(ChangeNickNameRequestModel model);
        Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel model);
    }
}

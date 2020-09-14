namespace GameWebApi.Features.User
{
    using System.Threading.Tasks;
    using Model;
    using GameWebApi.Features.Email.Models;
    using GameWebApi.Models.DB;

    public interface IUserService
    {
        Task<bool> ChangeNickName(ChangeNickNameRequestModel model);
        Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel model);
        Task<UserDetailsResponseModel> GetUserDetails(BaseRequestData data);
        Task<ConfirmEmailResponseModel> ConfirmUserEmail(int id, string playerHash);
        Task<PlayerIdentity> GetPlayerById(int playerId);
        Task<bool> ChangePasswordByEmailFirstStep(ChangePasswordByEmailRequestModel model);
        Task<bool> CanChangePasswordByEmail(ChangeUserParamRequestModel model);
        Task<bool> ChangePasswordByEmailSecondStep(ChangePasswordSStepRequestModel model);
    }
}

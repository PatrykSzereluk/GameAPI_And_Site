namespace GameWebApi.Features.Ban
{
    using System.Threading.Tasks;
    using User.Model;
    public interface IBanService
    {
        Task<bool> CheckUserBan(int playerId);
        Task<bool> BanPlayer(BanPlayerRequestModel model);
        Task<bool> CancelBan(BanPlayerRequestModel model);
    }
}

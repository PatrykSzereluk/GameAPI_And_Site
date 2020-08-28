namespace GameWebApi.Features.Ban
{
    using Models;
    using System.Threading.Tasks;
    public interface IBanService
    {
        Task<bool> CheckUserBan(int playerId);
        Task<bool> BanPlayer(BanPlayerRequestModel model);
        Task<bool> CancelBan(int playerId);
    }
}

namespace GameWebApi.Features.Ranking
{
    using System.Collections.Generic;
    using Models;
    using System.Threading.Tasks;

    public interface IRankingService
    {
        Task<IEnumerable<UserRankingResponseData>> GetUserRanking(RankingRequestData model);
        Task<IEnumerable<ClanRankingResponseModel>> GetClanRanking(RankingRequestData rankingModel);
    }
}

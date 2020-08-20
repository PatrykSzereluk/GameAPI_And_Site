namespace GameWebApi.Features.Ranking
{
    using System.Collections.Generic;
    using Models;
    using System.Threading.Tasks;

    public interface IRankingService
    {
        Task<IEnumerable<UserRankingResponseData>> GetUserRanking(UserRankingRequestData model);
    }
}

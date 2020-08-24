namespace GameWebApi.Features.Ranking
{
    using Sql.Interfaces;
    using GameWebApi.Sql.Helpers;
    using GameWebApi.Models.DB;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using System;
    using System.Data;
    using System.Linq;
    using GameWebApi.Sql.Models;

    public class RankingService : IRankingService
    {
        private readonly GameDBContext _context;
        private readonly ISqlManager _sqlManager;
        public RankingService(GameDBContext context,
                              ISqlManager sqlManager)
        {
            _context = context;
            _sqlManager = sqlManager;
        }

        public async Task<IEnumerable<UserRankingResponseData>> GetUserRanking(UserRankingRequestData rankingModel)
        {

            var takeParam = rankingModel.Take.ToSqlParameter("Take");
            var skipParam = rankingModel.Take.ToSqlParameter("Skip");
            var rankingCategoryParam = rankingModel.Take.ToSqlParameter("RankingCategory");
            var orderParam = rankingModel.Take.ToSqlParameter("Order");

            var result = await _sqlManager.ExecuteDataCommand("[Web].[GetRanking]", CommandType.StoredProcedure, null, takeParam,skipParam,rankingCategoryParam,orderParam);

            return await GetUserRankingData(result);
        }

        private async Task<IEnumerable<UserRankingResponseData>> GetUserRankingData(TableSets data)
        {
            var res = await Task.Run(() =>
            {
                var result = new List<UserRankingResponseData>();
                var tmpDate = data.Elements.First();

                foreach (var row in tmpDate.Rows)
                {
                    result.Add(
                        new UserRankingResponseData
                        {
                            Place = Convert.ToInt32(row.Elements[0]),
                            NickName = Convert.ToString(row.Elements[1]),
                            Kills = Convert.ToInt32(row.Elements[2]),
                            Deaths = Convert.ToInt32(row.Elements[3]),
                            Assists = Convert.ToInt32(row.Elements[4]),
                            GamesPlayed = Convert.ToInt32(row.Elements[5]),
                            GamesWon = Convert.ToInt32(row.Elements[6]),
                            GameLose = Convert.ToInt32(row.Elements[7])
                        });
                }
                return result;
            });
            return res;
        }

    }
}

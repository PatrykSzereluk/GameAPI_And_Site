using Microsoft.Data.SqlClient;

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



        public async Task<IEnumerable<ClanRankingResponseModel>> GetClanRanking(RankingRequestData rankingModel)
        {

            var result = await _sqlManager.ExecuteDataCommand("[Web].[GetClanRanking]", CommandType.StoredProcedure, null, GetSqlParameters(rankingModel));

            return await GetClanRankingData(result);
        }



        public async Task<IEnumerable<UserRankingResponseData>> GetUserRanking(RankingRequestData rankingModel)
        {


            var result = await _sqlManager.ExecuteDataCommand("[Web].[GetRanking]", CommandType.StoredProcedure, null, GetSqlParameters(rankingModel));

            return await GetUserRankingData(result);
        }


        private async Task<IEnumerable<ClanRankingResponseModel>> GetClanRankingData(TableSets data)
        {
            return await Task.Run(() =>
            {
                var result = new List<ClanRankingResponseModel>();
                var tmpDate = data.Elements.First();

                foreach (var row in tmpDate.Rows)
                {
                    result.Add(
                        new ClanRankingResponseModel
                        {
                            Name = Convert.ToString(row.Elements[0]),
                            Wins = Convert.ToInt32(row.Elements[1]),
                            Losses = Convert.ToInt32(row.Elements[2]),
                            Draws = Convert.ToInt32(row.Elements[3]),
                            Experience = Convert.ToInt32(row.Elements[4])
                        });
                }
                return result;
            });
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
                            KD = Convert.ToDouble(row.Elements[3]),
                            GamesPlayed = Convert.ToInt32(row.Elements[4]),
                            GamesWon = Convert.ToInt32(row.Elements[5]),
                            ClanName = Convert.ToString(row.Elements[6])
                        });
                }
                return result;
            });
            return res;
        }

        private SqlParameter[] GetSqlParameters(RankingRequestData rankingModel)
        {
            return new[] {
                rankingModel.Take.ToSqlParameter("Take"),
                rankingModel.Skip.ToSqlParameter("Skip"),
                rankingModel.RankingCategory.ToSqlParameter("RankingCategory"),
                rankingModel.Order.ToSqlParameter("Order")
            };
        }
    }
}

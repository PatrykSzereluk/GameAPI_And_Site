using System.Data;
using GameWebApi.Sql.Helpers;
using GameWebApi.Sql.Interfaces;
using Microsoft.Data.SqlClient;

namespace GameWebApi.Features.Clan
{
    using Core.Enums;
    using GameWebApi.Features.Clan.Models;
    using GameWebApi.Models.DB;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ClanService : IClanService
    {
        private readonly GameDBContext _context;
        private readonly ISqlManager _sqlManager;
        public ClanService(GameDBContext context, ISqlManager sqlManager)
        {
            _context = context;
            _sqlManager = sqlManager;
        }

        public async Task<NewClanResponseModel> AddNewClan(NewClanRequestModel model)
        {
            NewClanResponseModel response = new NewClanResponseModel()
            {
                IsSuccess = true,
                IsNameValid = true,
                IsAcronymValid = true,
                IsLeaderValid = true
            };

            #region EF
            if (await CheckName(model.Name))
            {
                response.IsNameValid = false;
            }

            if (await CheckAcronym(model.Acronym))
            {
                response.IsAcronymValid = false;
            }

            if (await CheckLeader(model.PlayerId))
            {
                response.IsLeaderValid = false;
            }

            if (response.IsAcronymValid == false || response.IsNameValid == false || response.IsLeaderValid == false)
            {
                response.IsSuccess = false;
                return response;
            }

            Clans newClan = new Clans();
            newClan.Acronym = model.Acronym;
            newClan.Name = model.Name;
            newClan.Experience = 0;
            newClan.AvatarId = model.AvatarId;
            newClan.AvatarUrl = model.AvatarUrl;

            var result = await _context.Clans.AddAsync(newClan);

            if (result.State != EntityState.Added)
            {
                response.IsSuccess = false;
                return response;
            }

            await _context.SaveChangesAsync();

            var clanEntity = await _context.Clans.FirstOrDefaultAsync(t => t.Acronym == model.Acronym && t.Name == model.Name);

            var isLeaderValid = await AddClanLeader(model.PlayerId, clanEntity.Id, ClanFunction.Leader);

            response.IsLeaderValid = isLeaderValid;

            if (!response.IsLeaderValid)
            {
                _context.Clans.Remove(clanEntity);
                await _context.SaveChangesAsync();
                response.IsSuccess = false;
            }

            #endregion
            #region SqlManager
            //var result = await _sqlManager.ExecuteDataCommand("[Common].[AddNewClan]",
            //                                                CommandType.StoredProcedure,
            //                                                null,
            //                                                new SqlParameter[]
            //                                                        {
            //                                                            model.Acronym.ToSqlParameter("Acronym"),
            //                                                            model.Name.ToSqlParameter("Name"),
            //                                                            model.AvatarId.ToSqlParameter("AvatarId"),
            //                                                            model.AvatarUrl.ToSqlParameter("AvatarUrl")
            //                                                        });


            //var resultRows = result.Elements.First().Rows.First().Elements;

            //response.IsNameValid = Convert.ToBoolean(resultRows[0]);
            //response.IsAcronymValid = Convert.ToBoolean(resultRows[1]);

            //var clanId = Convert.ToInt32(resultRows[2]);
            #endregion

            return response;
        }

        private async Task<bool> AddClanLeader(int playerId, int clanId, ClanFunction clanFunction)
        {
            NewMemberToClanRequestModel model = new NewMemberToClanRequestModel()
            {
                PlayerId = playerId,
                ClanId = clanId,
                ClanFunction = clanFunction
            };

            var result = await AddMemberToClan(model);

            if (!result.ExistsClan || result.playerHasClan)
                return false;

            return true;
        }

        public async Task<NewMemberToClanResponseModel> AddMemberToClan(NewMemberToClanRequestModel model)
        {
            var result = await _sqlManager.ExecuteDataCommand("[Common].[AddMemberToClan]",
                CommandType.StoredProcedure,
                null,
                new[]
                {
                    model.PlayerId.ToSqlParameter("PlayerId"),
                    model.ClanId.ToSqlParameter("ClanId"),
                    ((byte)model.ClanFunction).ToSqlParameter("Function"),
                    DateTime.Now.ToSqlParameter("DateOfJoin")
                });

            var elements = result.Elements.First().Rows.First().Elements;


            NewMemberToClanResponseModel response = new NewMemberToClanResponseModel()
            {
                playerHasClan = Convert.ToBoolean(elements[0]),
                ExistsClan = Convert.ToBoolean(elements[1])
            };

            return response;
        }

        private async Task<bool> CheckName(string name)
        {
            var result = await _context.Clans.FirstOrDefaultAsync(t => t.Name == name);
            return result != null;
        }

        private async Task<bool> CheckAcronym(string acronym)
        {
            var result = await _context.Clans.FirstOrDefaultAsync(t => t.Acronym == acronym);
            return result != null;
        }

        private async Task<bool> CheckLeader(int playerId)
        {
            var result = await _context.ClanMembers.FirstOrDefaultAsync(t => t.PlayerId == playerId);
            return result != null;
        }

    }
}

namespace GameWebApi.Features.Clan
{
    using Core.Enums;
    using Models;
    using GameWebApi.Models.DB;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Data;
    using GameWebApi.Sql.Helpers;
    using Sql.Interfaces;
    using User;
    using Utility.Logging;
    using System.Collections.Generic;

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
            var response = new NewClanResponseModel()
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

            var newClan = new Clans
            {
                Acronym = model.Acronym,
                Name = model.Name,
                Experience = 0,
                AvatarId = model.AvatarId,
                AvatarUrl = model.AvatarUrl
            };

            var result = await _context.Clans.AddAsync(newClan);

            if (result.State != EntityState.Added)
            {
                response.IsSuccess = false;
                return response;
            }

            await _context.SaveChangesAsync();

            var clanEntity = await _context.Clans.FirstOrDefaultAsync(t => t.Acronym == model.Acronym && t.Name == model.Name);

            bool isLeaderValid = false;

            try
            {
                isLeaderValid = await AddClanLeader(model.PlayerId, clanEntity.Id, ClanFunction.Leader);
            }
            catch(Exception e) { Logger.GetInstance().Error(e.Message); }
            finally
            {
                response.IsLeaderValid = isLeaderValid;

                if (!response.IsLeaderValid)
                {
                    _context.Clans.Remove(clanEntity);
                    await _context.SaveChangesAsync();
                    response.IsSuccess = false;
                }
            }

            if (!response.IsLeaderValid)
            {
                return response;
            }

            var clanStatistics = new ClanStatistics()
            {
                ClanId = clanEntity.Id,
                Draws = 0,
                Losses = 0,
                Wins = 0
            };

            var csResult = await _context.ClanStatistics.AddAsync(clanStatistics);

            if (csResult.State != EntityState.Added)
            {
                var leaderEntity = await _context.ClanMembers.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);

                _context.ClanMembers.Remove(leaderEntity);
                _context.Clans.Remove(clanEntity);
                response.IsSuccess = false;
            }

            await _context.SaveChangesAsync();

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

        private async Task<bool> RemoveAllClanMembers(int clanId, bool forceRemove)
        {
            var clan = await _context.Clans.FirstOrDefaultAsync(t => t.Id == clanId);

            if (clan == null)
                return false;

            var members = await _context.ClanMembers.Where(t => t.ClanId == clanId && t.Function != (byte)ClanFunction.Leader).ToListAsync();

            foreach (var member in members)
            {
                var result = _context.ClanMembers.Remove(member); // I need result object, RemoveRange is void

                if (result.State != EntityState.Deleted)
                {
                    return false;
                }
            }

            if (forceRemove)
                await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> AddClanLeader(int playerId, int clanId, ClanFunction clanFunction)
        {
            var model = new NewMemberToClanRequestModel()
            {
                PlayerId = playerId,
                ClanId = clanId,
                ClanFunction = clanFunction
            };

            var result = await AddMemberToClan(model);

            return result.IsSuccess;
        }

        public async Task<bool> ModifyMemberFunction(ModifyMemberRequestModel model)
        {
            if (model.ClanFunction == ClanFunction.Leader)
                return false;

            var clanMember = await _context.ClanMembers.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);

            if (clanMember == null)
                return false;

            clanMember.Function = (byte) model.ClanFunction;

            var result = _context.ClanMembers.Update(clanMember);

            if (result.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
                return true;
            }
                

            return false;
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


            var response = new NewMemberToClanResponseModel()
            {
                playerHasClan = Convert.ToBoolean(elements[0]),
                ExistsClan = Convert.ToBoolean(elements[1]),
                IsSuccess = Convert.ToBoolean(elements[2])
            };

            return response;
        }

        public async Task<bool> RemoveClan(RemoveClanRequestModel model, bool lateDelete)
        {
            var leader = await _context.ClanMembers.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId && t.ClanId == model.ClanId);

            if(leader == null)
                return false;

            var removeMembers = await RemoveAllClanMembers(model.ClanId, !lateDelete);

            if (!removeMembers)
                return false;

            var clan = await _context.Clans.FirstOrDefaultAsync(t => t.Id == model.ClanId);
            
            var removeStatistics = await _context.ClanStatistics.FirstOrDefaultAsync(t => t.ClanId == clan.Id);

            if (removeStatistics == null)
                return false;

            if (clan == null)
                return false;

            var invitations = await _context.InvationsPlayerToClan.Where(t => t.ClanId == model.ClanId).ToListAsync();

            if(invitations != null)
                 _context.InvationsPlayerToClan.RemoveRange(invitations);

            var leaderResult = _context.ClanMembers.Remove(leader);
            var statsResult = _context.ClanStatistics.Remove(removeStatistics);
            var clanResult = _context.Clans.Remove(clan);

            if (leaderResult.State == EntityState.Deleted && clanResult.State == EntityState.Deleted && statsResult.State == EntityState.Deleted)
            {
                if(!lateDelete)
                    await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveMember(RemoveUserRequestModel model, bool lateDelete)
        {
            var clanMember = await _context.ClanMembers.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId && t.ClanId == model.ClanId);

            if (clanMember == null)
                return false;

            var result = _context.ClanMembers.Remove(clanMember);

            if (result.State == EntityState.Deleted)
            {
                if(!lateDelete)
                    await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteInvitation(ClanInviteRequestModel model)
        {
            var invitation = await 
                _context.InvationsPlayerToClan.FirstOrDefaultAsync(t =>
                    t.PlayerId == model.PlayerId && t.ClanId == model.ClanId);

            if (invitation == null) return false;

            var deleteResult = _context.InvationsPlayerToClan.Remove(invitation);

            if (deleteResult.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return true;
        }


        public async Task<bool> SendClanInvitationToPlayer(ClanInviteRequestModel model)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == model.PlayerId);

            var clan = await _context.Clans.FirstOrDefaultAsync(t => t.Id == model.ClanId);

            if (user == null || clan == null) return false;

            if (await CheckIfExistsInvitation(model)) return false;

            var inviteEntity = new InvationsPlayerToClan {ClanId = model.ClanId, PlayerId = model.PlayerId};

            var addResult = await _context.InvationsPlayerToClan.AddAsync(inviteEntity);

            if (addResult.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private async Task<bool> CheckIfExistsInvitation(ClanInviteRequestModel model)
        {
            return await 
                _context.InvationsPlayerToClan.AnyAsync(t =>
                    t.PlayerId == model.PlayerId && t.ClanId == model.ClanId);
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

        public async Task<IEnumerable<InvationsPlayerToClan>> GetInvitationList(BaseRequestData model)
        {
            return await _context.InvationsPlayerToClan.Where(t => t.PlayerId == model.PlayerId).ToListAsync();
        }
    }
}

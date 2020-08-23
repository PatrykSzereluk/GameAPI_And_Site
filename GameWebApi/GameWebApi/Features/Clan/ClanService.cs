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
        public ClanService(GameDBContext context)
        {
            _context = context;
        }

        public async Task<NewClanResponseModel> AddNewClan(NewClanRequestModel model)
        {
            NewClanResponseModel response = new NewClanResponseModel()
            {
                IsSuccess = true,
                IsNameValid = true,
                IsAcronymValid = true
            };

            if (await CheckName(model.Name))
            {
                response.IsNameValid = false;
            }

            if (await CheckAcronym(model.Acronym))
            {
                response.IsAcronymValid = false;
            }

            if(response.IsAcronymValid == false || response.IsNameValid == false)
            {
                response.IsSuccess = false;
                return response;
            }

            Clans newClan = new Clans();
            newClan.Acronym = model.Acronym;
            newClan.Name = model.Name;
            newClan.Experience = 0;
            newClan.AvatarId = model.AvatarId;
            newClan.AvatarUrl = model.AvatarURL;

            var result = await _context.Clans.AddAsync(newClan);

            if(result.State != EntityState.Added)
            {
                response.IsSuccess = false;
                return response;
            }
            await _context.SaveChangesAsync();

            var clanEntity = await _context.Clans.FirstOrDefaultAsync(t => t.Acronym == model.Acronym && t.Name == model.Name);

            var isLeaderValid = await AddClanLeader(model.PlayerId, clanEntity.Id, ClanFunction.Leader);

            response.IsLeaderValid = isLeaderValid;

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

            return await AddMemberToClan(model);
        }

        public async Task<bool> AddMemberToClan(NewMemberToClanRequestModel model)
        {
            ClanMembers newMember = new ClanMembers()
            {
                PlayerId = model.PlayerId,
                ClanId = model.ClanId,
                Function = (byte)model.ClanFunction,
                DateOfJoin = DateTime.Now
            };

            var result = await _context.ClanMembers.AddAsync(newMember);

            if (result.State == EntityState.Added)
                return true;

            return false;
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

    }
}

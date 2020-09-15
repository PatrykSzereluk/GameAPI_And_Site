using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GameWebApi.Features.User
{
    using System.Text;
    using Security;
    using System.Threading.Tasks;
    using Model;
    using Models.DB;
    using Microsoft.EntityFrameworkCore;
    using GameWebApi.Features.Email.Models;
    using Email;
    using Core.Enums;
    using Clan;
    using GameWebApi.Features.Clan.Models;

    public class UserService : IUserService
    {
        private readonly GameDBContext _context;
        private readonly IEncrypter _encrypter;
        private readonly IEmailService _emailService;
        private readonly IClanService _clanService;

        public UserService(GameDBContext context, IEncrypter encrypter, IEmailService emailService, IClanService clanService)
        {
            _context = context;
            _encrypter = encrypter;
            _emailService = emailService;
            _clanService = clanService;
        }

        public async Task<PlayerIdentity> GetPlayerById(int playerId)
        {
            return await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == playerId);
        }

        public async Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel model)
        {
            var player = await GetPlayerById(model.PlayerId);

            if (player == null) return new ChangePasswordResponseModel(){IsSuccess = false, BadPassword = false};

            var passwordSb = new StringBuilder();

            passwordSb.Append(_encrypter.Encrypted(model.Password)); 

            var salt = await _context.PlayerSalt.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);

            passwordSb.Append(salt.Salt);

            if(passwordSb.ToString() != player.Password) return new ChangePasswordResponseModel() { IsSuccess = false, BadPassword = true };

            var newPassword = _encrypter.Encrypted(model.NewPassword);

            player.Password = newPassword;

            var dbResult = _context.PlayerIdentity.Update(player);

            if (dbResult.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
                return new ChangePasswordResponseModel() { IsSuccess = true, BadPassword = false };
            }

            return new ChangePasswordResponseModel() { IsSuccess = false, BadPassword = false };
        }

        public async Task<bool> CanChangePasswordByEmail(ChangeUserParamRequestModel model)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t =>
                t.Id == model.PlayerId && model.PlayerHash == t.PlayerHash);

            if (user == null) return false;

            return user.PasswordChanging;
        }

        public async Task<bool> ChangePasswordByEmailFirstStep(ChangePasswordByEmailRequestModel model)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Email == model.Email);

            if (user == null) return false;

            user.PasswordChanging = true;

            var addResult = _context.PlayerIdentity.Update(user);

            if (addResult.State != EntityState.Modified) return false;

            await _context.SaveChangesAsync();

            await _emailService.SendEmailToUser(model.Email, "", EmailType.ChangePassword, new EmailData() {NickName = user.Nick, PlayerHash = user.PlayerHash, PlayerId = user.Id });

            return true;
        }

        public async Task<bool> ChangePasswordByEmailSecondStep(ChangePasswordSStepRequestModel model)
        {
            var player = await _context.PlayerIdentity.FirstOrDefaultAsync(t =>
                t.Id == model.PlayerId && model.PlayerHash == t.PlayerHash);

            if (player == null) return false;
            if (player.PasswordChanging == false) return false;

            var passwordSb = new StringBuilder();

            passwordSb.Append(_encrypter.Encrypted(model.Password));

            var salt = await _context.PlayerSalt.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);

            passwordSb.Append(salt.Salt);

            player.Password = passwordSb.ToString();
            player.PasswordChanging = false;

            var dbResult = _context.PlayerIdentity.Update(player);
            if (dbResult.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePlayerAccount(BaseRequestData model)
        {
            var playerEntity = await GetPlayerById(model.PlayerId);

            if (playerEntity == null) return false;

            var clanMemberEntity = await _context.ClanMembers.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);
            bool deleteClanResult = false;

            if (clanMemberEntity != null)
            {
                if (clanMemberEntity.Function == (byte) ClanFunction.Leader)
                {
                    deleteClanResult = await _clanService.RemoveClan(new RemoveClanRequestModel()
                        { ClanId = clanMemberEntity.ClanId, PlayerId = model.PlayerId }, true);
                }
                else
                {
                    deleteClanResult = await _clanService.RemoveMember(new RemoveUserRequestModel(){ ClanId = clanMemberEntity.ClanId, PlayerId = model.PlayerId }, true);
                }
            }

            var playerStatisticsEntity = await _context.PlayerStatistics.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);
            var playerSaltEntity = await _context.PlayerSalt.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);
            var playerBansEntity = await _context.PlayerBans.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);
            var playerDatesEntity = await _context.PlayerDates.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId);

            EntityEntry<PlayerBans> deleteResultBansEntity = null;

            if (playerBansEntity != null)
            {
                deleteResultBansEntity = _context.PlayerBans.Remove(playerBansEntity);
            }

            var deleteResultPlayerStatistics = _context.PlayerStatistics.Remove(playerStatisticsEntity);
            var deleteResultDatesEntity = _context.PlayerDates.Remove(playerDatesEntity);
            var deleteResultPlayerSalt = _context.PlayerSalt.Remove(playerSaltEntity);
            var deleteResultPlayerEntity = _context.PlayerIdentity.Remove(playerEntity);

            if (deleteResultPlayerStatistics.State == EntityState.Deleted &&
                deleteResultDatesEntity.State == EntityState.Deleted &&
                deleteResultPlayerSalt.State == EntityState.Deleted &&
                deleteResultPlayerEntity.State == EntityState.Deleted && 
                (deleteResultBansEntity == null ||  deleteResultBansEntity.State == EntityState.Deleted) &&
                ((clanMemberEntity != null && deleteClanResult) || clanMemberEntity == null))
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<UserDetailsResponseModel> GetUserDetails(BaseRequestData data)
        {
            var userExists = await GetPlayerById(data.PlayerId);

            if (userExists != null)
            {
                var result = new UserDetailsResponseModel();
                var playerStats = await _context.PlayerStatistics.FirstOrDefaultAsync(t => t.PlayerId == userExists.Id);

                result.Kills = playerStats.Kills;
                result.Deaths = playerStats.Deaths;
                result.Assists = playerStats.Assists;
                result.GamesPlayed = playerStats.GamesPlayed;
                result.GamesWon = playerStats.GamesWon;
                result.GameLose = playerStats.GameLose;
                
                var clanMemberEntity = await _context.ClanMembers.FirstOrDefaultAsync(t => t.PlayerId == userExists.Id);

                if (clanMemberEntity != null)
                {
                    result.Function = clanMemberEntity.Function;
                    result.DateOfJoin = clanMemberEntity.DateOfJoin;

                    var clanEntity = await _context.Clans.FirstOrDefaultAsync(t => t.Id == clanMemberEntity.ClanId);

                    result.AvatarId = clanEntity.AvatarId;
                    result.Acronym = clanEntity.Acronym;
                    result.AvatarUrl = clanEntity.AvatarUrl;
                    result.Experience = clanEntity.Experience;
                    result.Name = clanEntity.Name;

                    var clanStatsEntity = await _context.ClanStatistics.FirstOrDefaultAsync(t => t.ClanId == clanEntity.Id);

                    result.Wins = clanStatsEntity.Wins;
                    result.Losses = clanStatsEntity.Losses;
                    result.Draws = clanStatsEntity.Draws;

                }

                return result;
            }

            return new UserDetailsResponseModel();
        }

        public async Task<ConfirmEmailResponseModel> ConfirmUserEmail(int id, string playerHash)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == id && t.PlayerHash == playerHash);

            var response = new ConfirmEmailResponseModel(){NotFound = false, Confirmed = false, IsSuccess = false};

            if (user == null) {
                response.NotFound = true;
                return response;
            }

            if (user.EmailConfirmed == true)
            {
                response.Confirmed = true;
                return response;
            }

            user.EmailConfirmed = true;

            var result = _context.PlayerIdentity.Update(user);

            if (result.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
                response.IsSuccess = true;
                return response;
            }

            return response;
        }

        public async Task<bool> ChangeNickName(ChangeNickNameRequestModel model)
        {
            var player = await GetPlayerById(model.PlayerId);

            if (player == null) return false;

            var existNickName = await _context.PlayerIdentity.AnyAsync(t => t.Nick == model.NewNickName);

            if (existNickName)
            {
                return false;
            }
            else
            {
                player.Nick = model.NewNickName;

                var dbResult = _context.PlayerIdentity.Update(player);

                if (dbResult.State == EntityState.Modified)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}

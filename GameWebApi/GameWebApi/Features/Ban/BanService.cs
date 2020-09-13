namespace GameWebApi.Features.Ban
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using GameWebApi.Models.DB;
    using Microsoft.EntityFrameworkCore;
    using GameWebApi.Features.User;

    public class BanService : IBanService
    {
        private readonly GameDBContext _context;
        private readonly IUserService _userService;
        public BanService(GameDBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        public async Task<BanPlayerResponseModel> BanPlayer(BanPlayerRequestModel model)
        {
            var response = new BanPlayerResponseModel() {IsActiveBan = false, PlayerNotFound = false };

            var banEntity = await _context.PlayerBans.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId && t.IsActive);

            if (banEntity != null && banEntity.IsActive) return new BanPlayerResponseModel() { IsActiveBan = true, PlayerNotFound = false, IsSuccess = false };

            var userEntity = _userService.GetPlayerById(model.PlayerId);

            if (userEntity == null)
                return new BanPlayerResponseModel() { IsActiveBan = false, PlayerNotFound = true, IsSuccess = false };

            PlayerBans playerBans = new PlayerBans()
            {
                PlayerId = model.PlayerId,
                BanReason = (byte)model.BanReason,
                BanMessage = model.BanMessage,
                BeginBanDate = DateTime.Now,
                EndBanDate = model.EndBanDate,
                IsActive = true
            };

            var result = await _context.PlayerBans.AddAsync(playerBans);

            if (result.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return new BanPlayerResponseModel() { IsActiveBan = false, PlayerNotFound = false, IsSuccess = true };
            }

            return new BanPlayerResponseModel() { IsActiveBan = false, PlayerNotFound = false, IsSuccess = false };
        }

        public async Task<bool> CancelBan(int playerId)
        {
            var banEntity = await _context.PlayerBans.FirstOrDefaultAsync(t => t.PlayerId == playerId && t.IsActive);
            if (banEntity == null) return false;

            banEntity.IsActive = false;
            banEntity.Cancelled = true;

            var result =  _context.PlayerBans.Update(banEntity);

            if (result.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CheckUserBan(int playerId)
        {
            var result = await _context.PlayerBans.FirstOrDefaultAsync(t => t.PlayerId == playerId);

            if (result != null)
            {
                if (await CheckBanDate(result)) return false;

                if (result.IsActive) return true;
            }

            return false;
        }

        private async Task<bool> CheckBanDate(PlayerBans result)
        {
            if (result.EndBanDate == DateTime.Now.Date)
            {
                result.IsActive = false;
                var dbResult = _context.PlayerBans.Update(result);
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

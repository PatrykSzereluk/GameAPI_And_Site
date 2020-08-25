using System;

namespace GameWebApi.Features.User
{
    using System.Threading.Tasks;
    using Model;
    using Models.DB;
    using Microsoft.EntityFrameworkCore;
    public class UserService : IUserService
    {
        private readonly GameDBContext _context;

        public UserService(GameDBContext context)
        {
            _context = context;
        }

        public async Task<bool> BanPlayer(BanPlayerRequestModel model)
        {

            var banEntity = await _context.PlayerBans.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId && t.IsActive);

            if (banEntity != null && banEntity.IsActive) return false; // sprecyzować jaki błąd

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
                return true;
            }

            return false;
        }

        public async Task<bool> CheckUserBan(int playerId)
        {
            var result = await _context.PlayerBans.FirstOrDefaultAsync(t => t.PlayerId == playerId);

            if (result != null)
            {
                if(await CheckBanDate(result)) return false;

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

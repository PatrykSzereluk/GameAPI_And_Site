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
            PlayerBans playerBans = new PlayerBans()
            {
                PlayerId = model.PlayerId,
                BanReason = model.BanReason,
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
                if (result.IsActive) return true;
            }

            return false;
        }
    }
}

namespace GameWebApi.Features.User
{
    using System.Text;
    using System;
    using Security;
    using System.Threading.Tasks;
    using Model;
    using Models.DB;
    using Microsoft.EntityFrameworkCore;
    public class UserService : IUserService
    {
        private readonly GameDBContext _context;
        private readonly IEncrypter _encrypter;

        public UserService(GameDBContext context, IEncrypter encrypter)
        {
            _context = context;
            _encrypter = encrypter;
        }

        public async Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel model)
        {
            var player = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == model.PlayerId);

            if(player == null) return new ChangePasswordResponseModel(){IsSuccess = false, BadPassword = false};

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

        public async Task<bool> ChangeNickName(ChangeNickNameRequestModel model)
        {
            var player = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == model.PlayerId);

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


        public async Task<bool> BanPlayer(BanPlayerRequestModel model)
        {
            var banEntity = await _context.PlayerBans.FirstOrDefaultAsync(t => t.PlayerId == model.PlayerId && t.IsActive);

            if (banEntity != null && banEntity.IsActive) return false; // sprecyzować jaki błąd -- gracz już zbanowany

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

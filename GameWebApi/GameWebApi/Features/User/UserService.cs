namespace GameWebApi.Features.User
{
    using System.Text;
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

        public async Task<bool> GetUserDetails(BaseRequestData data)
        {
            var userExists = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == data.PlayerId);

            if (userExists != null)
            {

            }

            return false;
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
    }
}

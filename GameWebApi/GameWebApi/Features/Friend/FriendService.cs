using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Features.Friend.Models;
using Microsoft.EntityFrameworkCore;

namespace GameWebApi.Features.Friend
{
    using GameWebApi.Models.DB;

    public class FriendService : IFriendService
    {
        private readonly GameDBContext _context;
        public FriendService(GameDBContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<FriendResponseModel>> GetFriends(BaseRequestData data)
        {
            var friendIds = await _context.Friends.Where(t => t.OwnerPlayerId == data.PlayerId).Select(t => t.FriendPlayerId).ToListAsync();

            var friends = await _context.PlayerIdentity.Where(t => friendIds.Contains(t.Id)).ToListAsync();

            var responseFriends = new List<FriendResponseModel>();

            foreach (var friend in friends)
            {
                responseFriends.Add(new FriendResponseModel()
                {
                    PlayerId = friend.Id,
                    NickName = friend.Nick,
                    IsActive = false
                });
            }
            return responseFriends;
        }

        public async Task<bool> AddNewFriend(FriendBaseRequestModel model)
        {
            if (model.FriendId == model.PlayerId) return false;

            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == model.PlayerId);

            if (user == null) return false;

            if (await _context.Friends.AnyAsync(t => t.Id == model.PlayerId && t.FriendPlayerId == model.FriendId))
                return false;

            var friendEntity = new Friends() {FriendPlayerId = model.FriendId, OwnerPlayerId = model.PlayerId};

            var addResult = await _context.Friends.AddAsync(friendEntity);

            if (addResult.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteFriend(FriendBaseRequestModel model)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == model.PlayerId);

            if (user == null) return false;

            var friendEntity = await _context.Friends.FirstOrDefaultAsync(t =>
                t.FriendPlayerId == model.FriendId && t.OwnerPlayerId == model.PlayerId);

            if (friendEntity == null) return false;

            var deleteResult = _context.Friends.Remove(friendEntity);

            if (deleteResult.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRangeFriend(DeleteRangeFriendRequestModel model)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Id == model.PlayerId);

            if (user == null) return false;

            var friendsEntity = await _context.Friends.Where(t => model.FriendIds.Contains(t.FriendPlayerId) && t.OwnerPlayerId == model.PlayerId).ToListAsync();

            if (friendsEntity == null) return false;

            _context.RemoveRange(friendsEntity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

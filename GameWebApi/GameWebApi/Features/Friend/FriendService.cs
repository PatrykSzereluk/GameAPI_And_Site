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

        public Task<bool> AddNewFriend(FriendBaseRequestModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteFriend(BaseRequestData model)
        {
            throw new System.NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Features.Friend.Models;

namespace GameWebApi.Features.Friend
{
    public interface IFriendService
    {
        Task<IEnumerable<FriendResponseModel>> GetFriends(BaseRequestData data);
        Task<bool> AddNewFriend(FriendBaseRequestModel model);
        Task<bool> DeleteFriend(FriendBaseRequestModel model);

        Task<bool> DeleteRangeFriend(FriendBaseRequestModel model);
    }
}

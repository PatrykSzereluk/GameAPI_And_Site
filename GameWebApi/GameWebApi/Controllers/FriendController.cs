using Microsoft.AspNetCore.Mvc;

namespace GameWebApi.Controllers
{
    using System.Threading.Tasks;
    using Features;
    using Features.Friend;
    using GameWebApi.Features.Friend.Models;
    using System.Collections.Generic;
    public class FriendController : ApiController
    {
        private readonly IFriendService _friendService;
        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet]
        [Route(nameof(GetFriends))]
        public async Task<IEnumerable<FriendResponseModel>> GetFriends(BaseRequestData data)
        {
            return await _friendService.GetFriends(data);
        }

        [HttpPost]
        [Route(nameof(AddNewFriend))]
        public async Task<bool> AddNewFriend(FriendBaseRequestModel data)
        {
            return await _friendService.AddNewFriend(data);
        }

        [HttpDelete]
        [Route(nameof(DeleteFriend))]
        public async Task<bool> DeleteFriend(FriendBaseRequestModel data)
        {
            return await _friendService.DeleteFriend(data);
        }
    }
}

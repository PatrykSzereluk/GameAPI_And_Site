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

        public async Task<IEnumerable<FriendResponseModel>> GetFriends(BaseRequestData data)
        {
            return await _friendService.GetFriends(data);
        }
    }
}

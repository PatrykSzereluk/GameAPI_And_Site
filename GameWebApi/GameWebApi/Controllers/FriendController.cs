using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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

        [HttpDelete]
        [Route(nameof(DeleteRangeFriend))]
        public async Task<bool> DeleteRangeFriend([FromBody] DeleteRangeFriendRequestModel data)
        {
            return await _friendService.DeleteRangeFriend(data);
        }

        [HttpPost, RequestSizeLimit(6000000)]
        [Route(nameof(UploadImage))]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine(Directory.GetCurrentDirectory(), "images");

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();
                    var fullPath = Path.Combine(folderName, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);

                    file.CopyTo(stream);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
                    

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

    }
}

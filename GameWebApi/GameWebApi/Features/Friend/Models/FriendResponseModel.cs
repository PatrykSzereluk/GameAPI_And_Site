using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Friend.Models
{
    public class FriendResponseModel
    {
        public int PlayerId { get; set; }
        public string NickName { get; set; }
        public bool IsActive { get; set; }
    }
}

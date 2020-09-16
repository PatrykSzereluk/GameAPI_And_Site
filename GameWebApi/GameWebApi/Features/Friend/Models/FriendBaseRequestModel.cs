using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Friend.Models
{
    public class FriendBaseRequestModel : BaseRequestData
    {
        public int FriendId { get; set; }  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.User.Model
{
    public class BanPlayerRequestModel
    {
        public int PlayerId { get; set; }
        public byte BanReason { get; set; }
        public string BanMessage { get; set; }
        public DateTime EndBanDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;

namespace GameWebApi.Features.User.Model
{
    public class BanPlayerRequestModel
    {
        public int PlayerId { get; set; }
        public BanReason BanReason { get; set; }
        public string BanMessage { get; set; }
        public DateTime EndBanDate { get; set; }
    }
}

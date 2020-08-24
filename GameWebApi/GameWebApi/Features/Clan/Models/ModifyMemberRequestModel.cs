using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;

namespace GameWebApi.Features.Clan.Models
{
    public class ModifyMemberRequestModel : BaseRequestData
    {
        public ClanFunction ClanFunction { get; set; }
    }
}

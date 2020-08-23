using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Clan.Models
{
    public class NewMemberToClanRequestModel : BaseRequestData
    {
        public int ClanId { get; set; }
        public ClanFunction ClanFunction { get; set; }
    }
}

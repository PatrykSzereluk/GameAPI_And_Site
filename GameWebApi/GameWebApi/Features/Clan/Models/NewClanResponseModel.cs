using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Clan.Models
{
    public class NewClanResponseModel
    {
        public bool IsSuccess { get; set; }
        public bool IsNameValid { get; set; }
        public bool IsAcronymValid { get; set; }
    }
}

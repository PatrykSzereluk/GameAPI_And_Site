using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Clan.Models
{
    public class NewMemberToClanResponseModel
    {

        public bool playerHasClan { get; set; }
        public bool ExistsClan { get; set; }
        public bool IsSuccess { get; set; }

    }
}

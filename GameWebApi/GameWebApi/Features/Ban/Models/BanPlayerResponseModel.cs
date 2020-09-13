using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Ban.Models
{
    public class BanPlayerResponseModel : BaseResponseModel
    {
        public bool IsActiveBan { get; set; }
    }
}

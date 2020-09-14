using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Email.Models
{
    public class ChangeUserParamRequestModel : BaseRequestData
    {
        public string PlayerHash { get; set; }
    }
}

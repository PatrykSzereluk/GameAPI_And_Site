using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.User.Model
{
    public class ChangePasswordResponseModel
    {
        public bool IsSuccess { get; set; }
        public bool BadPassword { get; set; }
    }
}

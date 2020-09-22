using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.User.Model
{
    public class CheckLoginRequestModel
    {
        public string Login { get; set; }
    }

    public class CheckNickNameRequestModel
    {
        public string NickName { get; set; }
    }

    public class CheckEmailRequestModel
    {
        public string Email { get; set; }
    }
}

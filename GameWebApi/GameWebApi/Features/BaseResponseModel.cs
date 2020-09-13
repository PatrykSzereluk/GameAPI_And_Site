using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features
{
    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public bool PlayerNotFound { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Models.Features.Identity
{
    public class RegisterResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}

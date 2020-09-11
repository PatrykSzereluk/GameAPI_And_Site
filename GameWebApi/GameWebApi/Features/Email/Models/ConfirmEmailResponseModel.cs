using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Email.Models
{
    public class ConfirmEmailResponseModel
    {
        public bool IsSuccess { get; set; }
        public bool NotFound { get; set; }
        public bool Confirmed { get; set; }
    }
}

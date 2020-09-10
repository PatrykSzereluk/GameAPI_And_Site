using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Email.Models
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Definition{ get; set; }
    }
}

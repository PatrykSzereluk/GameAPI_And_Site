using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Email.Models
{
    public class EmailData
    {
        public int PlayerId { get; set; }
        public string NickName { get; set; }
        public string PlayerHash { get; set; }
    }
}

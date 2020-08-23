using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Clan.Models
{
    public class NewClanRequestModel
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public byte AvatarId { get; set; }
        public string AvatarURL { get; set; }
    }
}

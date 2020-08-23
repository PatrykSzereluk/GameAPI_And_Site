using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class Clans
    {
        public int Id { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public byte? AvatarId { get; set; }
        public string AvatarUrl { get; set; }
    }
}

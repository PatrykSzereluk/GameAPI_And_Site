using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class PlayerIdentity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public string GameToken { get; set; }

        public virtual PlayerDates PlayerDates { get; set; }
    }
}

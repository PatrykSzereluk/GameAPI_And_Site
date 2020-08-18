using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class PlayerSalt
    {
        public int PlayerId { get; set; }
        public string Salt { get; set; }

        public virtual PlayerIdentity Player { get; set; }
    }
}

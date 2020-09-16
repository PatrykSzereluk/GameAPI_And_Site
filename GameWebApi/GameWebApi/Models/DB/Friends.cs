using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class Friends
    {
        public int Id { get; set; }
        public int OwnerPlayerId { get; set; }
        public int FriendPlayerId { get; set; }

        public virtual PlayerIdentity OwnerPlayer { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace GameWebApi.Models.DB
{
    public partial class PlayerIdentity
    {
        public PlayerIdentity()
        {
            Friends = new HashSet<Friends>();
            InvationsPlayerToClan = new HashSet<InvationsPlayerToClan>();
            PlayerBans = new HashSet<PlayerBans>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PlayerHash { get; set; }
        public string GameToken { get; set; }
        public byte RoleType { get; set; }
        public bool PasswordChanging { get; set; }

        public virtual ClanMembers ClanMembers { get; set; }
        public virtual PlayerDates PlayerDates { get; set; }
        public virtual PlayerSalt PlayerSalt { get; set; }
        public virtual PlayerStatistics PlayerStatistics { get; set; }
        public virtual ICollection<Friends> Friends { get; set; }
        public virtual ICollection<InvationsPlayerToClan> InvationsPlayerToClan { get; set; }
        public virtual ICollection<PlayerBans> PlayerBans { get; set; }
    }
}

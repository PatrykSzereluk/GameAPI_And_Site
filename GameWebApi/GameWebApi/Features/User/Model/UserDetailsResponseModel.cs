using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.User.Model
{
    public class UserDetailsResponseModel
    {
        public int PlayerId { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GameLose { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public byte? AvatarId { get; set; }
        public string AvatarUrl { get; set; }
        public byte Function { get; set; }
        public DateTime DateOfJoin { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}

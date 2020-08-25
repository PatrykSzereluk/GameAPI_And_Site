namespace GameWebApi.Features.Clan.Models
{
    public class NewClanRequestModel : BaseRequestData
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public byte AvatarId { get; set; }
        public string AvatarUrl { get; set; }
    }
}

namespace GameWebApi.Features.Clan.Models
{
    public class RemoveUserRequestModel
    {
        public int PlayerId { get; set; }
        public int ClanId { get; set; }
    }
}

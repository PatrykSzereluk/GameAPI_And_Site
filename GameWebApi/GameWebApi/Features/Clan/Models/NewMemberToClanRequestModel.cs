
namespace GameWebApi.Features.Clan.Models
{
    using Core.Enums;
    public class NewMemberToClanRequestModel : BaseRequestData
    {
        public int ClanId { get; set; }
        public ClanFunction ClanFunction { get; set; }
    }
}

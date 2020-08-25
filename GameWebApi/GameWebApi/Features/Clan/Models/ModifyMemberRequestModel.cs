namespace GameWebApi.Features.Clan.Models
{
    using Core.Enums;
    public class ModifyMemberRequestModel : BaseRequestData
    {
        public ClanFunction ClanFunction { get; set; }
    }
}

namespace GameWebApi.Features.Clan.Models
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GameWebApi.Models.DB;
    public interface IClanService
    {
         Task<NewClanResponseModel> AddNewClan(NewClanRequestModel model);
         Task<NewMemberToClanResponseModel> AddMemberToClan(NewMemberToClanRequestModel model);
         Task<bool> ModifyMemberFunction(ModifyMemberRequestModel model);
         Task<bool> RemoveMember(RemoveUserRequestModel model);
         Task<bool> RemoveClan(RemoveClanRequestModel model);
         Task<bool> SendClanInvitationToPlayer(ClanInviteRequestModel model);
         Task<IEnumerable<InvationsPlayerToClan>> GetInvitationList(BaseRequestData model);
         Task<bool> DeleteInvitation(ClanInviteRequestModel model);
    }
}

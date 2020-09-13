namespace GameWebApi.Features.Clan.Models
{
    using System.Threading.Tasks;

    public interface IClanService
    {
         Task<NewClanResponseModel> AddNewClan(NewClanRequestModel model);
         Task<NewMemberToClanResponseModel> AddMemberToClan(NewMemberToClanRequestModel model);
         Task<bool> ModifyMemberFunction(ModifyMemberRequestModel model);
         Task<bool> RemoveMember(RemoveUserRequestModel model);
         Task<bool> RemoveClan(RemoveClanRequestModel model);
         Task<bool> SendClanInvationToUser(ClanInviteRequestModel model);
    }
}

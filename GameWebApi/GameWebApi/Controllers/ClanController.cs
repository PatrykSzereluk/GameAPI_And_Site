using System.Collections.Generic;
using GameWebApi.Features;
using GameWebApi.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace GameWebApi.Controllers
{
    using System.Threading.Tasks;
    using GameWebApi.Features.Clan.Models;
    using Microsoft.AspNetCore.Mvc;

    public class ClanController : ApiController
    {
        private readonly IClanService _clanService;

        public ClanController(IClanService clanService)
        {
            this._clanService = clanService;
        }

        //[Authorize]
        [HttpPost]
        [Route(nameof(AddNewClan))]
        public async Task<NewClanResponseModel> AddNewClan(NewClanRequestModel model)
        {
            return await _clanService.AddNewClan(model);
        }

        //[Authorize]
        [HttpPost]
        [Route(nameof(AddMemberToClan))]
        public async Task<NewMemberToClanResponseModel> AddMemberToClan(NewMemberToClanRequestModel model)
        {
            return await _clanService.AddMemberToClan(model);
        }

        //[Authorize]
        [HttpPut]
        [Route(nameof(ModifyMemberFunction))]
        public async Task<bool> ModifyMemberFunction(ModifyMemberRequestModel model)
        {
            return await _clanService.ModifyMemberFunction(model);
        }

        //[Authorize]
        [HttpDelete]
        [Route(nameof(RemoveMember))]
        public async Task<bool> RemoveMember(RemoveUserRequestModel model)
        {
            return await _clanService.RemoveMember(model);  
        }

        //[Authorize]
        [HttpDelete]
        [Route(nameof(RemoveClan))]
        public async Task<bool> RemoveClan(RemoveClanRequestModel model)
        {
            return await _clanService.RemoveClan(model);
        }

        [HttpPost]
        [Route(nameof(InvitePlayerToClan))]
        public async Task<bool> InvitePlayerToClan(ClanInviteRequestModel model)
        {
            return await _clanService.SendClanInvitationToPlayer(model);
        }

        [HttpGet]
        [Route(nameof(GetInvitationList))]
        public async Task<IEnumerable<InvationsPlayerToClan>> GetInvitationList(BaseRequestData model)
        {
            return await _clanService.GetInvitationList(model);
        }

        [HttpDelete]
        [Route(nameof(DeleteInvitation))]
        public async Task<bool> DeleteInvitation(ClanInviteRequestModel model)
        {
            return await _clanService.DeleteInvitation(model);
        }
    }
}

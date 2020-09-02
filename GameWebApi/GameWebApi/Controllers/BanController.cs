using GameWebApi.Features;
using Microsoft.AspNetCore.Authorization;

namespace GameWebApi.Controllers
{
    using System.Threading.Tasks;
    using Features.Ban;
    using GameWebApi.Features.Ban.Models;
    using Microsoft.AspNetCore.Mvc;
    public class BanController : ApiController
    {
        private readonly IBanService _banService;
        public BanController(IBanService banService)
        {
            _banService = banService;
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(BanPlayer))]
        public async Task<bool> BanPlayer(BanPlayerRequestModel model)
        {
            return await _banService.BanPlayer(model);
        }

        [Authorize]
        [HttpPut]
        [Route(nameof(CancelBan))]
        public async Task<bool> CancelBan(BaseRequestData model)
        {
            return await _banService.CancelBan(model.PlayerId);
        }

    }
}

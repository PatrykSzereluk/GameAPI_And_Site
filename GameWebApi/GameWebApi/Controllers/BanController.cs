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

        [Route(nameof(BanPlayer))] // TODO: PRZENIEŚĆ DO INNEGO KONTROLERA
        public async Task<bool> BanPlayer(BanPlayerRequestModel model)
        {
            return await _banService.BanPlayer(model);
        }

        [Route(nameof(CancelBan))] // TODO: PRZENIEŚĆ DO INNEGO KONTROLERA
        public async Task<bool> CancelBan(BanPlayerRequestModel model)
        {
            return await _banService.CancelBan(model);
        }

    }
}

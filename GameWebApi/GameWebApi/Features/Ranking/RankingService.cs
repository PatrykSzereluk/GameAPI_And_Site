namespace GameWebApi.Features.Home
{
    using System.Threading.Tasks;
    using Models;
    using GameWebApi.Models.DB;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;

    public class RankingService : IRankingService
    {
        private readonly GameDBContext _context;

        public RankingService(GameDBContext context)
        {
            _context = context;
        }

       
    }
}

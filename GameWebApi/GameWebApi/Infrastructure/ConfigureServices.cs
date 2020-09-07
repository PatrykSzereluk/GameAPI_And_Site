using GameWebApi.Features.Email;

namespace GameWebApi.Infrastructure
{
    using Features.Identity;
    using Sql.Interfaces;
    using Sql.Managers;
    using Microsoft.Extensions.DependencyInjection;
    using Security;
    using Features.Home;
    using Features.Ranking;
    using GameWebApi.Features.Clan.Models;
    using Features.Clan;
    using Features.User;
    using Features.Ban;
    public static class ConfigureServices
    {

        public static IServiceCollection AddTransientCollection(this IServiceCollection services)
        {
            return services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ISqlManager, SqlManager>()
                .AddTransient<IEncrypter, Encrypter>()
                .AddTransient<IHomeService, HomeService>()
                .AddTransient<IRankingService, RankingService>()
                .AddTransient<IClanService, ClanService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IBanService, BanService>()
                .AddTransient<IEmailService,EmailService>();
        }


    }
}

namespace GameWebApi.Infrastructure
{
    using GameWebApi.Features.Identity;
    using GameWebApi.Sql.Interfaces;
    using GameWebApi.Sql.Managers;
    using Microsoft.Extensions.DependencyInjection;
    using Security;

    public static class ConfigureServices
    {

        public static IServiceCollection AddTransientCollection(this IServiceCollection services)
        {
            return services
                 .AddTransient<IIdentityService, IdentityService>()
                 .AddTransient<ISqlManager, SqlManager>()
                 .AddTransient<IEncrypter, Encrypter>();
        }


    }
}

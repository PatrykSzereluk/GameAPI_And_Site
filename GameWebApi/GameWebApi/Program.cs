namespace GameWebApi
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using NLog;

    public class Program
    {
        public static void Main(string[] args)
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            GlobalDiagnosticsContext.Set("basePath", basePath);
            var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

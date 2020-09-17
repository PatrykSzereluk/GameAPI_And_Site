using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

namespace GameWebApi
{
    using System.Text;
    using Models.DB;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Infrastructure;
    using System.IO;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GameDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DBContext")));

            services.AddTransientCollection();

            var applicationSettingsConfig = Configuration.GetSection("ApplicationSettings");

            services.Configure<ApplicationSettings>(applicationSettingsConfig);

            var appSettings = applicationSettingsConfig.Get<ApplicationSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(t =>
                {
                    t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(t =>
                {
                    t.RequireHttpsMetadata = false;
                    t.SaveToken = true;
                    t.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // for upload files
            services.Configure<FormOptions>(opt =>
            {
                opt.ValueLengthLimit = 6000000;
                opt.MultipartBodyLengthLimit = 6000000;
                opt.MemoryBufferThreshold = 6000000;
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthentication();

            app.UseAuthorization();

            // for upload files
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"@images")),
                RequestPath = new PathString("/images")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
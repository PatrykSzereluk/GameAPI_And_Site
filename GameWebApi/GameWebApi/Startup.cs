using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWebApi.Models;
using GameWebApi.Models.DB;
using GameWebApi.Models.Features.Identity;
using GameWebApi.Services;
using GameWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GameWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GameDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DBContext")));
            
            services.AddTransient<IIdentityService, IdentityService>();

            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<GameDBContext>();

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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


//,
//  "Jwt": {
//    "Key": "MySecretKey",
//    "Issuer": "GameApi",
//    "Audience": "Gamers",
//    "Subject": "Data"
//  }
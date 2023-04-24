using Antoher.DAL;
using Antoher.DAL.Interfaces;
using Antoher.DAL.Repos;
using Antoher.Domain.Models;
using Antoher.Hubs;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.HttpsPolicy;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Antoher
{
    /// <summary>
    /// Class for configuring all services for DI and inserting things in pipeline
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adding services to ServiceCollection
        /// </summary>
        /// <param name="services">ServiceCollection - DI container</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSignalR();

            services.AddRouting(x =>
            {
                x.LowercaseQueryStrings = true;
                x.LowercaseUrls = true;
            });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "ProgRate API", Version = "v1" });
                var path = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, path);
                var options = new SwaggerGenOptions();
                x.IncludeXmlComments(xmlPath);
            });

            #region Скоупы для зависимостей контекста
            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<ILikeRepo, LikeRepo>();
            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<IRequestRepo, RequestRepo>();
            services.AddScoped<IFriendRepo, FriendRepo>();
            services.AddScoped<IChatRepo, ChatRepo>();
            services.AddScoped<IGroupRepo, GroupRepo>();
            #endregion


            var str = Configuration.GetConnectionString("DbConnection");
            #region Конеткст Бд
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql
            (str,
                MySqlServerVersion.AutoDetect(str)));
            #endregion

            #region Identity настройки
            services.AddDefaultIdentity<User>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            #endregion

            #region JWT настройки
            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;//токен не будет храниться на сервере!
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

        }

        
        /// <summary>
        /// Pipeline configuring
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCookiePolicy();

            app.UseSwagger();
            app.UseSwaggerUI(o => {
                o.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "v1");
                });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region для клиента Андрея

            
            app.UseCors(cors =>
            {
                cors.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials();
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });

                
            


        }
    }
}

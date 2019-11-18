using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AracTakipSistemi.Domain.Services;
using AracTakipSistemi.Models;
using AracTakipSistemi.Security.Token;
using AracTakipSistemi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AracTakipSistemi
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

            services.AddSwaggerGen(_ => _.SwaggerDoc("docname", new Info { Title = "Doc Title", Version = "v1" }));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "AracTakip API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
            });

            services.AddMvc();

            //services.AddSwaggerGen(swagger =>
            //{
            //    swagger.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My First Swagger" });
            //});

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddSwaggerGen(
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "MyTestService", Version = "v1" });
            //});

            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                });
            });

            services.AddDbContext<AppIdentityDbContext>(opts =>
            {
                opts.UseSqlServer("Data Source=.;Initial Catalog=True;Persist Security Info=True; User ID=sa; Password=ea3981");
            });

            services.AddIdentity<Users, Roles>(opts =>
             {
                 opts.User.RequireUniqueEmail = true;
                 opts.User.AllowedUserNameCharacters = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZQWXabcçdefgğhıijklmnoöprsştuüvyzqwx0123456789.-_";
                 opts.Password.RequiredLength = 6;
                 opts.Password.RequireNonAlphanumeric = false;
                 opts.Password.RequireLowercase = false;
                 opts.Password.RequireNonAlphanumeric = false;
                 opts.Password.RequireUppercase = false;
                 opts.Password.RequireDigit = false;

             }).AddEntityFrameworkStores<AppIdentityDbContext>();

            services.Configure<CustomTokenOptions>(Configuration.GetSection("TokenOptions"));
            var TokenOptions = Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtbeareroptions =>
            {
                jwtbeareroptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = TokenOptions.Issuer,
                    ValidAudience = TokenOptions.Audience,
                    IssuerSigningKey = SignHandler.GetSecurityKey(TokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AracTakip API V1");
            //});

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "AracTakip/swagger/{documentName}swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/AracTakip/swagger/v1/swagger.json", "TestService");
                c.RoutePrefix = "MyTestService/swagger";
            });

            //app.UseSwagger();
            //app.UseSwaggerUI(_ => _.SwaggerEndpoint("/swagger/docname/swagger.json", "AracTakipSistemi"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

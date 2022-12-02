using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AuthenticationService.Api.Application.Handlers;
using AuthenticationService.Api.Application.Middlewares;
using AuthenticationService.Api.Application.Services.Jwt;
using AuthenticationService.Api.Application.Services.Password;
using AuthenticationService.Infrastructure;
using AuthenticationService.Infrastructure.Repositories;
using AuthenticationService.Infrastructure.Repositories.Interfaces;
using Serilog;

namespace AuthenticationService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();
            services.AddHttpClient();
            services.AddMemoryCache();

            services.AddJwt(c =>
            {
                c.ValidIssuer = Configuration.GetValue<string>("JwtOptions:ValidIssuer");
                c.ValidAudience = Configuration.GetValue<string>("JwtOptions:ValidAudience");
                c.AccessSecurityKey = Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_KEY");
                c.RefreshSecurityKey = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_KEY");
                c.AccessTokenExpiry = Configuration.GetValue<TimeSpan>("JwtOptions:AccessTokenExpiry");
                c.RefreshTokenExpiry = Configuration.GetValue<TimeSpan>("JwtOptions:RefreshTokenExpiry");
            });

            services.AddMvc().ConfigureApiBehaviorOptions(c =>
            {
                c.InvalidModelStateResponseFactory = context =>
                {
                    return new BadRequestObjectResult(new
                    {
                        Error = "Validation error",
                        Fields = context.ModelState.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage)
                    });
                };
            });

            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MsSql")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = JwtSettings.TokenValidationParameters();
            });
            
            /*
            services.AddAuthorization(options =>
            {
                options.AddPolicyWithSuperUser(Policies.CreateUser, "DigitalChannels.ExtAuthentication", "CreateUser");
                options.AddPolicyWithSuperUser(Policies.CreateClaim, "DigitalChannels.ExtAuthentication", "CreateClaim");

                options.AddPolicyWithSuperUser(Policies.DeleteUser, "DigitalChannels.ExtAuthentication", "DeleteUser");
                options.AddPolicyWithSuperUser(Policies.BlockUser, "DigitalChannels.ExtAuthentication", "BlockUser");

                options.AddPolicyWithSuperUser(Policies.AddClaimsToUser, "DigitalChannels.ExtAuthentication", "AddClaimsToUser");

                options.AddPolicyWithSuperUser(Policies.GetUserClaims, "DigitalChannels.ExtAuthentication", "GetUserClaims");
                options.AddPolicyWithSuperUser(Policies.GetClaims, "DigitalChannels.ExtAuthentication", "GetClaims");
                options.AddPolicyWithSuperUser(Policies.GetRoles, "DigitalChannels.ExtAuthentication", "GetRoles");
                options.AddPolicyWithSuperUser(Policies.GetUsers, "DigitalChannels.ExtAuthentication", "GetUsers");
            });
            */

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Authentication API", 
                    Version = "v1", 
                    Description = "example authentication api"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT token", 
                    In = ParameterLocation.Header, 
                    Name = "Authorization", 
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            //services.AddSingleton<IMemoryCacheWrapper, MemoryCacheWrapper>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<IClaimRepository, ClaimRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();
            services.AddScoped<ISignInHandler, SignInHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders();

            app.UseForwardedHeaders();
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            //app.UseMiddleware<AuthorizationMiddleware>();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "auth");
            });

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

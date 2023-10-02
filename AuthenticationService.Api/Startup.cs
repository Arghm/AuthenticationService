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
using Serilog;
using AuthenticationService.Infrastructure.DependencyInjections;
using AuthenticationService.Application.Services.Jwt;
using AuthenticationService.Migrations;
using AuthenticationService.Api.Middlewares;
using System.Security.Claims;
using AuthenticationService.Contracts.Authentication;
using AuthenticationService.Api.Authorization;

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

            services.AddJwt(c =>
            {
                c.ValidIssuer = Configuration.GetValue<string>("JwtOptions:ValidIssuer");
                c.ValidAudience = Configuration.GetValue<string>("JwtOptions:ValidAudience");
                c.AccessSecurityKey = Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_KEY");
                c.RefreshSecurityKey = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_KEY");
                c.AccessTokenExpiry = Configuration.GetValue<TimeSpan>("JwtOptions:AccessTokenExpiry");
                c.RefreshTokenExpiry = Configuration.GetValue<TimeSpan>("JwtOptions:RefreshTokenExpiry");
            });

            services.AddMvc()
                .ConfigureApiBehaviorOptions(c =>
                {
                    c.InvalidModelStateResponseFactory = context =>
                    {
                        return new BadRequestObjectResult(new
                        {
                            Error = "Validation error",
                            Fields = context.ModelState.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage)
                        });
                    };
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddApplicationPart(typeof(Program).Assembly)
                .AddControllersAsServices();

            services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PgSql")));
            //.UseSqlServer(Configuration.GetConnectionString("MsSql")));

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


            services.AddAuthorization(options =>
            {
                options.AddPolicyWithSuperUser(Politics.UserOperationsPolicy, ClaimTypes.UserData);
                options.AddPolicyWithSuperUser(Politics.ReadOnlyUsersInfo, ClaimTypes.UserData, Claims.GetUsers);
                options.AddPolicyWithSuperUser(Politics.ReadWriteUsersInfo, ClaimTypes.UserData, ClaimTypes.UserData, Claims.GetUsers, Claims.CreateUser, Claims.UpdateUser, Claims.DeleteUser);
                options.AddPolicyWithSuperUser(Politics.ReadWriteUsersClaims, ClaimTypes.UserData, Claims.CreateClaim, Claims.GetUserClaims, Claims.AddClaimsToUser);
            });

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

            // register auth services
            services.AddAuthServices();
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

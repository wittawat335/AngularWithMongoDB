using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using System.Text;
using Demo.Core.AutoMapper;
using AspNetCore.Identity.MongoDbCore.Extensions;
using Demo.Domain.Models.Collections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Demo.Core.Interfaces;
using Demo.Core.Services;
using Demo.Domain.Utilities;

namespace Demo.Core
{
    public static class Configuration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMenuService, MenuService>();
        }

        public static void MongoDbIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:JWT:key").Value!);
            var connectionString = configuration.GetSection("MongoDbSetting:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDbSetting:DatabaseName").Value;
            var Client_URL = configuration.GetSection(Constants.AppSettings.Client_URL).Value;

            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

            //add mongoIdentityConfiguration...
            var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = connectionString,
                    DatabaseName = databaseName
                },
                IdentityOptionsAction = options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireLowercase = false;

                    //lockout
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;

                    options.User.RequireUniqueEmail = true;

                }

            };

            services.ConfigureMongoDbIdentity<User, Role, Guid>(mongoDbIdentityConfig).AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Client_URL,
                    ValidAudience = Client_URL,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero

                };
            });
        }
    }
}

using System.Reflection;
using System.Text;
using GameShop.Common.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GameShop.Common.Jwt
{
    public static class Extensions
    {


        public static IServiceCollection AddCustomJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {


            // services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            // var tokenValidationParameters = new TokenValidationParameters
            // {
            //     ValidateIssuerSigningKey = true,
            //     ValidateIssuer = true,
            //     ValidateAudience = true,
            //     ValidateLifetime = true,

            //     ValidIssuer = jwtSettings.Issuer,
            //     ValidAudiences = jwtSettings.Audiences,
            //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            // };
            // services.AddSingleton(tokenValidationParameters);
            // services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {
                    // var jwtSettings = option.Configuration.
                    option.RequireHttpsMetadata = true;
                    option.SaveToken = true;
                    option.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,

                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudiences = jwtSettings.Audiences,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                });
            return services;
        }
        // public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration){
        //     services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        //     services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        //     services.AddAuthentication();
        // }
    }
}
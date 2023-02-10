// using System.Text;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.IdentityModel.Tokens;

// namespace JwtAuthenticationManager
// {
//     public static class CustomJwtAuthExtension
//     {
//         public static void AddCustomJwtAuthentication(this IServiceCollection services){
//             services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
//             .AddAuthentication(option =>{
//                 option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                 option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//                 option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//             })
//             .AddJwtBearer(option =>
//             {
//                 option.RequireHttpsMetadata = false;
//                 option.SaveToken = true;
//                 option.TokenValidationParameters = new TokenValidationParameters
//                 {
//                     ValidateIssuerSigningKey = true,
//                     ValidateIssuer = false,
//                     ValidateAudience = false,
//                     ValidateLifetime = true,
//                     ValidIssuer = configuration[JwtSettings.Issuer],
//                     ValidAudience = configuration[JwtSettings.Audience],
//                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenHandler.JWT_SECURITY_KEY))
//                 };
//             });
//             // services.AddIdentity();
//         }
//         public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration){
//             services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
//             services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
//             services.AddAuthentication();
//         }
//     }
// }

// appsettings.json
// ,
//   "Authentication":{
//     "Schemes": {
//       "Bearer":{
//         "ValidAudiences": [
//           "https://localhost:5001",
//           "https://localhost:6001",
//           "https://localhost:7001",
//           "https://localhost:8001"
//         ],
//         "ValidIssuer": "dotnet-user-jwts"  
//       }
//     }
//   }
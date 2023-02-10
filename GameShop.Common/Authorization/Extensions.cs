using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GameShop.Common.Authorization
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddAuthorization(options =>{
                options.AddPolicy("Administrator", policy =>{
                    policy.RequireRole("Administrator");
                });
                options.AddPolicy("PersionalPolicy", policy =>{
                    policy.RequireClaim("ID");

                });
                options.AddPolicy("BadgeEntry", policy =>
                    policy.RequireAssertion(context => context.User.HasClaim(c =>
                        (c.Type == "BadgeId" || c.Type == "TemporaryBadgeId")
                        && c.Issuer == "https://microsoftsecurity")
                ));
            });

            return services;
        }
    }
}
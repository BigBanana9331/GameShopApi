namespace GameShop.User.Services
{
    public static class Extensions
    {
        public static void AddServices(this IServiceCollection services){
            services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
            services.AddSingleton<IPasswordHandler, PasswordHandler>();
        }
    }
}
// using GameShop.Cart.Client;
// using Polly;
// using Polly.Timeout;
using GameShop.Cart.Entities;
using GameShop.Common.MongoDB;
using GameShop.Common.MassTransit;
using GameShop.Common.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCustomJwtAuthentication(builder.Configuration);
builder.Services.AddMongo()
            .AddMongoRepository<CartItem>("carts")
            .AddMongoRepository<CatalogItem>("catalogs")
            .AddMongoRepository<Customer>("customers")
            .AddMassTransitWithRabbitMq();


// AddCatalogClient(builder);
builder.Services.AddControllers(options => 
{ 
    options.SuppressAsyncSuffixInActionNames = false; 
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
























// static void AddCatalogClient(WebApplicationBuilder builder)
// {
//     Random jetterer = new Random();
//     builder.Services.AddHttpClient<GameClient>(gameClient =>
//     {
//         gameClient.BaseAddress = new Uri("https://localhost:5001");
//     })
//     .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
//         5,
//         retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)) + TimeSpan.FromMilliseconds(jetterer.Next(0, 1000)),
//         onRetry: (outcome, timespan, retryAttemp) =>
//         {
//             var servicesProvider = builder.Services.BuildServiceProvider();
//             servicesProvider.GetService<ILogger<GameClient>>()?
//                 .LogWarning($"Delay for {timespan.TotalSeconds} seconds, then making retry {retryAttemp}");
//         }
//     ))
//     .AddTransientHttpErrorPolicy(policyBuider => policyBuider.Or<TimeoutRejectedException>().CircuitBreakerAsync(
//         3,
//         TimeSpan.FromSeconds(15),
//         onBreak: (outcome, timespan) =>
//         {
//             var servicesProvider = builder.Services.BuildServiceProvider();
//             servicesProvider.GetService<ILogger<GameClient>>()?
//                 .LogWarning($"Opening the circuit for {timespan.TotalSeconds} seconds...");
//         },
//         onReset: () =>
//         {
//             var servicesProvider = builder.Services.BuildServiceProvider();
//             servicesProvider.GetService<ILogger<GameClient>>()?
//                 .LogWarning($"Closing circuit...");
//         }
//     ))
//     .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
// }
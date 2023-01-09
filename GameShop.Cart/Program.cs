using GameShop.Cart.Client;
using GameShop.Cart.Entities;
using GameShop.Common.MongoDB;
using GameShop.Common.Settings;
using Polly;
using Polly.Timeout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMongo().AddMongoRepository<CartItem>("cartitems");
Random jetterer = new Random();
builder.Services.AddHttpClient<GameClient>(gameClient =>
{
    gameClient.BaseAddress = new Uri("http://localhost:5001");
})
.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
    5,
    retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)) + TimeSpan.FromMilliseconds(jetterer.Next(0, 1000)),
    onRetry: (outcome, timespan, retryAttemp) =>
    {
        var servicesProvider = builder.Services.BuildServiceProvider();
        servicesProvider.GetService<ILogger<GameClient>>()?
            .LogWarning($"Delay for {timespan.TotalSeconds} seconds, then making retry {retryAttemp}");
    }
))
.AddTransientHttpErrorPolicy(policyBuider => policyBuider.Or<TimeoutRejectedException>().CircuitBreakerAsync(
    3,
    TimeSpan.FromSeconds(15),
    onBreak: (outcome, timespan) =>{
        var servicesProvider = builder.Services.BuildServiceProvider();
        servicesProvider.GetService<ILogger<GameClient>>()?
            .LogWarning($"Opening the circuit for {timespan.TotalSeconds} seconds...");
    },
    onReset: () =>{
        var servicesProvider = builder.Services.BuildServiceProvider();
        servicesProvider.GetService<ILogger<GameClient>>()?
            .LogWarning($"Closing circuit...");
    }
))
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

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

using GameShop.Common.MongoDB;
using GameShop.Common.MassTransit;
using GameShop.User.Entities;
// using GameShop.User.Settings;

var builder = WebApplication.CreateBuilder(args);

// var serviceSettings = new ServiceSettings();
// serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });

// Add services to the container.
builder.Services.AddMongo().AddMongoRepository<UserAcount>("users").AddMassTransitWithRabbitMq();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

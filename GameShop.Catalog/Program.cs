using GameShop.Common.MongoDB;
using GameShop.Common.Settings;
using GameShop.Catalog.Entities;
using MassTransit;
using GameShop.Catalog.Settings;
// using MongoDB.Driver;

var serviceSettings = new ServiceSettings();
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

builder.Services.AddMongo().AddMongoRepository<Game>("games");
builder.Services.AddMassTransit(x =>{
    x.UsingRabbitMq((context, configurator)=>{
        var rabbitMQSettings = builder.Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
        configurator.Host(rabbitMQSettings.Host);
        configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
    });
});
// builder.Services.AddMassTransitHostedService();
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

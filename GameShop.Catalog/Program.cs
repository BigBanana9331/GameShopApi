using GameShop.Common.MongoDB;
using GameShop.Catalog.Entities;
using GameShop.Common.MassTransit;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddMongo()
                .AddMongoRepository<Game>("games")
                .AddMassTransitWithRabbitMq();
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

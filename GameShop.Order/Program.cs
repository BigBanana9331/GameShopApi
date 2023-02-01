using GameShop.Common.MassTransit;
using GameShop.Common.MongoDB;
using GameShop.Order.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
builder.Services.AddMongo()
            .AddMongoRepository<OrderSumary>("orders")
            .AddMongoRepository<OrderItem>("orderitems")
            .AddMongoRepository<CatalogItem>("catalogs")
            .AddMongoRepository<Customer>("customers")
            .AddMassTransitWithRabbitMq();
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

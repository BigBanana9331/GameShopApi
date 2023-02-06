using GameShop.Common.MongoDB;
using GameShop.Common.MassTransit;
using GameShop.User.Entities;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddMongo()
                .AddMongoRepository<UserAccount>("users")
                .AddMassTransitWithRabbitMq();
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
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

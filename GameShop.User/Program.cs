using GameShop.Common.MongoDB;
using GameShop.Common.MassTransit;
using GameShop.User.Entities;
using GameShop.Common.Jwt;
using GameShop.User.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCustomJwtAuthentication(builder.Configuration);

builder.Services.AddMongo()
                .AddMongoRepository<UserAccount>("users")
                .AddMongoRepository<RefreshToken>("tokens")
                .AddMassTransitWithRabbitMq();

builder.Services.AddServices();

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
    // app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

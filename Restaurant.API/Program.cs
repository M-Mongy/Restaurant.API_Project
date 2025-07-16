using Restaurant.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.addAplication();


builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();
var scope=app.Services.CreateScope();
var seeder= scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.seed();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

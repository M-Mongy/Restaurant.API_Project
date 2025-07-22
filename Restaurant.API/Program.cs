using Restaurant.API.Middlewares;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);
new CompactJsonFormatter();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.addAplication();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMilddle>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context , configration) => {
    configration.ReadFrom.Configuration(context.Configuration);
});
var app = builder.Build();

var scope=app.Services.CreateScope();
var seeder= scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.seed();
// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMilddle>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();

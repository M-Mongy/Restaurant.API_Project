using Restaurants.Infrastructure.Extensions;

using Restaurants.Application.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

using Restaurant.API.Middlewares;
using Restaurant.Infrastructure.Seeders;
using Restaurant.Domain.Entities;
using Microsoft.OpenApi.Models;
using Restaurant.API.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication();
builder.Services.addAplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Addpresentaion();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // Ensure this is present
}
await seeder.seed();
// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMilddle>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();


app.UseAuthorization();

app.MapControllers();

app.Run();
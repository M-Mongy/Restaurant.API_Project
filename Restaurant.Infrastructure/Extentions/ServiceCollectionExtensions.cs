using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authentication;
using Restaurant.Infrastructure.Authentication.Requerments;
using Restaurant.Infrastructure.Authorization.Services;
using Restaurant.Infrastructure.Repository;
using Restaurant.Infrastructure.Seeders;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RestaurantsDbContext>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();

        services.AddAuthorizationBuilder()
            .AddPolicy(policy.HasNationality, builder => builder.RequireClaim(AppClaimsTypes.Nationality))
        .AddPolicy(policy.Atleast20, builder => builder.AddRequirements(new MinmumAppRequerment(20)));

        services.AddScoped<IAuthorizationHandler, MinmumAppRequermentHandler>();

        services.AddScoped<IReastaurantAuthrizationService, ReastaurantAuthrizationService>();
    }
}
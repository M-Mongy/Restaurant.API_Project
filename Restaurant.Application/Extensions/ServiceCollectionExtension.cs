using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurant;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void addAplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService,RestaurantService>();

    }
}
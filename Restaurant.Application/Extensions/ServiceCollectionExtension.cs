using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurant;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void addAplication(this IServiceCollection services)
    {
        var ApplicationAssmply = typeof(ServiceCollectionExtensions).Assembly;
        services.AddScoped<IRestaurantService,RestaurantService>();
        services.AddAutoMapper(ApplicationAssmply);
        services.AddValidatorsFromAssembly(ApplicationAssmply).AddFluentValidationAutoValidation();

    }
}

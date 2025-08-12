using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.Command.CreateDish;
using Restaurant.Domain.Constents;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authorization.Services;
namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IRestaurantRepository restaurantsRepository,
    IDishRepository dishesRepository,
    IMapper mapper,
    IReastaurantAuthrizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);
        var restaurant = await restaurantsRepository.GetByIdasync(request.restaurantId);
        if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.restaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForBidException();

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.Create(dish);

    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.Command.Delete;
using Restaurant.Domain.Constents;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authorization.Services;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;
public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger,
IRestaurantRepository restaurantsRepository,
IDishRepository dishesRepository,
    IReastaurantAuthrizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesFromRestaurantCommand>
{
    public async Task Handle(DeleteDishesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.ResturantID);

        var restaurant = await restaurantsRepository.GetByIdasync(request.ResturantID);
        if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.ResturantID.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForBidException();

        await dishesRepository.Delete(restaurant.Dishes);
    }
}
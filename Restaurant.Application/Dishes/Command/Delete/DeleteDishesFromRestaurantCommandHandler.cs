using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Command.Delete
{
    public class DeleteDishesFromRestaurantCommandHandler(ILogger<DeleteDishesFromRestaurantCommandHandler> logger
        ,IRestaurantRepository restaurantRepository ,IDishRepository dishRepository ): IRequestHandler<DeleteDishesFromRestaurantCommand>
    {
        public async Task Handle(DeleteDishesFromRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Removing all Dishes From restaurant: {RestaurantID}", request.ResturantID);

            var restaurant = await restaurantRepository.GetByIdasync(request.ResturantID);

            if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.ResturantID.ToString());

           await dishRepository.Delete(restaurant.Dishes);

        }
    }
}

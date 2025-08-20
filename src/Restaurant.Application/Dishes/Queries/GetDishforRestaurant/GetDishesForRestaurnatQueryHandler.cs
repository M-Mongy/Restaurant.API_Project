using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.GetDishforRestaurant
{
    internal class GetDishesForRestaurnatQueryHandler(ILogger<GetDishesForRestaurnatQueryHandler> logger , 
        IRestaurantRepository restaurantRepository,IMapper mapper) : IRequestHandler<GetDishesForRestaurnatQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurnatQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dishes for Restaurant with id: {restaurntId}",request.RestaurnatId);
            var restaurant = await restaurantRepository.GetByIdasync(request.RestaurnatId);

            if (restaurant == null)
            {
                // If the restaurant is not found, throw a custom exception.
                // This can be caught by a middleware to return a 404 Not Found response.
                throw new NotfoundException(nameof(Restaurant), request.RestaurnatId.ToString());
            }
            var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return result;
        }
    }
}

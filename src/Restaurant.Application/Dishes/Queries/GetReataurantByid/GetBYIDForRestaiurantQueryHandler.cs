using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.GetReataurantByid
{
    public class GetBYIDForRestaiurantQueryHandler(ILogger<GetBYIDForRestaiurantQueryHandler> logger ,IRestaurantRepository restaurantRepository
        , IMapper mapper) : IRequestHandler<GetBYIDForRestaiurantQuery,DishDto>
    {
        public async Task<DishDto> Handle(GetBYIDForRestaiurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id: {RestaurantId}",
                request.dishId,
                request.restaurantId);

            var restaurant = await restaurantRepository.GetByIdasync(request.restaurantId);

            if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.restaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.dishId);
            if (dish == null) throw new NotfoundException(nameof(Dish), request.dishId.ToString());

            var result = mapper.Map<DishDto>(dish);
            return result;
        }


    }
}

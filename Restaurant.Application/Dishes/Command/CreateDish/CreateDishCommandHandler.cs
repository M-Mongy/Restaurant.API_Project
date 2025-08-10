using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constents;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authorization.Services;

namespace Restaurant.Application.Dishes.Command.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
         IRestaurantRepository restaurantRepository ,
         IDishRepository dishRepository,IMapper mapper, IReastaurantAuthrizationService reastaurantAuthrization) :IRequestHandler<CreateDishCommand,int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish: {@DishRequest}", request);
            var restaurant = await restaurantRepository.GetByIdasync(request.restaurantId);
            if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.restaurantId.ToString());

            if (!reastaurantAuthrization.Authorize(restaurant, ResourceOperation.Create))
                throw new ForBidException();
            var dish = mapper.Map<Dish>(request);

            return await dishRepository.Create(dish);

        }
    }
}

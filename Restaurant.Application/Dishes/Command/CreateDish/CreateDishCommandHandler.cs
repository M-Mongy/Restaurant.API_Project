using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Command.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
         IRestaurantRepository restaurantRepository , IDishRepository dishRepository,IMapper mapper) :IRequestHandler<CreateDishCommand>
    {
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish: {@DishRequest}", request);
            var restaurant = await restaurantRepository.GetByIdasync(request.restaurantId);
            if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.restaurantId.ToString());

            var dish = mapper.Map<Dish>(request);

            await dishRepository.Create(dish);

        }
    }
}

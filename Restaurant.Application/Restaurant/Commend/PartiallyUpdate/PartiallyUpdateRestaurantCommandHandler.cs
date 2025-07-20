using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commend.PartiallyUpdate
{
    public class PartiallyUpdateRestaurantCommandHandler(ILogger<PartiallyUpdateRestaurantCommandHandler> logger
        , IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<PartiallyUpdateRestaurantCommand ,bool>
    {
        public async Task<bool> Handle(PartiallyUpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"updateing Restaurant With id {request.Id}");
            var restaurant = await restaurantRepository.GetByIdasync(request.Id);

            if (restaurant == null) 
                return false;

            if (!string.IsNullOrEmpty(request.Name))
                restaurant.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Description))
                restaurant.Description = request.Description;


            if (!string.IsNullOrEmpty(request.Category))
                restaurant.Category = request.Category;

            await restaurantRepository.Update(restaurant);

            return true;

        }
    }
}

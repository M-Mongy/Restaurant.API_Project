using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commend.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger 
       , IMapper mapper , IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand,bool>
    {
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"deleting Restaurant With id {request.id}");
            var restaurant = await restaurantRepository.GetByIdasync(request.id);
            if (restaurant is null)
                return false;

            await restaurantRepository.Delete(restaurant);
            return true;
            
        }
    }
}

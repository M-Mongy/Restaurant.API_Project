using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commend.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger 
       , IMapper mapper , IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("deleting Restaurant With id : {RestaurantID} ",request.id);
            var restaurant = await restaurantRepository.GetByIdasync(request.id);
            if (restaurant is null)
                throw new NotfoundException(nameof(Restaurant),restaurant.Id.ToString());

            await restaurantRepository.Delete(restaurant);
          
            
        }
    }
}

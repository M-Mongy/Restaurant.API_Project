using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;


namespace Restaurant.Application.Restaurant.Commend.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommand> logger 
        , IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create new Restaurant {@Restaurant}",request);
            var resutant = mapper.Map<Restaurant2>(request);
            int id = await restaurantRepository.Create(resutant);
            return id;
        }
    }
}

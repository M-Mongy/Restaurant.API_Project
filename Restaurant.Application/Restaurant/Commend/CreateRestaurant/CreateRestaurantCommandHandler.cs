using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;


namespace Restaurant.Application.Restaurant.Commend.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommand> logger 
        , IMapper mapper, IRestaurantRepository restaurantRepository,IuserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser=userContext.GetCurrentUser();

            logger.LogInformation("{userName} [{userId}] Create new Restaurant {@Restaurant}",currentUser.Email,currentUser.id,request);
            var resutant = mapper.Map<Restaurant2>(request);
            resutant.ownerId=currentUser.id;
            int id = await restaurantRepository.Create(resutant);
            return id;
        }
    }
}

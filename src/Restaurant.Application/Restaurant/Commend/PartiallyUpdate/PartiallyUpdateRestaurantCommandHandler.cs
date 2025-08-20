using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constents;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authorization.Services;

namespace Restaurant.Application.Restaurant.Commend.PartiallyUpdate
{
    public class PartiallyUpdateRestaurantCommandHandler(ILogger<PartiallyUpdateRestaurantCommandHandler> logger
        , IMapper mapper, IRestaurantRepository restaurantRepository,
        IReastaurantAuthrizationService reastaurantAuthrization) : IRequestHandler<PartiallyUpdateRestaurantCommand>
    {
        public async Task Handle(PartiallyUpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await restaurantRepository.GetByIdasync(request.Id);
            if (restaurant is null)
                throw new NotfoundException(nameof(Restaurant), request.Id.ToString());

            if (!reastaurantAuthrization.Authorize(restaurant, ResourceOperation.Update))
                throw new ForBidException();

            mapper.Map(request, restaurant);
         

            await restaurantRepository.SaveChanges();

        }
    }
}

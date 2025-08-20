using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Queries.GetByIdRestaurant
{
    public class GetByIdRestaurantQueryHandler(ILogger<GetByIdRestaurantQueryHandler> logger ,IMapper mapper
        , IRestaurantRepository restaurantRepository) : IRequestHandler<GetByIdRestaurantQuery, RestaurantDTO>
    {
        public async Task<RestaurantDTO> Handle(GetByIdRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get Restaurant by Id {Restaurant}",request.id);
            var rest_id = await restaurantRepository.GetByIdasync(request.id)
                        ?? throw new NotfoundException(nameof(Restaurant), request.id.ToString());


            var restDTO = mapper.Map<RestaurantDTO>(rest_id);
            return restDTO;

        }
    }
}

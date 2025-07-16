using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant
{
    public class RestaurantService(IRestaurantRepository restaurant
        , ILogger<RestaurantService> logger , IMapper mapper) : IRestaurantService
    {

        public async Task<IEnumerable<RestaurantDTO>> GetAllaRestaurant()
        {
            logger.LogInformation("Get All Restaurants");
            var returants = await restaurant.GetAllasync();
            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDTO>>(returants);

            return restaurantsDtos!;
        }

        public async Task<RestaurantDTO?> GetById(int id)
        {
            logger.LogInformation("Get Restaurant by Id");
            var rest_id= await restaurant.GetByIdasync(id);
            var restDTO = mapper.Map<RestaurantDTO>(rest_id);
            return restDTO;

        }
    }
}

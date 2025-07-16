using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant
{
    public class RestaurantService(IRestaurantRepository restaurant, ILogger<RestaurantService> logger) : IRestaurantService
    {

        public async Task<IEnumerable<Restaurant2>> GetAllaRestaurant()
        {
            logger.LogInformation("Get All Restaurants");
            var returants = await restaurant.GetAllasync();
            return returants;
        }
    }
}

using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurant
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant2>> GetAllaRestaurant();
    }
}
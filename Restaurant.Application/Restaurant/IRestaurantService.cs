using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurant
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllaRestaurant();
        Task<RestaurantDTO?> GetById(int id);
        Task<int> CreateAsync(CreateRestaurantDto dto);
    }
}
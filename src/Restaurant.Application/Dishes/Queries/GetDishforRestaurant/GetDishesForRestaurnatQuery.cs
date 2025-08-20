using MediatR;
using Restaurant.Application.Dishes.DTOS;


namespace Restaurant.Application.Dishes.Queries.GetDishforRestaurant
{
    public class GetDishesForRestaurnatQuery(int resaurantid):IRequest<IEnumerable<DishDto>>
    {
        public int RestaurnatId { get; } = resaurantid;
    }
}

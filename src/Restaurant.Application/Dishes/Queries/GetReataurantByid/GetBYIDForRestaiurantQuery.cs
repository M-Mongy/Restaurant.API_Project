using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Dishes.Queries.GetReataurantByid
{
    public class GetBYIDForRestaiurantQuery(int restaurantId,int dishId) :IRequest<DishDto>
    {
        public int restaurantId { get; } = restaurantId;
        public int dishId { get; } = dishId;

    }
}

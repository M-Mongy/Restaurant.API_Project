using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurant.Application.Dishes.Command.Delete
{
    public class DeleteDishesFromRestaurantCommand(int restaurantId):IRequest
    {
        public int ResturantID { get; } = restaurantId;
    }
}

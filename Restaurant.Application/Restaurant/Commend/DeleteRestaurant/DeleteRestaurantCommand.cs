using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurant.Application.Restaurant.Commend.DeleteRestaurant
{
    public class DeleteRestaurantCommand(int id):IRequest
    {
        public int id { get; } = id;
    }
}

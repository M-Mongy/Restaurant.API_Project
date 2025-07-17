using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurant.Application.Restaurant.Commend.DeleteRestaurant
{
    public class DeleteRestaurantQuery(int id):IRequest<bool>
    {
        public int id { get; } = id;
    }
}

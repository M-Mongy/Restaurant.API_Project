using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurant.Application.Restaurant.DTOS;

namespace Restaurant.Application.Restaurant.Queries.GetByIdRestaurant
{
    public class GetByIdRestaurantQuery(int id):IRequest<RestaurantDTO>
    {
        public int id { get; } = id;

    }
}
 
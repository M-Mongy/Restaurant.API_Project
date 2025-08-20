using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurant.Application.Dishes.Command.CreateDish
{
    public class CreateDishCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int? kiloCalories { get; set; }
        public int restaurantId { get; set; }
    }
}

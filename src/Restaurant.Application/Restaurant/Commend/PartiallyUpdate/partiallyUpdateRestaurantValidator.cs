using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.Restaurant.DTOS;
using FluentValidation;

namespace Restaurant.Application.Restaurant.Commend.CreateRestaurant
{
    public class partiallyUpdateRestaurantCommandValidator : AbstractValidator<PartiallyUpdateRestaurantCommand>
    {
        public partiallyUpdateRestaurantCommandValidator()
        {
            RuleFor(c => c.Name)
                .Length(3, 100);
        }
    }
}

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
        private readonly List<string> ValidCategories = ["Italian","English","Japanese","Mexican","Amarican"];
        public partiallyUpdateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
             .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(dto => dto.Category).Must(ValidCategories.Contains).WithMessage("Invalid Category , Please Choose From valid Categories");

        }
    }
}

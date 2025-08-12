using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Restaurant.Application.Restaurant.DTOS;

namespace Restaurant.Application.Restaurant.Queries.GetAllRestaurant
{
    public class GetAllRestaurantsQueryValidator: AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];
        private string[] allowSortByColumnsNames = [nameof(RestaurantDTO.Name)
            , nameof(RestaurantDTO.Category), nameof(RestaurantDTO.Description)];

        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => allowPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

            RuleFor(r => r.SortBy)
              .Must(value => allowSortByColumnsNames.Contains(value))
              .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColumnsNames)}]");
        }
    }
}

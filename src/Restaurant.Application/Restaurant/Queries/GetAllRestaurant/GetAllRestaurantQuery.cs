using MediatR;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Constents;

namespace Restaurant.Application.Restaurant.Queries.GetAllRestaurant
{
    public class GetAllRestaurantsQuery : IRequest<PageResult<RestaurantDTO>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection sortdirection { get; set; }

    }
}

﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Application.Restaurant.Queries.GetAllRestaurant;
using Restaurant.Domain.Repositories;


namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
    IMapper mapper,
    IRestaurantRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDTO>>
{
    public async Task<PageResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        
        logger.LogInformation("Getting all restaurants");
        var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase,
                  request.PageSize,
             request.PageNumber,
            request.SortBy,
            request.sortdirection);

        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

        var result = new PageResult<RestaurantDTO>(restaurantsDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
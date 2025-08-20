using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Application.Users;
using Restaurant.Domain.Repositories;

namespace Restaurant.Infrastructure.Authorization.Requerments
{
    public class createmultipleRestaurantRequermentHandler(IRestaurantRepository restaurantRepository,
        IuserContext userContext) : AuthorizationHandler<createmultipleRestaurantRequerment>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, createmultipleRestaurantRequerment requirement)
        {
            var currentuser = userContext.GetCurrentUser();
            var restaurants = await restaurantRepository.GetAllasync();

            var userRestaurantCreated = restaurants.Count(r => r.ownerId == currentuser!.id);

            if (userRestaurantCreated >= requirement.MinimumRestaurantCreated)
            {
                context.Succeed(requirement);
            }
            else 
            {
                context.Fail();
            }
        }
    }
}

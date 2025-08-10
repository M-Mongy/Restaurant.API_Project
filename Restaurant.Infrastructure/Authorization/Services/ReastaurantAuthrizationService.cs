using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;
using Restaurant.Domain.Constents;
using Restaurant.Domain.Contents;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Authorization.Services
{
    public class ReastaurantAuthrizationService(ILogger<ReastaurantAuthrizationService> logger
      , IuserContext userContext) : IReastaurantAuthrizationService
    {
        public bool Authorize(Restaurant2 restaurant, ResourceOperation resource)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Authorization user {UserEmail}, to {operarion} for Restaurant {RrestaurantName}"
                , user.Email, resource, restaurant.Name);

            if (resource == ResourceOperation.Read || resource == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

            if (resource == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if ((resource == ResourceOperation.Delete || resource == ResourceOperation.Update)
                && user.id == restaurant.ownerId)
            {
                logger.LogInformation("Restaurant owner - successful authorization");
                return true;
            }

            return false;
        }

    }
}

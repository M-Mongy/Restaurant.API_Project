using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;

namespace Restaurant.Infrastructure.Authentication.Requerments
{
    public class MinmumAppRequermentHandler(ILogger<MinmumAppRequermentHandler> logger ,IuserContext userContext) : AuthorizationHandler<MinmumAppRequerment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinmumAppRequerment requirement)
        {
            var currentuser = userContext.GetCurrentUser();
            logger.LogInformation("user:{Email},date of birth {dob} - handling MinmumAppRequerment", currentuser.Email, currentuser.DateOfBirth);

            if (currentuser.DateOfBirth == null) {
                logger.LogWarning("user date of birth is null");   
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentuser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization Succeeded");
                context.Succeed(requirement);
            }
            else { 
                context.Fail();

            }
            return Task.CompletedTask;
        }
    }
}

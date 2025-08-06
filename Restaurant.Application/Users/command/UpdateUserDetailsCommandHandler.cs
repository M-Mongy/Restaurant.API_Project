using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Users;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Users.command
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger
        ,IuserContext userContext , IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("updating User:{userId},with {@request}", user!.id, request);
            var dbUser = await userStore.FindByIdAsync(user!.id, cancellationToken);

            if (dbUser == null) 
            {
                throw new NotfoundException(nameof(User), user!.id);
            

            }
            
            dbUser.DateOfBirth=request.DateOfBirth;
            dbUser.Nationality=request.Nationality;

            await userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}

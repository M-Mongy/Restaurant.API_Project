using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users.command.AssigningRole;
using Restaurant.Domain.Contents;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Users.command.RemoveUserRole
{
    public class RemovaRoleCommandHandler(ILogger<RemovaRoleCommandHandler> logger,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<RemovaRoleCommand>
    {
        public async Task Handle(RemovaRoleCommand request, CancellationToken cancellationToken)
        {
               logger.LogInformation("Unassigning user role: {@Request}", request);
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotfoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotfoundException(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}

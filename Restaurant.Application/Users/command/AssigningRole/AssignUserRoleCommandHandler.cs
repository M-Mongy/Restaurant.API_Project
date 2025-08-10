using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Users.command.AssigningRole
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger ,
        UserManager<User> userManager , RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning user Role :(@Request) ", request);
            var user = await userManager.FindByEmailAsync(request.UserEmail) 
                ?? throw new NotfoundException(nameof(User),request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotfoundException(nameof(IdentityRole), request.RoleName);

            await userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}

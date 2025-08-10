using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.command.AssigningRole;
using Restaurant.Application.Users.command.RemoveUserRole;
using Restaurant.Application.Users.command.updateCommand;
using Restaurant.Domain.Contents;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("Api/Identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command) {
            await mediator.Send(command);
            return NoContent();
        }


        [HttpPatch("userRole")]
        [Authorize(Roles=UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteUserRole(RemovaRoleCommand command)
       {
            await mediator.Send(command);
            return NoContent();
        }
    }
}

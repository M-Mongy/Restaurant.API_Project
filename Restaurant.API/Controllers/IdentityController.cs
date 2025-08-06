using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.command;

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
    }
}

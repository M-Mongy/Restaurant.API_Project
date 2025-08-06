using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurant;
using Restaurant.Application.Restaurant.Commend.CreateRestaurant;
using Restaurant.Application.Restaurant.Commend.DeleteRestaurant;
using Restaurant.Application.Restaurant.Commend.PartiallyUpdate;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Application.Restaurant.Queries.GetAllRestaurant;
using Restaurant.Application.Restaurant.Queries.GetByIdRestaurant;
using Restaurant.Domain.Repositories;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/Restaurant")]
    [Authorize]
    public class RestaurantController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll()
        {
            var resturants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(resturants);
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<RestaurantDTO?>> GetById([FromRoute]int id)
        {
                var rest_id = await mediator.Send(new GetByIdRestaurantQuery(id));
                    return Ok(rest_id);


        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
                return NoContent();
            
        
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] PartiallyUpdateRestaurantCommand command) 
        { 
          command.Id = id;
            await mediator.Send(command);
            return NoContent();
         }
        [HttpPost] 
        public async Task<IActionResult> createRestaurant([FromBody] CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}

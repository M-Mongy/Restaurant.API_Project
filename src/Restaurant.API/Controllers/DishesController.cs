using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Command.CreateDish;
using Restaurant.Application.Dishes.Command.Delete;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Application.Dishes.Queries.GetDishforRestaurant;
using Restaurant.Application.Dishes.Queries.GetReataurantByid;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Authentication;

namespace Restaurant.API.Controllers
{
    [Route("api/Restaurant/{restaurantid}/Dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.restaurantId = restaurantId;

            var dishId= await mediator.Send(command);
            return CreatedAtAction(nameof(GetBYIDForRestaiurant), new { restaurantId ,dishId},null);
        }

        [HttpGet]
        [Authorize(Policy=policy.Atleast20)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaiurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurnatQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetBYIDForRestaiurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetBYIDForRestaiurantQuery(restaurantId, dishId));
            return Ok(dish);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDishesFromRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesFromRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}

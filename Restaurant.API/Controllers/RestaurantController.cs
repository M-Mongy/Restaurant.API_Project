using System.Threading.Tasks;
using MediatR;
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
    public class RestaurantController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resturants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(resturants);
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var rest_id = await mediator.Send(new GetByIdRestaurantQuery(id));
            if (rest_id == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(rest_id);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] PartiallyUpdateRestaurantCommand command) 
        { 
          command.Id = id;  
            if (id!= command.Id)
            {
                return BadRequest();
            }
            var result = await mediator.Send(command);
            if(result == null)
            {
                return NotFound();
            }
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

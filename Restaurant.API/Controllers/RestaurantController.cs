using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurant;
using Restaurant.Application.Restaurant.Commend.CreateRestaurant;
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
        [HttpPost] 
        public async Task<IActionResult> createRestaurant([FromBody] CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}

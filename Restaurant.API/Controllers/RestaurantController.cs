using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurant;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Repositories;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/Restaurant")]
    public class RestaurantController(IRestaurantService restaurant) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resturants = await restaurant.GetAllaRestaurant();
            return Ok(resturants);
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var rest_id = await restaurant.GetById(id);
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
        public async Task<IActionResult> createRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            int id = await restaurant.CreateAsync(createRestaurantDto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}

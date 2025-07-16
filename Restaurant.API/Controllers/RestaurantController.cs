using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Repositories;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/Restaurant")]
    public class RestaurantController(IRestaurantRepository restaurant) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resturants = await restaurant.GetAllasync();
            return Ok(resturants);
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var rest_id = await restaurant.GetByIdasync(id);
            if (rest_id == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(rest_id);
            }
        }
    }
}

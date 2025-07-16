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
    }
}

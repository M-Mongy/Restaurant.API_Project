using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Repository
{
    internal class RestaurantRepository(RestaurantsDbContext _dbContext) : IRestaurantRepository
    {
        public async Task<int> Create(Restaurant2 entity)
        {
             _dbContext.restaurants.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Restaurant2>> GetAllasync()
        {
          var restaurants= await _dbContext.restaurants.ToListAsync(); 
            return restaurants;
        }

        public async Task<Restaurant2?> GetByIdasync(int id)
        {
            return await _dbContext.restaurants.Include(x=>x.Dishes).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}

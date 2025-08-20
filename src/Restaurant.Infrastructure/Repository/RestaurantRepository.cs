using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constents;
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

        public async Task Delete(Restaurant2 entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant2>> GetAllasync()
        {
          var restaurants= await _dbContext.restaurants.Include(x=>x.Dishes).ToListAsync(); 
            return restaurants;
        } 
        
        public async Task<(IEnumerable<Restaurant2>,int)> GetAllMatchingAsync(string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = _dbContext
                .restaurants
                .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                       || r.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant2, object>>>
            {
                { nameof(Restaurant2.Name), r => r.Name },
                { nameof(Restaurant2.Description), r => r.Description },
                { nameof(Restaurant2.Category), r => r.Category },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }

        public async Task<Restaurant2?> GetByIdasync(int id)
        {
            return await _dbContext.restaurants.Include(x=>x.Dishes).SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task SaveChanges()
        => _dbContext.SaveChangesAsync();

        public async Task Update(Restaurant2 entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

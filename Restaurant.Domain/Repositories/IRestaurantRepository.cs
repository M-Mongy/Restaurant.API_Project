using Restaurant.Domain.Constents;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant2>> GetAllasync();
        Task<Restaurant2?> GetByIdasync(int id);
        Task<int> Create(Restaurant2 entity);
        Task Delete(Restaurant2 entity);
        Task Update(Restaurant2 entity);
        Task<(IEnumerable<Restaurant2>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task SaveChanges();

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant2>> GetAllasync();
        Task<Restaurant2?> GetByIdasync(int id);
        Task<int> Create(Restaurant2 entity);

    }

}

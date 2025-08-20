using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories
{
    public interface IDishRepository
    {
        Task<int> Create(Dish entity);
        Task Delete(IEnumerable<Dish> entities);

    }
}

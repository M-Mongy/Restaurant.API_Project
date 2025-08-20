using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurant.DTOS
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }


        public string? City { get; set; }  
        public string? Street { get; set; }  
        public string? PostalCode { get; set; }  
        public List<DishDto> Dishes { get; set; } = [];

        public static RestaurantDTO FromEntity(Restaurant2? r)
        {
            if (r == null) { return null; }
            return new RestaurantDTO()
            {
                Category = r.Category,
                Description = r.Description,
                Id = r.Id,
                HasDelivery = r.HasDelivery,
                Name = r.Name,
                City = r.Address?.City,
                Street = r.Address?.Street,
                PostalCode = r.Address?.PostalCode,
                Dishes = r.Dishes.Select(DishDto.FromEntity).ToList()
            };


        }
    }
}

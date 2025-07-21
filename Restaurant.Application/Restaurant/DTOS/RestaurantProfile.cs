using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurant.Application.Restaurant.Commend.CreateRestaurant;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurant.DTOS
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<PartiallyUpdateRestaurantCommand, Restaurant2>();

            CreateMap<CreateRestaurantCommand, Restaurant2>()
                .ForMember(d => d.Address, opt => opt.MapFrom(
                    src => new Address
                    {
                        City = src.City,
                        PostalCode = src.PostalCode,
                        Street = src.Street
                    }));

            CreateMap<Restaurant2, RestaurantDTO>()
                .ForMember(d => d.City, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.PostalCode, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Street, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
        }
    }
}

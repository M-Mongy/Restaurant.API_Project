using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Application.Restaurant.Commend.CreateRestaurant;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Application.Restaurant.DTOS.Tests
{
    public class RestaurantProfileTests
    {
        [Fact()]
        public void createMap_ForRestaurantToRestaurantDTO_MapsCorrectly()
        {
            var configration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
            });

            var mapper = configration.CreateMapper();
            var restaurant = new Restaurant2()
            {
                Id = 1,
                Name = "Test restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12-345"
                }
            };

            var restaurantDto = mapper.Map<RestaurantDTO>(restaurant);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);

        }



        [Fact()]
        public void createMap_toCreateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            var configration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
            });

            var mapper = configration.CreateMapper();

            var command = new CreateRestaurantCommand
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"
            };


            var restaurant = mapper.Map<Restaurant2>(command);
            restaurant.Should().NotBe(command.Name);


            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            restaurant.ContactEmail.Should().Be(command.ContactEmail);
            restaurant.ContactNumber.Should().Be(command.ContactNumber);
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);

        }
    }
}
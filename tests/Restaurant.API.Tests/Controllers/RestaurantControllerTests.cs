using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Seeders;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization.Policy;
using Restaurant.API.Tests;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Entities;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Restaurant.API.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantRepository> _restaurantsRepositoryMock = new();
        private readonly Mock<IRestaurantSeeder> _restaurantsSeederMock = new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantRepository),
                                                _ => _restaurantsRepositoryMock.Object));


                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantSeeder),
                                                _ => _restaurantsSeederMock.Object));
                });
            });
        }


        [Fact]
        public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
        {
            // arrange

            var id = 1123;

            _restaurantsRepositoryMock.Setup(m => m.GetByIdasync(id)).ReturnsAsync((Restaurant2?)null);

            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync($"/api/restaurants/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForExistingId_ShouldReturn200Ok()
        {
            // arrange

            var id = 99;

            var restaurant = new Restaurant2()
            {
                Id = id,
                Name = "Test",
                Description = "Test description"
            };

            _restaurantsRepositoryMock.Setup(m => m.GetByIdasync(id)).ReturnsAsync(restaurant);

            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync($"/api/Restaurant/{id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDTO>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test");
            restaurantDto.Description.Should().Be("Test description");
        }

        [Fact]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/Restaurant?PageNumber=1&PageSize=10");

            // assert

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/Restaurant");

            // assert

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

        }
    }
}
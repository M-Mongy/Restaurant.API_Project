using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Moq;
using Restaurant.Application.Users;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Xunit;

namespace Restaurant.Infrastructure.Authorization.Requerments.Tests
{
    public class createmultipleRestaurantRequermentHandlerTests
    {
        [Fact()]
        public async Task HandleRequirmentAsync_userHasCreateMultipleRestaurant_shouldfaild()
        {
            var currentUser=new CurrentUser("1", "test@test.com", [],null,null);
            var UserContextMock = new Mock<IuserContext>();
            UserContextMock.Setup(m=>m.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant2>()
             {
              new ()
              {
                  ownerId = currentUser.id,
              },
              new ()
              {
                  ownerId = "2",
              },
             };

            var restaurantsRepositoryMock = new Mock<IRestaurantRepository>();
            restaurantsRepositoryMock.Setup(r => r.GetAllasync()).ReturnsAsync(restaurants);

            var requirement = new createmultipleRestaurantRequerment(2);
            var handler = new createmultipleRestaurantRequermentHandler(restaurantsRepositoryMock.Object,
                UserContextMock.Object);

            var context = new AuthorizationHandlerContext([requirement], null, null);
            await handler.HandleAsync(context);

            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();


        }[Fact()]
        public async Task HandleRequirmentAsync_userHasCreateMultipleRestaurant_shouldSucceed()
        {
            var currentUser=new CurrentUser("1", "test@test.com", [],null,null);
            var UserContextMock = new Mock<IuserContext>();
            UserContextMock.Setup(m=>m.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant2>()
             {
              new ()
              {
                  ownerId = currentUser.id,
              },
              new ()
              {
                  ownerId = currentUser.id,
              },
              new ()
              {
                  ownerId = "2",
              },
             };

            var restaurantsRepositoryMock = new Mock<IRestaurantRepository>();
            restaurantsRepositoryMock.Setup(r => r.GetAllasync()).ReturnsAsync(restaurants);

            var requirement = new createmultipleRestaurantRequerment(2);
            var handler = new createmultipleRestaurantRequermentHandler(restaurantsRepositoryMock.Object,
                UserContextMock.Object);

            var context = new AuthorizationHandlerContext([requirement], null, null);
            await handler.HandleAsync(context);

            context.HasSucceeded.Should().BeTrue();


        }
    }
}
using AutoMapper;
using FluentAssertions;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Application.Restaurant.Commend.CreateRestaurant;
using Restaurant.Application.Users;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            // arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommand>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant2();

            mapperMock.Setup(m => m.Map<Restaurant2>(command)).Returns(restaurant);

            var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            restaurantRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<Restaurant2>()))
                .ReturnsAsync(1);

            var userContextMock = new Mock<IuserContext>();
            var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);


            var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object,
                mapperMock.Object,
                restaurantRepositoryMock.Object,
                userContextMock.Object);

            // act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(1);
            restaurant.ownerId.Should().Be("owner-id");
            restaurantRepositoryMock.Verify(r => r.Create(restaurant), Times.Once);
        }
    }
}

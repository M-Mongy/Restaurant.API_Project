using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Domain.Constents;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authorization.Services;
using Xunit;

namespace Restaurant.Application.Restaurant.Commend.PartiallyUpdate.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<PartiallyUpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IReastaurantAuthrizationService> _restaurantAuthorizationServiceMock;

        private readonly PartiallyUpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<PartiallyUpdateRestaurantCommandHandler>>();
            _restaurantsRepositoryMock = new Mock<IRestaurantRepository>();
            _mapperMock = new Mock<IMapper>();
            _restaurantAuthorizationServiceMock = new Mock<IReastaurantAuthrizationService>();

            _handler = new PartiallyUpdateRestaurantCommandHandler(
                _loggerMock.Object,
                _mapperMock.Object, // Swapped
                _restaurantsRepositoryMock.Object, // Swapped
                _restaurantAuthorizationServiceMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            // arrange
            var restaurantId = 1;
            var command = new PartiallyUpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "New Test",
                Description = "New Description",
                HasDelivery = true,
            };

            var restaurant = new Restaurant2()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test",
            };

            _restaurantsRepositoryMock.Setup(r => r.GetByIdasync(restaurantId))
                .ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock.Setup(m => m.Authorize(restaurant, Domain.Constents.ResourceOperation.Update))
                .Returns(true);


            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert

            _restaurantsRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
            _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
        {
            // Arrange
            var restaurantId = 2;
            var request = new PartiallyUpdateRestaurantCommand
            {
                Id = restaurantId
            };

            _restaurantsRepositoryMock.Setup(r => r.GetByIdasync(restaurantId))
                    .ReturnsAsync((Restaurant2?)null);

            // act

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotfoundException>()
                    .WithMessage($"Restaurant with id:{restaurantId} doesn't exist");
        }

        [Fact]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            // / Arrange
            var restaurantId = 3;
            var request = new PartiallyUpdateRestaurantCommand
            {
                Id = restaurantId
            };

            var existingRestaurant = new Restaurant2
            {
                Id = restaurantId
            };

            _restaurantsRepositoryMock
                .Setup(r => r.GetByIdasync(restaurantId))
                    .ReturnsAsync(existingRestaurant);

            _restaurantAuthorizationServiceMock
                .Setup(a => a.Authorize(existingRestaurant, ResourceOperation.Update))
                    .Returns(false);

            // act

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ForBidException>();
        }
    }
}

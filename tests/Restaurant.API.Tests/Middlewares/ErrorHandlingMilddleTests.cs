using Xunit;
using Restaurant.API.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using Restaurant.Domain.Exceptions;

namespace Restaurant.API.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            // arrange

            var loggerMock = new Mock<ILogger<ErrorHandlingMilddle>>();
            var middleware = new ErrorHandlingMilddle(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextDelegateMock = new Mock<RequestDelegate>();

            // act

            await middleware.InvokeAsync(context, nextDelegateMock.Object);

            // assert

            nextDelegateMock.Verify(next => next.Invoke(context), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMilddle>>();
            var middleware = new ErrorHandlingMilddle(loggerMock.Object);
            var notFoundException = new NotfoundException(nameof(Restaurant), "1");

            // act
            await middleware.InvokeAsync(context, _ => throw notFoundException);

            // Assert
            context.Response.StatusCode.Should().Be(404);

        }

        [Fact]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMilddle>>();
            var middleware = new ErrorHandlingMilddle(loggerMock.Object);
            var exception = new ForBidException();

            // act
            await middleware.InvokeAsync(context, _ => throw exception);

            // Assert
            context.Response.StatusCode.Should().Be(403);

        }

        [Fact]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMilddle>>();
            var middleware = new ErrorHandlingMilddle(loggerMock.Object);
            var exception = new Exception();

            // act
            await middleware.InvokeAsync(context, _ => throw exception);

            // Assert
            context.Response.StatusCode.Should().Be(500);

        }

    }
}
using Xunit;
using Restaurant.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurant.Domain.Contents;
using FluentAssertions;

namespace Restaurant.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticationUser_ShouldReturnCurrentUser()
        {
            var dateOfBirth = new DateOnly(1990, 1, 1);
            var httpContextAccessormock = new Mock<IHttpContextAccessor>();
            var claim =new List<Claim>() 
            {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, "test@test.com"),
            new Claim(ClaimTypes.Role, UserRoles.Admin),
            new Claim(ClaimTypes.Role, UserRoles.User),
            new Claim("Nationality", "German"),
            new Claim("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))

            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claim,"Test"));
            httpContextAccessormock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessormock.Object);


            var currentUser= userContext.GetCurrentUser();

            currentUser.Should().NotBeNull();
            currentUser.id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("German");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // act
            Action action = () => userContext.GetCurrentUser();

            // assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}
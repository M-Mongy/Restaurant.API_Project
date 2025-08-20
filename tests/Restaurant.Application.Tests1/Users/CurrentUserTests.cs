using FluentAssertions;
using Restaurant.Domain.Contents;
using Xunit;

namespace Restaurant.Application.Users.Tests
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithWatchingRole_ShouldReturnTrue(string roleName)
        {
            var currentuser = new CurrentUser("1", "test@test.com", [UserRoles.Admin,UserRoles.User],null,null);

            var IsinRole = currentuser.IsInRole(roleName);

            IsinRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithNoWatchingRole_ShouldReturnFalse()
        {
            var currentuser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            var IsinRole = currentuser.IsInRole(UserRoles.Owner);

            IsinRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoWatchingRoleCase_ShouldReturnFalse()
        {
            var currentuser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            var IsinRole = currentuser.IsInRole(UserRoles.Admin.ToLower());

            IsinRole.Should().BeFalse();
        }
    }
}
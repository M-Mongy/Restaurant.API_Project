using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Restaurant.Application.Users
{
    public interface IuserContext
    {
        CurrentUser? GetCurrentUser();
    }
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IuserContext
    {
     
        public CurrentUser? GetCurrentUser()
        {
            var user =httpContextAccessor?.HttpContext?.User;
            if (user == null) 
            {
                throw new InvalidOperationException("User context is not present");
            }
            if (user.Identity == null || !user.Identity.IsAuthenticated) 
            { 
            return null; 
            }

            var userId=user.FindFirst(c=>c.Type==ClaimTypes.NameIdentifier)!.Value;
            var email=user.FindFirst(c=>c.Type==ClaimTypes.Email)!.Value;
            var roles=user.Claims.Where(c=>c.Type==ClaimTypes.Role)!.Select(c=>c.Value);
            var Nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
            var DateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
            var DateOfBirth = DateOfBirthString==null ? (DateOnly?) null:DateOnly.ParseExact(DateOfBirthString,"yyyy-MM-dd");



            return new CurrentUser(userId, email, roles, Nationality, DateOfBirth);
        }
    }
}

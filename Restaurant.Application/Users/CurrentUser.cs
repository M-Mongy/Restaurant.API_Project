
namespace Restaurant.Application.Users
{
    public record class CurrentUser(string id,string Email,IEnumerable<string> Roles)
    {
        public bool IsInRole(string role)=> Roles.Contains(role);

    }
}

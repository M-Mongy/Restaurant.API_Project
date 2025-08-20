using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurant.Application.Users.command.RemoveUserRole
{
    public class RemovaRoleCommand:IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;

    }
}

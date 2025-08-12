using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Infrastructure.Authorization.Requerments
{
    public class createmultipleRestaurantRequerment(int minimumRestaurantCreated) :IAuthorizationRequirement
    {
        public int MinimumRestaurantCreated { get; } = minimumRestaurantCreated;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Authentication
{
    public static class policy
    {
        public const string HasNationality = "HasNationality";
        public const string Atleast20 = "Atleast20";
    }

    public static class AppClaimsTypes
    {
        public const string Nationality = "Nationality";
        public const string DateOfBirth = "DateOfBirth";

    }
}

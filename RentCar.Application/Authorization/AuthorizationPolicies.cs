using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Authorization
{
    public static class AuthorizationPolicies
    {
        public const string ManageCars = "ManageCars";
        public const string ManageReservations = "ManageReservations";
        public const string ManageUsers = "ManageUsers";
        public const string ViewReports = "ViewReports";
    }
}

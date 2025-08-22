using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Common.Identity
{
    public static class DefaultClaims
    {
        public const string ManageCars = "ManageCars";
        public const string ManageReservations = "ManageReservations";
        public const string ManagePayments = "ManagePayments";
        public const string ViewReports = "ViewReports";
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Paid = 2,
        Refunded = 3
    }

    public enum DepositStatus
    {
        None = 0,
        Held = 1,
        Released = 2,
        UsedForDamage = 3
    }

    public enum InspectionType
    {
        Pickup = 1,
        Dropoff = 2
    }
}

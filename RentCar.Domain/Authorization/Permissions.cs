namespace RentCar.Domain.Authorization
{
    public static class Permissions
    {
        public static class Cars
        {
            public const string View = "Permissions.Cars.View";
            public const string Create = "Permissions.Cars.Create";
            public const string Update = "Permissions.Cars.Update";
            public const string Delete = "Permissions.Cars.Delete";
            public const string ManageImages = "Permissions.Cars.ManageImages";
        }

        public static class CarBrands
        {
            public const string View = "Permissions.CarBrands.View";
            public const string Create = "Permissions.CarBrands.Create";
            public const string Update = "Permissions.CarBrands.Update";
            public const string Delete = "Permissions.CarBrands.Delete";
        }

        public static class CarModels
        {
            public const string View = "Permissions.CarModels.View";
            public const string Create = "Permissions.CarModels.Create";
            public const string Update = "Permissions.CarModels.Update";
            public const string Delete = "Permissions.CarModels.Delete";
        }

        public static class CarPricingRules
        {
            public const string View = "Permissions.CarPricingRules.View";
            public const string Create = "Permissions.CarPricingRules.Create";
            public const string Update = "Permissions.CarPricingRules.Update";
            public const string Delete = "Permissions.CarPricingRules.Delete";
        }

        public static class Reservations
        {
            public const string View = "Permissions.Reservations.View";
            public const string Create = "Permissions.Reservations.Create";
            public const string Update = "Permissions.Reservations.Update";
            public const string Cancel = "Permissions.Reservations.Cancel";
            public const string Approve = "Permissions.Reservations.Approve";
            public const string Reject = "Permissions.Reservations.Reject";
            public const string ViewHistory = "Permissions.Reservations.ViewHistory";
            public const string AddHistory = "Permissions.Reservations.AddHistory";
            public const string GenerateContract = "Permissions.Reservations.GenerateContract";
        }

        public static class Menus
        {
            public const string Add = "Permissions.Menus.Add";
            public const string Edit = "Permissions.Menus.Edit";
            public const string Manage = "Permissions.Menus.Manage";
            public const string AssignToRole = "Permissions.Menus.AssignToRole";
            public const string ViewByRole = "Permissions.Menus.ViewByRole";
            public const string DeleteByRole = "Permissions.Menus.DeleteByRole";
        } 

        public static class Reports
        {
            public const string View = "Permissions.Reports.View";
        }

        public static class Contracts
        {
            public const string Generate = "Permissions.Contracts.Generate";
        }

        public static class Payments
        {
            public const string Add = "Permissions.Payments.Add";
            public const string Confirm = "Permissions.Payments.Confirm";
        }


    }
}

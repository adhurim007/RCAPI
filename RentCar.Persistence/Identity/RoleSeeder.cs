using Microsoft.AspNetCore.Identity;
using RentCar.Domain.Entities;
using System.Security.Claims;
using RentCar.Domain.Authorization;

namespace RentCar.Persistence.Identity
{
    public static class RoleSeeder
    {
        public static class DefaultRoles
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string BusinessAdmin = "BusinessAdmin";
            public const string Client = "Client";
            public static readonly string[] AllRoles = { SuperAdmin, BusinessAdmin, Client };
        }

        public static async Task SeedAsync(
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            // Ensure roles exist
            foreach (var role in DefaultRoles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            // 🔹 Collect ALL permissions dynamically
            var allPermissions = GetAllPermissions();

            // Assign all permissions to SuperAdmin role
            var superAdminRole = await roleManager.FindByNameAsync(DefaultRoles.SuperAdmin);
            if (superAdminRole != null)
            {
                var existingClaims = await roleManager.GetClaimsAsync(superAdminRole);

                foreach (var permission in allPermissions)
                {
                    if (!existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    {
                        await roleManager.AddClaimAsync(superAdminRole, new Claim("Permission", permission));
                    }
                }
            }

            // Optionally: Seed a default SuperAdmin user
            var superAdminEmail = "superadmin@rentcar.com";
            var superAdminUser = await userManager.FindByEmailAsync(superAdminEmail);
            if (superAdminUser == null)
            {
                superAdminUser = new ApplicationUser
                {
                    UserName = "superadmin",
                    Email = superAdminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(superAdminUser, "SuperAdmin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdminUser, DefaultRoles.SuperAdmin);
                }
            }
        }

        /// <summary>
        /// Extracts all permission constants from the Permissions static class
        /// </summary>
        private static List<string> GetAllPermissions()
        {
            var permissions = new List<string>();

            // Reflect over Permissions class and get all public const strings
            var fields = typeof(Permissions).GetNestedTypes()
                .SelectMany(t => t.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy))
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));

            foreach (var field in fields)
            {
                var value = field.GetRawConstantValue() as string;
                if (!string.IsNullOrEmpty(value))
                {
                    permissions.Add(value);
                }
            }

            return permissions;
        }
    }
}

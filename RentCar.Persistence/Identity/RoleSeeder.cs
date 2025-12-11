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
            
            foreach (var role in DefaultRoles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
             
            var allPermissions = GetAllPermissions();
             
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
             
            var superAdminEmail = "superadmin@rentcar.com";
            var superAdminUser = await userManager.FindByEmailAsync(superAdminEmail);
            if (superAdminUser == null)
            {
                superAdminUser = new ApplicationUser
                {
                    UserName = "superadmin",
                    Email = superAdminEmail,
                    EmailConfirmed = true,
                    FullName = "System Super Admin"
                };

                var result = await userManager.CreateAsync(superAdminUser, "SuperAdmin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdminUser, DefaultRoles.SuperAdmin);
                }
            }
        }
  
        private static List<string> GetAllPermissions()
        {
            var permissions = new List<string>();

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

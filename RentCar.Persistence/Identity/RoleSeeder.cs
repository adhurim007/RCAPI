using Microsoft.AspNetCore.Identity;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public static class DefaultClaims
        {
            public static readonly Claim[] AllClaims = new[]
            {
                new Claim("Permission", "ManageUsers"),
                new Claim("Permission", "ManageCars"),
                new Claim("Permission", "ViewReports"), 
            };
        }
        public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager)
        {
            // Ensure roles exist
            foreach (var role in DefaultRoles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            // Assign claims to SuperAdmin role
            var superAdminRole = await roleManager.FindByNameAsync(DefaultRoles.SuperAdmin);
            if (superAdminRole != null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(superAdminRole);

                foreach (var claim in DefaultClaims.AllClaims)
                {
                    if (!roleClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                    {
                        await roleManager.AddClaimAsync(superAdminRole, claim);
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
    }
}

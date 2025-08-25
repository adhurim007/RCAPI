using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty; // e.g. "/cars/list"
        public string Icon { get; set; } = string.Empty;

        // Role-based visibility
        public ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
    }

    public class RoleMenu
    {
        public int Id { get; set; }

        // Must match IdentityRole<Guid>
        public Guid RoleId { get; set; }

        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public IdentityRole<Guid> Role { get; set; }
    }
}

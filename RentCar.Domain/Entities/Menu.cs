using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace RentCar.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }

        // Hierarchy
        public int? ParentId { get; set; }
        public Menu Parent { get; set; }
        public ICollection<Menu> Children { get; set; } = new List<Menu>();

        // Display
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Type { get; set; } // e.g. "basic", "group", "collapsable" (Fuse navigation)
        public string? Icon { get; set; }
        public string? Link { get; set; }

        // Behavior
        public bool HasSubMenu { get; set; }
        public string? Claim { get; set; } // Permission claim (Cars.View, Reservations.Manage, etc.)
        public bool Active { get; set; } = true;
        public int SortNumber { get; set; }

        // Audit
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        // Role-based visibility 
    }

 
}

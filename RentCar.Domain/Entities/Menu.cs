using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class Menu
{
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public int? ParentId { get; set; }
    public Menu Parent { get; set; }
    public ICollection<Menu> Children { get; set; } = new List<Menu>();

    [Column(Order = 2)]
    public string Title { get; set; } = string.Empty;

    [Column(Order = 3)]
    public string? Subtitle { get; set; }

    [Column(Order = 4)]
    public string? Type { get; set; }

    [Column(Order = 5)]
    public string? Icon { get; set; }

    [Column(Order = 6)]
    public string? Link { get; set; }

    [Column(Order = 7)]
    public bool HasSubMenu { get; set; }

    [Column("Claim", Order = 8)]
    public string? Claim { get; set; }

    [Column(Order = 9)]
    public bool Active { get; set; } = true;

    [Column(Order = 10)]
    public int SortNumber { get; set; }

    [Column(Order = 11)]
    public Guid? CreatedBy { get; set; }

    [Column(Order = 12)]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [Column(Order = 13)]
    public Guid? LastModifiedBy { get; set; }

    [Column(Order = 14)]
    public DateTime? LastModifiedOn { get; set; }

    [Column(Order = 15)]
    public Guid? DeletedBy { get; set; }

    [Column(Order = 16)]
    public DateTime? DeletedOn { get; set; }
}

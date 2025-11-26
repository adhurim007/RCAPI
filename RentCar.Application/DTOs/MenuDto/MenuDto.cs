using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.MenuDto
{
    public class MenuDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Type { get; set; }
        public string? Icon { get; set; }
        public string? Link { get; set; }
        public bool HasSubMenu { get; set; }
        public bool Active { get; set; }
        public int SortNumber { get; set; }
        public string? Claim { get; set; }
    }


}

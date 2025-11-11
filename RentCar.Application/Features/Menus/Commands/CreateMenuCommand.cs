using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Commands
{
    public class CreateMenuCommand : IRequest<int>
    {
        public int? ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Subtitle { get; set; }
        public string Type { get; set; } = "basic";  
        public string? Icon { get; set; }
        public string? Link { get; set; }
        public bool HasSubMenu { get; set; }
        public bool Active { get; set; } = true;
        public string? Claim { get; set; }  
        public int SortNumber { get; set; } = 0;
    }
}

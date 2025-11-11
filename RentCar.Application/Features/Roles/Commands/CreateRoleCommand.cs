using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Roles.Commands
{
    public class CreateRoleCommand : IRequest<bool>
    {
        [JsonPropertyName("roleName")]   // System.Text.Json
        public string Name { get; set; }
    }

}

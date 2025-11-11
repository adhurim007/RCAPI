using MediatR;

namespace RentCar.Application.Features.Users.Command
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // Optional fields for Business role
        public string? CompanyName { get; set; }
        public string? TaxId { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }

        // Parameterless constructor is required for System.Text.Json
        public CreateUserCommand() { }
    }
}

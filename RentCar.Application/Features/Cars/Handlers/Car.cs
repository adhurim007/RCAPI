
namespace RentCar.Application.Features.Cars.Handlers
{
    internal class Car
    {
        public int BusinessId { get; set; }
        public int CarModelId { get; set; }
        public int CarTypeId { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionId { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public decimal DailyPrice { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Application.Features.Reservations.Validators;
using RentCar.Application.Notifications;
using RentCar.Application.Pricing;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class CreateReservationCommandHandler
    : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly RentCarDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; 
        public CreateReservationCommandHandler(RentCarDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.DocumentNumber == request.PersonalNumber);

            int customerUserId;

            if (existingCustomer == null)
            { 
                string username = $"{request.FirstName}.{request.LastName}".ToLower();
                string email = $"{username}@rentacar.com";

                var newUser = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    FullName = $"{request.FirstName} {request.LastName}",
                    PhoneNumber = request.PhoneNumber,
                    EmailConfirmed = true
                };

                var createUserResult = await _userManager.CreateAsync(newUser, "Client123!");

                if (!createUserResult.Succeeded)
                    throw new Exception("Unable to create AspNetUser: " +
                        string.Join(", ", createUserResult.Errors.Select(e => e.Description)));
                 
                 
                await _userManager.AddToRoleAsync(newUser, "Customer");
                 
                var newCustomer = new Domain.Entities.Customer
                {
                    FullName = $"{request.FirstName} {request.LastName}",
                    PhoneNumber = request.PhoneNumber,
                    DocumentNumber = request.PersonalNumber,
                    UserId = newUser.Id,
                    DateOfBirth = DateTime.UtcNow,
                    Address = request.Address
                };

                _context.Customer.Add(newCustomer);
                await _context.SaveChangesAsync();

                customerUserId = newCustomer.Id;
            }
            else
            {
                customerUserId = existingCustomer.Id;
            }
             
            var year = DateTime.UtcNow.Year;
             
            var totalReservationsThisYear = await _context.Reservations
                .Where(r => r.CreatedAt.Year == year)   
                .CountAsync(cancellationToken);

            var sequence = totalReservationsThisYear + 1;

            var reservationNumber = $"{sequence}/{year}";


            if (request.PickupDate >= request.DropoffDate)
                throw new Exception("Dropoff date must be after pickup date.");
             
            var car = await _context.Cars
                .Include(c => c.PricingRules)
                .FirstOrDefaultAsync(c => c.Id == request.CarId);

            if (car == null)
                throw new Exception("Car not found");
             
            var extraServiceIds = request.ExtraServices.Select(x => x.ExtraServiceId).ToList();

            var extraServiceMaster = await _context.ExtraServices
                .Where(e => extraServiceIds.Contains(e.Id))
                .ToListAsync();
             
            int totalDays = (request.DropoffDate - request.PickupDate).Days;
             
            decimal carTotal = 0;

            for (int i = 0; i < totalDays; i++)
            {
                var currentDate = request.PickupDate.AddDays(i);
                decimal dayPrice = car.DailyPrice;

                var rule = car.PricingRules
                    .FirstOrDefault(r => currentDate >= r.FromDate && currentDate <= r.ToDate);

                if (rule != null)
                {
                    if (rule.RuleType == "Discount")
                        dayPrice -= rule.PricePerDay;
                    else if (rule.RuleType == "Increase")
                        dayPrice += rule.PricePerDay;
                }

                carTotal += dayPrice;
            }
             
            decimal extrasTotal = 0;
            var reservationExtraEntities = new List<ReservationExtraService>();

            foreach (var item in request.ExtraServices)
            {
                var service = extraServiceMaster.FirstOrDefault(e => e.Id == item.ExtraServiceId);
                if (service == null) continue;

                var totalServicePrice = service.PricePerDay * item.Quantity * totalDays;
                extrasTotal += totalServicePrice;

                reservationExtraEntities.Add(new ReservationExtraService
                {
                    ExtraServiceId = service.Id,
                    Quantity = item.Quantity,
                    PricePerDay = service.PricePerDay,
                    TotalPrice = totalServicePrice
                });
            }
             
            decimal totalWithoutDiscount = carTotal + extrasTotal;
             
            decimal discount = request.Discount ?? 0;
            decimal finalTotal = totalWithoutDiscount - discount;
            if (finalTotal < 0) finalTotal = 0;

           
            var reservation = new Reservation
            {
                ReservationNumber = reservationNumber,
                CarId = request.CarId,
                CustomerId = customerUserId,
                PickupLocationId = request.PickupLocationId,
                DropoffLocationId = request.DropoffLocationId,
                PickupDate = request.PickupDate,
                DropoffDate = request.DropoffDate,
                TotalDays = totalDays,
                BasePricePerDay = car.DailyPrice,
                TotalPriceWithoutDiscount = totalWithoutDiscount,
                Discount = request.Discount,
                TotalPrice = finalTotal, 
                ReservationStatusId = 1,
                BusinessId = car.BusinessId,
                Notes = request.Notes,
                
                CreatedAt = DateTime.UtcNow
            };

            reservation.ExtraServices = reservationExtraEntities;
             
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }
    }

}

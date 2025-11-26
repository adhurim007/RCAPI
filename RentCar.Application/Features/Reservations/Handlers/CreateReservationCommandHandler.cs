using MediatR;
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
    //public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
    //{
    //    private readonly RentCarDbContext _context;
    //    private readonly IReservationValidator _validator;
    //    private readonly INotificationService _notificationService;
    //    private readonly IPricingEngine _pricingEngine;
    //    public CreateReservationCommandHandler(
    //        RentCarDbContext context, 
    //        IReservationValidator validator, 
    //        INotificationService notificationService,
    //        IPricingEngine pricingEngine
    //         )
    //    {
    //        _context = context;
    //        _validator = validator;
    //        _notificationService = notificationService;
    //        _pricingEngine = pricingEngine;

    //    }

    //    //public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    //    //{
    //    //    // calculate total price using pricing engine
    //    //    var totalPrice = await _pricingEngine.CalculateReservationPrice(request.CarId, request.StartDate, request.EndDate);

    //    //    var reservation = new Reservation
    //    //    {
    //    //        CarId = request.CarId,
    //    //      //  ClientId = request.ClientId,
    //    //      //  BusinessId = request.BusinessId,
    //    //       // StartDate = request.StartDate,
    //    //      //  EndDate = request.EndDate,
    //    //        TotalPrice = totalPrice,
    //    //      //  ReservationStatusId = 1  
    //    //    };


    //    //    var (isValid, errorMessage) = await _validator.ValidateAsync(reservation);
    //    //    if (!isValid)
    //    //        throw new InvalidOperationException(errorMessage);

          
    //    //    var car = await _context.Cars
    //    //        .Include(c => c.PricingRules)
    //    //        .FirstOrDefaultAsync(c => c.Id == reservation.CarId);

    //    //    if (car == null)
    //    //        throw new InvalidOperationException("Car not found.");

    //    //    int days = (reservation.EndDate - reservation.StartDate).Days;
            
    //    //    for (int i = 0; i < days; i++)
    //    //    {
    //    //        var currentDate = reservation.StartDate.AddDays(i);
    //    //        decimal dayPrice = car.DailyPrice;

              
    //    //        var rule = car.PricingRules
    //    //            .FirstOrDefault(r => currentDate >= r.FromDate && currentDate <= r.ToDate);

    //    //        if (rule != null)
    //    //        {
    //    //            if (rule.RuleType == "Discount")
    //    //                dayPrice -= rule.PricePerDay;
    //    //            else if (rule.RuleType == "Increase")
    //    //                dayPrice += rule.PricePerDay;
    //    //        }

    //    //        totalPrice += dayPrice;
    //    //    }

    //    //    reservation.TotalPrice = totalPrice;
             
    //    //    _context.Reservations.Add(reservation);
    //    //    await _context.SaveChangesAsync(cancellationToken);


    //    //    await _notificationService.SendEmailAsync(
    //    //        "client@email.com", // look up from client entity
    //    //        "Reservation Created",
    //    //        $"Your reservation #{reservation.Id} has been created and is pending approval."
    //    //    );

    //    //    // Send notification to business
    //    //    await _notificationService.SendEmailAsync(
    //    //        "business@email.com", // look up from business entity
    //    //        "New Reservation",
    //    //        $"A new reservation #{reservation.Id} has been made for your car."
    //    //    );

    //    //    return reservation.Id;
    //    //}
    //}

}

using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Contract;
using RentCar.Application.Features.Contracts.Commands;
using RentCar.Application.Notifications;
using RentCar.Application.Services;
using RentCar.Domain.Entities;
using RentCar.Domain.Enums;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Contracts.Handlers
{
    //public class GenerateContractCommandHandler : IRequestHandler<GenerateContractCommand, int>
    //{
    //    private readonly RentCarDbContext _context;
    //    private readonly INotificationService _notificationService;

    //    public GenerateContractCommandHandler(RentCarDbContext context, INotificationService notificationService)
    //    {
    //        _context = context;
    //        _notificationService = notificationService;
    //    }

    //    //public async Task<int> Handle(GenerateContractCommand request, CancellationToken cancellationToken)
    //    //{
    //    //    var reservation = await _context.Reservations
    //    //        .Include(r => r.Payment)
    //    //        .FirstOrDefaultAsync(r => r.Id == request.ReservationId, cancellationToken);

    //    //    if (reservation == null)
    //    //        throw new Exception("Reservation not found.");

    //    //    // ✅ Check payment before generating contract
    //    //    if (reservation.Payment == null || !reservation.Payment.IsConfirmed)
    //    //        throw new Exception("Contract cannot be generated until payment is confirmed.");

    //    //    // Generate PDF path or stub (we’ll implement actual PDF later)
    //    //    var contract = new Contract
    //    //    {
    //    //        ReservationId = reservation.Id,
    //    //        FileUrl = $"contracts/{Guid.NewGuid()}.pdf",
    //    //        CreatedAt = DateTime.UtcNow
    //    //    };

    //    //    _context.Contracts.Add(contract);
    //    //    await _context.SaveChangesAsync(cancellationToken);

    //    //    // Notify client
    //    //    await _notificationService.SendEmailAsync(
    //    //        "client@email.com",
    //    //        "Contract Generated",
    //    //        $"Your contract for reservation #{request.ReservationId} has been generated."
    //    //    );

    //    //    // Notify business
    //    //    await _notificationService.SendEmailAsync(
    //    //        "business@email.com",
    //    //        "Contract Generated",
    //    //        $"A new contract has been generated for reservation #{request.ReservationId}."
    //    //    );

    //    //    return contract.Id;
    //    //}
    //}
}

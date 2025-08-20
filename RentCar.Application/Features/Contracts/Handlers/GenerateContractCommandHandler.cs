using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Contract;
using RentCar.Application.Features.Contracts.Commands;
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
    public class GenerateContractCommandHandler : IRequestHandler<GenerateContractCommand, int>
    {
        private readonly RentCarDbContext _context;
        private readonly IContractPdfGenerator _pdfGenerator;

        public GenerateContractCommandHandler(RentCarDbContext context, IContractPdfGenerator pdfGenerator)
        {
            _context = context;
            _pdfGenerator = pdfGenerator;
        }

        public async Task<int> Handle(GenerateContractCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Car).ThenInclude(c => c.CarModel)
                .Include(r => r.Client)
                .Include(r => r.Business)
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId, cancellationToken);

            if (reservation == null)
                throw new Exception("Reservation not found.");

            if (reservation.ReservationStatusId != (int)ReservationStatusEnum.Confirmed)
                throw new Exception("Contract can only be generated for confirmed reservations.");

            if (reservation.Contract != null)
                throw new Exception("Contract already exists for this reservation.");

            // ✅ Generate PDF
            string filePath = _pdfGenerator.GenerateContractPdf(reservation);

            var contract = new Contract
            {
                ReservationId = reservation.Id,
                FileUrl = filePath,
                CreatedAt = DateTime.UtcNow
            };

            _context.Contracts.Add(contract);

            reservation.ReservationStatusId = (int)ReservationStatusEnum.Completed;

            _context.ReservationStatusHistories.Add(new ReservationStatusHistory
            {
                ReservationId = reservation.Id,
                ReservationStatusId = reservation.ReservationStatusId,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = request.GeneratedBy,
                Note = "Contract generated and saved as PDF."
            });

            await _context.SaveChangesAsync(cancellationToken);

            return contract.Id;
        }
    }
}

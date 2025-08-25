using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class GetReservationHistoryQueryHandler
        : IRequestHandler<GetReservationHistoryQuery, List<ReservationStatusHistoryDto>>
    {
        private readonly RentCarDbContext _context;

        public GetReservationHistoryQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationStatusHistoryDto>> Handle(GetReservationHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await _context.ReservationStatusHistories
                .Where(h => h.ReservationId == request.ReservationId)
                .Include(h => h.ReservationStatus)
                .OrderByDescending(h => h.ChangedAt)
                .ToListAsync(cancellationToken);

            return history.Select(h => new ReservationStatusHistoryDto
            {
                Id = h.Id,
                ReservationId = h.ReservationId,
                ReservationStatusId = h.ReservationStatusId,
                ReservationStatusName = h.ReservationStatus.Name,
                ChangedAt = h.ChangedAt,
                ChangedBy = h.ChangedBy,
                Note = h.Note
            }).ToList();
        }
    }
}
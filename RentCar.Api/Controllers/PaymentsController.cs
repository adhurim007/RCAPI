using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Authorization;
using RentCar.Application.Features.Payments.Commands;
using RentCar.Application.Features.Payments.Commands.RentCar.Application.Features.Payments.Commands;

namespace RentCar.Api.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        [Authorize(Policy = Permissions.Payments.Add)]
        public async Task<IActionResult> Add([FromBody] AddPaymentCommand command)
        {
            var paymentId = await _mediator.Send(command);
            return Ok(new { PaymentId = paymentId });
        }

        [HttpPost("confirm/{paymentId}")]
        [Authorize(Policy = Permissions.Payments.Confirm)]
        public async Task<IActionResult> Confirm(int paymentId)
        {
            var result = await _mediator.Send(new ConfirmPaymentCommand(paymentId));
            return Ok(new { Success = result });
        }
    }
}

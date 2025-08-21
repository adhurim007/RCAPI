using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Add([FromBody] AddPaymentCommand command)
        {
            var paymentId = await _mediator.Send(command);
            return Ok(new { PaymentId = paymentId });
        }

        [HttpPost("confirm/{paymentId}")]
        public async Task<IActionResult> Confirm(int paymentId)
        {
            var result = await _mediator.Send(new ConfirmPaymentCommand(paymentId));
            return Ok(new { Success = result });
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Authorization;
using RentCar.Application.Features.Contracts.Commands;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContractsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate")]
        [Authorize(Policy = Permissions.Contracts.Generate)]
        public async Task<IActionResult> Generate([FromBody] GenerateContractCommand command)
        {
            var contractId = await _mediator.Send(command);
            return Ok(new { ContractId = contractId, Message = "Contract generated successfully." });
        }
    }
}

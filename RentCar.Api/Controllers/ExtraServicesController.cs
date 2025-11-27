using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.ExtraServices.Commands;
using RentCar.Application.Features.ExtraServices.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtraServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExtraServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllExtraServicesQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _mediator.Send(new GetExtraServiceByIdQuery { Id = id });
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExtraServiceCommand cmd)
            => Ok(await _mediator.Send(cmd));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateExtraServiceCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();
            return await _mediator.Send(cmd) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _mediator.Send(new DeleteExtraServiceCommand { Id = id })
                ? NoContent()
                : NotFound();
    }
}

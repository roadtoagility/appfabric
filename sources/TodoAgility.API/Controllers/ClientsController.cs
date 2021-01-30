
using FluentMediator;
using Microsoft.AspNetCore.Mvc;
using System;
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.Framework;
using TodoAgility.Business.QueryHandlers;
using TodoAgility.Business.QueryHandlers.Filters;

namespace TodoAgility.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public IActionResult List([FromQuery] string name)
        {
            var result = _mediator.Send<GetClientsResponse>(GetClientsByFilter.From(name));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _mediator.Send<GetClientResponse>(GetClientByIdFilter.From(id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _mediator.Send<ExecutionResult>(new RemoveUserCommand{Id = id});
            return Ok(result);
        }
        
        [HttpPost("save")]
        public IActionResult Save([FromBody] AddUserCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);
            return Ok(result);
        }
    }
}


using FluentMediator;
using Microsoft.AspNetCore.Mvc;
using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers;
using AppFabric.Business.QueryHandlers.Filters;

namespace AppFabric.API.Controllers
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

            if (result.Items.Count.Equals(0))
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _mediator.Send<GetClientResponse>(GetClientByIdFilter.From(id));
            
            return Ok(result);
        }

        [HttpDelete("{id}/{version}")]
        public IActionResult Delete(Guid id, int version)
        {
            var result = _mediator.Send<ExecutionResult>(new RemoveUserCommand{Id = id, Version = version});
            return Ok(result);
        }
        
        [HttpPost("save")]
        public IActionResult Save([FromBody] AddUserCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);
            
            if (!result.IsSucceed)
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }
    }
}

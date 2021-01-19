using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.API.Mock;
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
        public IActionResult List([FromQuery] string razaoSocial, [FromQuery] string name)
        {
            var result = _mediator.Send<GetProjectsResponse>(GetClientsByFilter.From(razaoSocial, name));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _mediator.Send<GetClientResponse>(GetClientByIdFilter.From(id));
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

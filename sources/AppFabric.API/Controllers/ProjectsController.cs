using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers;
using AppFabric.Business.QueryHandlers.Filters;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;

namespace AppFabric.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public IActionResult List([FromQuery] string name)
        {
            var result = _mediator.Send<GetProjectsResponse>(GetProjectsByFilter.From(name));
            return Ok(result);
        }

        [HttpGet("list/{clientId}/{name?}")]
        public IActionResult List(Guid clientId, string name)
        {
            var result = _mediator.Send<GetProjectsResponse>(GetProjectsByClientAndNameFilter.From(name, clientId));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _mediator.Send<GetProjectResponse>(GetProjectByIdFilter.From(id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _mediator.Send<ExecutionResult>(new RemoveProjectCommand(id));
            return Ok(result);
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] AddProjectCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }

        [HttpPut("save/{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateProjectCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateProjectCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }
    }
}
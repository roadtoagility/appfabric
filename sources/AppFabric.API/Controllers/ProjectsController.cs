using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppFabric.API.Mock;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers;
using AppFabric.Business.QueryHandlers.Filters;

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
        public async Task<ActionResult<object>> List(int clientId, string name = "")
        {
            var projects = ProjectsMock.GetProjects();

            projects = projects.Where(x => x.ClientId == clientId && x.Nome.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            return await Task.FromResult(projects);
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
            var result = _mediator.Send<ExecutionResult>(new RemoveProjectCommand{Id = id});
            return Ok(result);
        }
        
        [HttpPut("save")]
        public IActionResult Save([FromBody] AddProjectCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }
        
        [HttpPost("save/{id}")]
        public IActionResult Update(Guid id, UpdateProjectCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }
    }
}

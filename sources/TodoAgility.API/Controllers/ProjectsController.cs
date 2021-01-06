using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.API.Mock;
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.Framework;

namespace TodoAgility.API.Controllers
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
        public async Task<ActionResult<object>> List([FromQuery] string name)
        {
            var projects = ProjectsMock.GetProjects();

            if (!string.IsNullOrEmpty(name))
                projects = projects.Where(x => x.Nome.Contains(name)).ToList();

            return await Task.FromResult(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> Get(int id)
        {
            var entity = ProjectsMock.GetProjects().FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(entity);
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] AddProjectCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }
    }
}

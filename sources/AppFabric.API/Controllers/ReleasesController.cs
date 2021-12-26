using System;
using System.Linq;
using System.Threading.Tasks;
using AppFabric.API.Mock;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;

namespace AppFabric.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReleasesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<ActionResult<object>> List([FromQuery] string status)
        {
            var releases = ReleaseMock.GetReleases();

            if (!string.IsNullOrEmpty(status))
                releases = releases.Where(x => x.Status.Contains(status, StringComparison.OrdinalIgnoreCase)).ToList();

            return await Task.FromResult(releases);
        }

        [HttpGet("list/{clientId}/{status?}")]
        public async Task<ActionResult<object>> List(int clientId, string status = "")
        {
            var releases = ReleaseMock.GetReleases();

            if (!string.IsNullOrEmpty(status))
                releases = releases.Where(x =>
                    x.Id == clientId && x.Status.Contains(status, StringComparison.OrdinalIgnoreCase)).ToList();

            return await Task.FromResult(releases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var entity = ReleaseMock.GetReleases().FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(entity);
        }

        [HttpPost("save")]
        public async Task<ActionResult<object>> Save([FromBody] ClientDto entity)
        {
            //var query = ActivityByProjectFilter.For(dto.ProjectId);
            //var result = await _mediator.Send(query);
            //return result;
            return await Task.FromResult("");
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateReleaseCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Create([FromBody] AddActivityCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }
    }
}
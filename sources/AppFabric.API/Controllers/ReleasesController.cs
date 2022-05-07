using System;
using System.Threading.Tasks;
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
        public IAsyncResult List([FromQuery] string status)
        {
            // var releases = ReleaseMock.GetReleases();
            //
            // if (!string.IsNullOrEmpty(status))
            //     releases = releases.Where(x => x.Status.Contains(status, StringComparison.OrdinalIgnoreCase)).ToList();
            //
            // return await Task.FromResult(releases);
            return Task.CompletedTask;
        }

        [HttpGet("list/{clientId}/{status?}")]
        public IAsyncResult List(int clientId, string status = "")
        {
            // var releases = ReleaseMock.GetReleases();
            //
            // if (!string.IsNullOrEmpty(status))
            //     releases = releases.Where(x =>
            //         x.Id == clientId && x.Status.Contains(status, StringComparison.OrdinalIgnoreCase)).ToList();
            //
            // return await Task.FromResult(releases);
            return Task.CompletedTask;
        }

        [HttpGet("{id}")]
        public IAsyncResult Get(int id)
        {
            // var entity = ReleaseMock.GetReleases().FirstOrDefault(x => x.Id == id);
            // return await Task.FromResult(entity);
            return Task.CompletedTask;
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
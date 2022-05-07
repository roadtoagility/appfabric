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
    public class BillingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BillingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public IAsyncResult List([FromQuery] string clientName)
        {
            // var releases = BillingMock.GetBillings();
            //
            // if (!string.IsNullOrEmpty(clientName))
            //     releases = releases.Where(x => x.Client.Contains(clientName, StringComparison.OrdinalIgnoreCase))
            //         .ToList();
            //
            // return await Task.FromResult(releases);
            return Task.CompletedTask;
        }

        [HttpGet("{id}")]
        public IAsyncResult Get(int id)
        {
            return Task.CompletedTask;
        }

        [HttpPost("save")]
        public async Task<ActionResult<object>> Save([FromBody] AddUserCommand entity)
        {
            //var query = ActivityByProjectFilter.For(dto.ProjectId);
            //var result = await _mediator.Send(query);
            //return result;
            return await Task.FromResult("");
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateBillingCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Create([FromBody] AddReleaseCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }
    }
}
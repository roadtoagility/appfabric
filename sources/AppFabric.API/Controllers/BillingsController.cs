using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppFabric.API.Mock;
using AppFabric.API.Models;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;

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
        public async Task<ActionResult<object>> List([FromQuery] string clientName)
        {
            var releases = BillingMock.GetBillings();

            if (!string.IsNullOrEmpty(clientName))
                releases = releases.Where(x => x.Client.Contains(clientName, StringComparison.OrdinalIgnoreCase)).ToList();

            return await Task.FromResult(releases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var entity = BillingMock.GetBillings().FirstOrDefault(x => x.Id == id);
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

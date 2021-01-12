using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.API.Mock;
using TodoAgility.API.Models;

namespace TodoAgility.API.Controllers
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
                releases = releases.Where(x => x.Id == clientId && x.Status.Contains(status, StringComparison.OrdinalIgnoreCase)).ToList();

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.API.Mock;

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
        public async Task<ActionResult<object>> List([FromQuery] string razaoSocial)
        {
            var activities = ClientsMock.GetClients();

            if (!string.IsNullOrEmpty(razaoSocial))
                activities = activities.Where(x => x.RazaoSocial.Contains(razaoSocial, StringComparison.OrdinalIgnoreCase)).ToList();

            return await Task.FromResult(activities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var entity = ClientsMock.GetClients().FirstOrDefault(x => x.Id == id);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.API.Models;

namespace TodoAgility.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<ActionResult<object>> ProjetosReleases([FromQuery] string filter)
        //{
        //    //var query = ActivityByProjectFilter.For(dto.ProjectId);
        //    //var result = await _mediator.Send(query);
        //    //return result;
        //    return "";
        //}

        //[HttpGet]
        //public async Task<ActionResult<object>> AtividadesConcluidas([FromQuery] string filter)
        //{
        //    //var query = ActivityByProjectFilter.For(dto.ProjectId);
        //    //var result = await _mediator.Send(query);
        //    //return result;
        //    return "";
        //}

        //[HttpGet]
        //public async Task<ActionResult<object>> ProjetosFavoritos([FromQuery] string filter)
        //{
        //    //var query = ActivityByProjectFilter.For(dto.ProjectId);
        //    //var result = await _mediator.Send(query);
        //    //return result;
        //    return "";
        //}

        //[HttpGet]
        //public async Task<ActionResult<object>> FaturamentoClients([FromQuery] string filter)
        //{
        //    //var query = ActivityByProjectFilter.For(dto.ProjectId);
        //    //var result = await _mediator.Send(query);
        //    //return result;
        //    return "";
        //}

        //[HttpPost]
        //public async Task<ActionResult<object>> UpdateActivityStatus([FromBody] object entity)
        //{
        //    //var query = ActivityByProjectFilter.For(dto.ProjectId);
        //    //var result = await _mediator.Send(query);
        //    //return result;
        //    return "";
        //}
    }
}

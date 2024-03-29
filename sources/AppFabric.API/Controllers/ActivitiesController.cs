﻿using System;
using System.Threading.Tasks;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;

namespace AppFabric.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public IAsyncResult List([FromQuery] string title)
        {
            // var activities = ActivitiesMock.GetActivities();
            //
            // if (!string.IsNullOrEmpty(title))
            //     activities = activities.Where(x => x.Titulo.Contains(title)).ToList();
            //
            //return await Task.FromResult(activities);
            return Task.CompletedTask;
        }

        [HttpGet("list/{projectId}/{title?}")]
        public IAsyncResult List(int projectId, string title = "")
        {
            // var activities = ActivitiesMock.GetActivities();
            //
            // activities = activities
            //     .Where(x => x.Id == projectId && x.Titulo.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            //
            // return await Task.FromResult(activities);
            return Task.CompletedTask;
        }

        [HttpGet("{id}")]
        public IAsyncResult Get(int id)
        {
            // var entity = ActivitiesMock.GetActivities().FirstOrDefault(x => x.Id == id);
            // return await Task.FromResult(entity);
            return Task.CompletedTask;
        }

        [HttpPost("save")]
        public async Task<ActionResult<object>> Save([FromBody] AddActivityCommand entity)
        {
            //var query = ActivityByProjectFilter.For(dto.ProjectId);
            //var result = await _mediator.Send(query);
            //return result;
            return await Task.FromResult("");
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateActivityCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }

        [HttpPost("close")]
        public IActionResult Create([FromBody] CloseActivityCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }

        [HttpPost("asign")]
        public IActionResult Create([FromBody] AssignResponsibleCommand entity)
        {
            var result = _mediator.Send<ExecutionResult>(entity);

            return Ok(result);
        }
    }
}
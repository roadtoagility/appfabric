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
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ProjectReleases")]
        public async Task<ActionResult<object>> ProjectReleases()
        {
            return await Task.FromResult(DashboardMock.GetProjectReleases());
        }

        [HttpGet("FinishedActivities")]
        public async Task<ActionResult<object>> FinishedActivities()
        {
            return await Task.FromResult(DashboardMock.GetFinishedActivities());
        }

        [HttpGet("FavoritedProjects")]
        public async Task<ActionResult<object>> FavoritedProjects()
        {
            return await Task.FromResult(DashboardMock.GetFavoritedProjects());
        }

        [HttpGet("ClientsRevenue")]
        public async Task<ActionResult<object>> ClientsRevenue()
        {
            return await Task.FromResult(DashboardMock.GetClientsRevenue());
        }

        [HttpPost]
        public async Task<ActionResult<object>> UpdateActivityStatus([FromBody] object entity)
        {
            return await Task.FromResult(Task.CompletedTask);
        }
    }
}

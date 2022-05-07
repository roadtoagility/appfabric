using System;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;

namespace AppFabric.API.Controllers
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
        public IAsyncResult ProjectReleases()
        {
            // return await Task.FromResult(DashboardMock.GetProjectReleases());
            return Task.CompletedTask;
        }

        [HttpGet("FinishedActivities")]
        public IAsyncResult FinishedActivities()
        {
            // return await Task.FromResult(DashboardMock.GetFinishedActivities());
            return Task.CompletedTask;
        }

        [HttpGet("FavoritedProjects")]
        public IAsyncResult FavoritedProjects()
        {
            // return await Task.FromResult(DashboardMock.GetFavoritedProjects());
            return Task.CompletedTask;
        }

        [HttpGet("ClientsRevenue")]
        public IAsyncResult ClientsRevenue()
        {
            // return await Task.FromResult(DashboardMock.GetClientsRevenue());
            return Task.CompletedTask;
        }

        [HttpPost]
        public async Task<ActionResult<object>> UpdateActivityStatus([FromBody] object entity)
        {
            return await Task.FromResult(Task.CompletedTask);
        }
    }
}
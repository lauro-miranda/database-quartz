using LAB.DatabaseQuartz.Api.Infra.Quartz.Scheduler;
using Microsoft.AspNetCore.Mvc;

namespace LAB.DatabaseQuartz.Api.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        ICustomScheduler Scheduler { get; }

        public JobController(ICustomScheduler customScheduler)
        {
            Scheduler = customScheduler;
        }

        [HttpGet, Route("group-name/{groupName}")]
        public async Task<IActionResult> GetAsync([FromRoute] string groupName)
        {
            return Ok();
        }

        [HttpPost, Route("{name}/group-name/{groupName}")]
        public async Task<IActionResult> RunAsync([FromRoute] string name, [FromRoute] string groupName)
        {
            await Scheduler.ExecuteAsync(name, groupName);
            return Ok();
        }
    }
}
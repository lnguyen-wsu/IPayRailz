using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HangFire_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayRailzController : ControllerBase
    {
        // Continuous Jobs
        [HttpPost]
        [Route("[action]")]
        public IActionResult Confirm()
        {
            var OriginalJob = BackgroundJob.Schedule(() => sayMessage("Hello!"), TimeSpan.FromSeconds(20));
            var jobId = BackgroundJob.ContinueJobWith(OriginalJob, () => sayMessage("You were unsubscribed!"));
            return Ok($"JobId {jobId} as Confirmation has been sent");
        }

        // Recurring Jobs
        [HttpPost]
        [Route("[action]")]
        public IActionResult DataBaseUpdate()
        {
            RecurringJob.AddOrUpdate(() => sayMessage(" Payrailz Job update ! "), Cron.Minutely);
            return Ok($"JobId Payrailz has been sent");
        }

        public void sayMessage(string v) => Console.WriteLine(v);
    }
}

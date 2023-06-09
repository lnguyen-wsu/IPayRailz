﻿using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;

namespace HangFire_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpassController : ControllerBase
    {
        // Fire and forget 
        [HttpPost]      
        [Route("[action]")]
        public IActionResult SetWelcome()
        {
            var jobId = BackgroundJob.Enqueue(()=>sayMessage("Hello Hackathon From Ipass!"));
            return Ok($"JobId {jobId} has been sent");
        }

        // Delayed Jobs
        [HttpPost]
        [Route("[action]")]
        public IActionResult Discount()
        {
            var jobId = BackgroundJob.Schedule(() => sayMessage("Hello Hackathon!"),TimeSpan.FromSeconds(20));
            return Ok($"JobId {jobId} as Discount has been sent");
        }

        // Recurring Jobs
        [HttpPost]
        [Route("[action]")]
        public IActionResult DataBaseUpdate()
        {
            RecurringJob.AddOrUpdate(() => sayMessage("Ipass Job update ! "), Cron.Minutely);
            return Ok($"JobId Ipass has been sent");
        }

        // Continuous Jobs
        [HttpPost]
        [Route("[action]")]
        public IActionResult Confirm()
        {
            var OriginalJob = BackgroundJob.Schedule(() => sayMessage("Hello Hackathon!"), TimeSpan.FromSeconds(20));
            var jobId = BackgroundJob.ContinueJobWith(OriginalJob, ()=> sayMessage("Hackathon Testing!"));
            return Ok($"JobId {jobId} as Confirmation has been sent");
        }


        public void sayMessage(string v) => Console.WriteLine(v);
    }
}

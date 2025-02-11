using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController(AppDbContext context): BaseApiController
    {
        [HttpGet]
        /*Returning a HTTP response in the form or a list that contains activity objects using an HTTP get method */
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await context.Activities.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Activity>> GetActivityDetail(string id)
        {
            var activity = await context.Activities.FindAsync(id);

            if (activity == null) return NotFound();

            return activity;
        }
    }
}
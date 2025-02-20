using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities.Commands;
using Application.Activities.DTO;
using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController: BaseApiController
    {
        [HttpGet]
        /*Returning a HTTP response in the form or a list that contains activity objects using an HTTP get method */
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await Mediator.Send(new GetACtivityList.Query());
            
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Activity>> GetActivityDetail(string id)
        {
            // var activity = await context.Activities.FindAsync(id);

            // if (activity == null) return NotFound();

            // return activity;

            

            return HandleResult( await Mediator.Send(new GetActivityDetails.Query{ Id = id }));

            
        }

        [HttpPost]

        public async Task<ActionResult<string>> CreateActivity(CreateActivityDto activityDto)
        {
           return HandleResult(await Mediator.Send(new CreateActivity.Command{ActivityDto = activityDto}));
        }

        [HttpPut/*("{id}")*/]

        public async Task<ActionResult> EditActivity(EditActivityDto activity/*, string id*/)
        {
           /* activity.Id = id;*/
            // Ask the mediator to send a request to edit the activity that was passed in the request body
           return HandleResult(await Mediator.Send(new EditActivity.Command{ActivityDto = activity}));
          
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteActivity(string id )
        {
            // Ask the mediator to send a request to delete the activity that was passed in the request body
            return HandleResult(await Mediator.Send(new DeleteActivity.Command{Id = id}));
            
        }
    }
}
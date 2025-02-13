using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands
{
    public class EditActivity
    { // the EditActivity class will be used to edit an activity by taking in the activity object passed in 
        public class Command : IRequest
        { 
            public required Activity Activity{ get; set; } // The activity object is set to the object passed in the request body(activities controller)
        }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken) ?? throw new Exception("Could not find activity");;

                // activity.Title = request.Activity.Title;

                mapper.Map(request.Activity, activity);

                await context.SaveChangesAsync(cancellationToken);

             
            }
        }
    }
}
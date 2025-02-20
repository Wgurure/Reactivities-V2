using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities.DTO;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands
{
    public class EditActivity
    { // the EditActivity class will be used to edit an activity by taking in the activity object passed in 
        public class Command : IRequest<Result<Unit>>
        { 
            public required EditActivityDto ActivityDto{ get; set; } // The activity object is set to the object passed in the request body(activities controller)
        }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit> >
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.FindAsync([request.ActivityDto.Id], cancellationToken) ;;
                if(activity == null) return Result<Unit>.Failure("Activity not found", 404);
                // activity.Title = request.Activity.Title;

                mapper.Map(request.ActivityDto, activity);

                var result = await context.SaveChangesAsync(cancellationToken)> 0;

                if(!result) return Result<Unit>.Failure("Failed to edit the activity", 400);

                return Result<Unit>.Success(Unit.Value);     
             
            }
        }
    }
}
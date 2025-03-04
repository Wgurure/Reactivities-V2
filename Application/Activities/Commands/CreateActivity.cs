using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities.DTO;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands
{
    public class CreateActivity
    {
        public class Command : IRequest<Result<string>>
        {
           
            public required CreateActivityDto ActivityDto { get; set; }
        }

// Takes
        public class Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Command,Result<string>>
        {
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userAccessor.GetUserAsync();

                var activity = mapper.Map<Activity>(request.ActivityDto);

                context.Activities.Add(activity);
                
// After the activity is created it creates a new Activity attendee in th ActivityAttendee table with the host being the current user 
                var attendee = new ActivityAttendee 
                {
                    ActivityId = activity.Id,
                    UserId = user.Id,
                    IsHost = true
                };

                activity.Attendees.Add(attendee);

                var result = await context.SaveChangesAsync(cancellationToken)> 0;

                if(!result) return Result<string>.Failure("Failed to create the activity", 400);

                return Result<string>.Success(activity.Id);  
            }

            
        }
    }
}
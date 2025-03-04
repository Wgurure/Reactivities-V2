using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities.DTO;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public class GetActivityDetails
    {
         public class Query : IRequest<Result<ActivityDto>> 
         {
            public required string Id { get; set; }
         }

         public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, Result<ActivityDto>>
         {
             public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
             {

                // /** Using Eager Loading ****/
                //  var activity = await context.Activities // Load the activities table 
                //                             .Include(x => x.Attendees) // Load the attendees associated with the activity in the Activity Attendees table 
                //                             .ThenInclude(x => x.User)   // Load the related users for that activity in the Attendees table 
                //                             .FirstOrDefaultAsync(x => request.Id == x.Id, cancellationToken); // It then fetches the first matching record in the joined table

                /** Using Projection ****/

                var activity = await context.Activities
                .ProjectTo<ActivityDto>(mapper.ConfigurationProvider) // Only fetches the data specified in ActivityDto
                .FirstOrDefaultAsync(x => request.Id == x.Id, cancellationToken);

                 if (activity == null) return Result<ActivityDto>.Failure("Activity not found", 404);
                

                /** Using Eager Loading **/
                //return Result<ActivityDto>.Success(mapper.Map<ActivityDto>(activity));

                return Result<ActivityDto>.Success(activity);

         }
}}}
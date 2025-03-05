using System;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Commands;

public class EditProfile {

        public class Command : IRequest<Result<Unit>>
    {
        // The things that the user will provide when they make a request to this endpoint
        public string DisplayName { get; set; } = "";

        public string Bio { get; set; } = "";
    }

   
    public class Handler(IUserAccessor userAccessor, AppDbContext context ) : IRequestHandler<Command, Result<Unit>>
    {
         public async Task<Result<Unit>> Handle(  Command request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserAsync();
            
            user.DisplayName = request.DisplayName;
            user.Bio = request.Bio;

             var result = await context.SaveChangesAsync(cancellationToken);

             if (result == 0) return Result<Unit>.Failure("Failed to update the activity", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }

}

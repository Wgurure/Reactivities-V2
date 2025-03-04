using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities.Queries
{
    public class GetACtivityList
    {
        public class Query : IRequest<List<ActivityDto>> {}

        public class Handler(AppDbContext context/*ILogger<GetACtivityList> logger*/, IMapper mapper) : IRequestHandler<Query, List<ActivityDto>>
        {
            public async Task<List<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                // try
                // {
                //     for (int i = 0; i < 10; i++)
                //     {
                //        cancellationToken.ThrowIfCancellationRequested();
                //        await Task.Delay(1000, cancellationToken);
                //        logger.LogInformation($"Task {i} has completed");
                //     }
                // }
                // catch (System.Exception)
                // {
                    
                //     logger.LogInformation("Task was cancelled");
                // }
                return await context.Activities.ProjectTo<ActivityDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }
        }
          
        
    }

    internal class DataContext
    {
    }
}
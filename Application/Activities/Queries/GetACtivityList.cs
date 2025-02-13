using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities.Queries
{
    public class GetACtivityList
    {
        public class Query : IRequest<List<Activity>> {}

        public class Handler(AppDbContext context/*ILogger<GetACtivityList> logger*/) : IRequestHandler<Query, List<Activity>>
        {
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
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
                return await context.Activities.ToListAsync(cancellationToken);
            }
        }
          
        
    }

    internal class DataContext
    {
    }
}
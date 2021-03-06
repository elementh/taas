﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaaS.Core.Domain.Category.Dto;
using TaaS.Persistence.Context;

namespace TaaS.Core.Domain.Category.Query.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        protected readonly ILogger<GetAllCategoriesQueryHandler> Logger;
        protected readonly TaaSDbContext Context;

        public GetAllCategoriesQueryHandler(ILogger<GetAllCategoriesQueryHandler> logger, TaaSDbContext context)
        {
            Logger = logger;
            Context = context;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Requested categories list.");

            var query = Context.Categories.AsNoTracking();

            if (request.Language != null)
            {
                query = query.Where(c => c.Gratitudes.Any(gc => gc.Gratitude.Language.ToLower() == request.Language));
            }

            var categories = await query
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Title = c.Title,
                })
                .ToListAsync(cancellationToken);

            return categories;
        }
    }
}
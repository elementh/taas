﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaaS.Core.Domain.Gratitude.Dto;
using TaaS.Persistence.Context;

namespace TaaS.Core.Domain.Gratitude.Query.GetGratitudeById
{
    public class GetGratitudeByIdQueryHandler : IRequestHandler<GetGratitudeByIdQuery, GratitudeDto?>
    {
        protected readonly ILogger<GetGratitudeByIdQueryHandler> Logger;
        protected readonly TaaSDbContext Context;

        public GetGratitudeByIdQueryHandler(ILogger<GetGratitudeByIdQueryHandler> logger, TaaSDbContext context)
        {
            Logger = logger;
            Context = context;
        }

        public async Task<GratitudeDto?> Handle(GetGratitudeByIdQuery request, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Requested gratitude by id.");

            var gratitude = await Context.Gratitudes.AsNoTracking()
                .Where(g => g.Id == request.Id)
                .Select(g => new GratitudeDto
                {
                    Id = g.Id,
                    Language = g.Language,
                    Text = g.Text,
                    Categories = g.Categories.Select(gc => gc.Category.Title)
                }).FirstOrDefaultAsync(cancellationToken);

            return gratitude;
        }
    }
}
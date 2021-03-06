﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaaS.Core.Domain.Language.Dto;
using TaaS.Persistence.Context;

namespace TaaS.Core.Domain.Language.Query.GetLanguageByCode
{
    public class GetLanguageByCodeQueryHandler : IRequestHandler<GetLanguageByCodeQuery, LanguageDetailDto?>
    {
        protected readonly ILogger<GetLanguageByCodeQueryHandler> Logger;
        protected readonly TaaSDbContext Context;

        public GetLanguageByCodeQueryHandler(ILogger<GetLanguageByCodeQueryHandler> logger, TaaSDbContext context)
        {
            Logger = logger;
            Context = context;
        }

        public async Task<LanguageDetailDto?> Handle(GetLanguageByCodeQuery request, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Language detail requested");

            var query = Context.Gratitudes.AsNoTracking()
                .Where(g => g.Language.ToLower() == request.Code.ToLower());
            
            var language = await query
                .Select(g => new LanguageDetailDto(g.Language, query.Count()))
                .FirstOrDefaultAsync(cancellationToken);

            return language;
            
        }
    }
}
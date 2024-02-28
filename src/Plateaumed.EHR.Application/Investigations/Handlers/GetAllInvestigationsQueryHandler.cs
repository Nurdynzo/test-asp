using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Plateaumed.EHR.Investigations.Abstractions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetAllInvestigationsQueryHandler : IGetAllInvestigationsQueryHandler
    {
        private readonly IRepository<Investigation, long> _repository;

        public GetAllInvestigationsQueryHandler(IRepository<Investigation, long> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllInvestigationsResponse>> Handle(GetAllInvestigationsRequest request)
        {
            var type = !string.IsNullOrWhiteSpace(request.Type) ? request.Type.ToLower() : null;
            var filter = !string.IsNullOrWhiteSpace(request.Filter) ? request.Filter.ToLower() : null;

            return await _repository.GetAll()
                .Where(x => x.PartOfId == null)
                .WhereIf(type != null, x => x.Type.ToLower() == type)
                .WhereIf(filter != null,
                    Filter(filter)
                ).Select(i => new GetAllInvestigationsResponse
                {
                    Id = i.Id,
                    Name = i.Name,
                    Specimen = i.Specimen,

                }).ToListAsync();
        }

        private static Expression<Func<Investigation, bool>> Filter(string filter) =>
            x => x.Name.ToLower().Contains(filter)
                 || !string.IsNullOrWhiteSpace(x.ShortName) && x.ShortName.ToLower().Contains(filter)
                 || !string.IsNullOrWhiteSpace(x.Synonyms) && x.Synonyms.ToLower().Contains(filter)
                 || x.Components.Any(y => y.Name.ToLower().Contains(filter) 
                                          || !string.IsNullOrWhiteSpace(y.ShortName) && y.ShortName.ToLower().Contains(filter)
                                          || !string.IsNullOrWhiteSpace(y.Synonyms) && y.Synonyms.ToLower().Contains(filter));
    }
}

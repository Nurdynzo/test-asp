using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetInvestigationResultsQueryHandler : IGetInvestigationResultsQueryHandler
    {
        private readonly IRepository<InvestigationResult, long> _investigationResultsRepository;
        private readonly IRepository<User,long> _userRepository;
        private readonly IObjectMapper _objectMapper;

        public GetInvestigationResultsQueryHandler(IRepository<InvestigationResult, long> investigationResultsRepository, IObjectMapper objectMapper, IRepository<User, long> userRepository)
        {
            _investigationResultsRepository = investigationResultsRepository;
            _objectMapper = objectMapper;
            _userRepository = userRepository;
        }

        public async Task<List<GetInvestigationResultsResponse>> Handle(GetInvestigationResultsRequest request)
        {
            return await (from i in _investigationResultsRepository.GetAll()
                              .Include(x => x.InvestigationComponentResults)
                              .Include(x => x.Investigation)
                              .Where(x => x.PatientId == request.PatientId)
                              .WhereIf(!string.IsNullOrWhiteSpace(request.Type), x => x.Investigation.Type.ToLower() == request.Type.ToLower())
                              .WhereIf(!string.IsNullOrWhiteSpace(request.Filter), x => x.Investigation.Name.ToLower().Contains(request.Filter.ToLower()))
                              .WhereIf(request.ProcedureId.HasValue, v => v.ProcedureId == request.ProcedureId.Value)
                              .OrderByDescending(x => x.CreationTime)
                          join u in _userRepository.GetAll() on i.DeleterUserId equals u.Id into user
                          from u in user.DefaultIfEmpty()
                          select new GetInvestigationResultsResponse
                          {
                              PatientId = i.PatientId,
                              InvestigationId = i.InvestigationId,
                              InvestigationRequestId = i.InvestigationRequestId,
                              Name = i.Investigation != null ? i.Investigation.Name : "",
                              Type = i.Investigation != null ? i.Investigation.Type : "",
                              Reference = i.Reference,
                              SampleCollectionDate = i.SampleCollectionDate,
                              ResultDate = i.ResultDate,
                              SampleTime = i.SampleTime,
                              ResultTime = i.ResultTime,
                              Specimen = i.Specimen,
                              Conclusion = i.Conclusion,
                              SpecificOrganism = i.SpecificOrganism,
                              View = i.View,
                              Notes = i.Notes,
                              CreationTime = i.CreationTime,
                              ProcedureId = i.ProcedureId,
                              IsDeleted = i.IsDeleted,
                              DeletedUser = u != null ? u.DisplayName : "",
                              InvestigationComponentResults = _objectMapper.Map<List<InvestigationComponentResultDto>>(i.InvestigationComponentResults),
                          })
                .ToListAsync();
        }
    }
}

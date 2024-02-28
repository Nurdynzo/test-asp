using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetElectroRadPulmRecentResultQueryHandler : IGetElectroRadPulmRecentResultQueryHandler
    {
        private readonly IRepository<ElectroRadPulmInvestigationResult, long> _repository;        
        private readonly IRepository<Patient, long> _patientRepository;

        public GetElectroRadPulmRecentResultQueryHandler(IRepository<ElectroRadPulmInvestigationResult, long> repository,
            IRepository<Patient, long> patientRepository)
        {
            _repository = repository;
            _patientRepository = patientRepository;
        }

        public async Task<List<ElectroRadPulmInvestigationResultResponseDto>> Handle(GetInvestigationResultWithNameTypeFilterDto request)
        {
            await ValidateInput(request.PatientId);
            return await GetInvestigationResult(request);
        }

        private async Task<List<ElectroRadPulmInvestigationResultResponseDto>> GetInvestigationResult(GetInvestigationResultWithNameTypeFilterDto request)
        {
            var name = string.IsNullOrWhiteSpace(request.Name) ? null : request.Name.ToLower();
            var type = string.IsNullOrWhiteSpace(request.Type) ? null : request.Type.ToLower();

            return await _repository.GetAll()
                                 .Where(x => x.PatientId == request.PatientId)
                                 .Include(x => x.ResultImages)
                                 .Include(x => x.Investigation)
                                 .WhereIf(name != null, x => x.Investigation.Name.ToLower().Contains(name))
                                 .WhereIf(type != null, x => x.Investigation.Type.ToLower().Contains(type))
                                 .WhereIf(request.ProcedureId.HasValue, x => x.ProcedureId == request.ProcedureId.Value)
                                 .Select(x => new ElectroRadPulmInvestigationResultResponseDto
                                 {
                                     Name = x.Investigation.Name,
                                     Type = x.Investigation.Type,
                                     PatientId = x.PatientId,
                                     InvestigationId = x.InvestigationId,
                                     InvestigationRequestId = x.InvestigationRequestId,
                                     Conclusions = x.Conclusion,
                                     ResultDate = x.ResultDate,
                                     ResultTime = x.ResultTime,
                                     ResultImageUrls = x.ResultImages.Select(x => x.ImageUrl).ToList(),
                                     CreationTime = x.CreationTime,
                                     ProcedureId = x.ProcedureId
                                 }).ToListAsync();
        }

        private async Task ValidateInput(long patientId)
        {
            _ = await _patientRepository.GetAll().Where(x => x.Id == patientId).FirstOrDefaultAsync() ??
                throw new UserFriendlyException("Patient not found");          
        }
    }
}


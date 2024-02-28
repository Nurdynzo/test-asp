using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetElectroRadPulmInvestigationResultQueryHandler : IGetElectroRadPulmInvestigationResultQueryHandler
    {
        private readonly IRepository<ElectroRadPulmInvestigationResult, long> _repository;
        private readonly IRepository<InvestigationRequest, long> _investigationRequest;
        private readonly IRepository<Patient, long> _patientRepository;

        public GetElectroRadPulmInvestigationResultQueryHandler(IRepository<ElectroRadPulmInvestigationResult, long> repository,
            IRepository<Patient, long> patientRepository,
            IRepository<InvestigationRequest, long> investigationRequest)
        {
            _repository = repository;
            _patientRepository = patientRepository;
            _investigationRequest = investigationRequest;
        }

        public async Task<ElectroRadPulmInvestigationResultResponseDto> Handle(long patientId, long investigationRequestId)
        {
            await ValidateInput(patientId, investigationRequestId);

            return await GetInvestigationResult(patientId, investigationRequestId);
        }

        private async Task<ElectroRadPulmInvestigationResultResponseDto> GetInvestigationResult(long patientId, long investigationRequestId)
        {
            var query = await _repository.GetAll()
                               .Where(x => x.PatientId == patientId && x.InvestigationRequestId == investigationRequestId)
                               .Include(x=>x.Investigation)
                               .Include(x => x.ResultImages)
                               .FirstOrDefaultAsync();
            return new ElectroRadPulmInvestigationResultResponseDto
            {
                InvestigationId = query.InvestigationId,
                Name = query.Investigation.Name,
                Type= query.Investigation.Type,
                PatientId = query.PatientId,
                InvestigationRequestId = query.InvestigationRequestId,
                Conclusions = query.Conclusion,
                ResultDate = query.ResultDate,
                ResultTime = query.ResultTime,
                ResultImageUrls = query.ResultImages.Select(x=>x.ImageUrl).ToList()
            };
        }

        private async Task ValidateInput(long patientId, long investigationRequestId)
        {  
            _ = await _patientRepository.GetAll().Where(x=>x.Id == patientId).FirstOrDefaultAsync() ??  throw new UserFriendlyException("Patient not found");
            _ = await _investigationRequest.GetAll().Where(x=>x.Id ==investigationRequestId).FirstOrDefaultAsync() ?? throw new UserFriendlyException("Investigation Request not found");            
        }        
    }
}


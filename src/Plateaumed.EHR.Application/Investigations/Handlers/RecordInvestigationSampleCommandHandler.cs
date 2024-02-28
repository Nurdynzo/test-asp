using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class RecordInvestigationSampleCommandHandler: IRecordInvestigationSampleCommandHandler
    {
        private readonly IRepository<InvestigationResult, long> _investigationResultRepository;
        private readonly IRepository<Investigation, long> _investigationRepository;
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
        private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
        private readonly IAbpSession _abpSession;

        public RecordInvestigationSampleCommandHandler(IRepository<InvestigationResult, long> investigationResultsRepository,
            IRepository<Investigation, long> investigationRepository,
            IRepository<Patient, long> patientRepository,
            IRepository<PatientEncounter, long> patientEncounterRepository,
            IRepository<InvestigationRequest, long> investigationRequestRepository,
            IAbpSession abpSession)
		{
            _investigationResultRepository = investigationResultsRepository;
            _investigationRepository = investigationRepository;
            _patientRepository = patientRepository;
            _patientEncounterRepository = patientEncounterRepository;
            _investigationRequestRepository = investigationRequestRepository;
            _abpSession = abpSession;
		}

        public async Task Handle(RecordInvestigationSampleRequest request)
        {
            var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("User Tenant not found");
            await ValidateInputs(request);           
            await _investigationResultRepository.InsertAsync(new InvestigationResult
            {
                PatientId = request.PatientId,
                InvestigationId = request.InvestigationId,
                InvestigationRequestId = request.InvestigationRequestId,
                EncounterId = request.EncounterId,
                Name = request.NameOfInvestigation,
                SampleCollectionDate = request.SampleCollectionDate,
                SampleTime = request.SampleCollectionTime,
                CreatorUserId = _abpSession.UserId,
                TenantId = tenantId,
                Specimen = request.Specimen,
                ProcedureId = request.ProcedureId
            });
        }

        private async Task ValidateInputs(RecordInvestigationSampleRequest request)
        {
            _ = await _investigationRequestRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == request.InvestigationRequestId) ?? throw new UserFriendlyException("Investigation request not found");
            _ = await _investigationRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == request.InvestigationId) ?? throw new UserFriendlyException("Investigation not found");
            _ = await _patientRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == request.PatientId) ?? throw new UserFriendlyException("Patient not found");
            _ = await _patientEncounterRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.EncounterId) ?? throw new UserFriendlyException("Encounter not found");
        }
    }
}


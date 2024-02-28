using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class LaboratoryQueueViewTestResultQueryHandler : ILaboratoryQueueViewTestResultQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
        private readonly IRepository<InvestigationResult, long> _investigationResultRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingsRepository;
        private readonly IRepository<User, long> _userRepository;

        public LaboratoryQueueViewTestResultQueryHandler(IRepository<Patient, long> patientRepository,
            IRepository<InvestigationRequest, long> investigationRequestRepository,
            IRepository<InvestigationResult, long> investigationResultRepository,
            IRepository<PatientCodeMapping, long> patientCodeMappingsRepository,
            IRepository<User, long> userRepository)
        {
            _patientRepository = patientRepository;
            _investigationRequestRepository = investigationRequestRepository;
            _investigationResultRepository = investigationResultRepository;
            _patientCodeMappingsRepository = patientCodeMappingsRepository;
            _userRepository = userRepository;
        }

        public async Task<ViewTestResultResponse> Handle(ViewTestInfoRequestCommand request)
        {
            await ValidateInputs(request);

            var query = await (from p in _patientRepository.GetAll().Where(x => x.Id.Equals(request.PatientId))
                               join pc in _patientCodeMappingsRepository.GetAll() on p.Id equals pc.PatientId
                               join ir in _investigationRequestRepository.GetAll().Include(x => x.Investigation).Include(x => x.Diagnosis)
                                          .Where(x => x.Id.Equals(request.InvestigationRequestId))
                               on p.Id equals ir.PatientId
                               join u in _userRepository.GetAll().AsNoTracking() on ir.CreatorUserId equals u.Id
                               join iresults in _investigationResultRepository.GetAll().Include(x=>x.InvestigationComponentResults) on ir.Id equals iresults.InvestigationRequestId
                               join creator in _userRepository.GetAll().AsNoTracking() on iresults.CreatorUserId equals creator.Id
                               select new ViewTestResultResponse
                               {
                                   PatientFirstName = p.FirstName ?? "",
                                   PatientLastName = p.LastName ?? "",
                                   PatientAge = $"{DateTime.Now.Year - p.DateOfBirth.Year}yrs",
                                   PatientImageUrl = p.PictureUrl ?? "",
                                   Gender = p.GenderType.ToString() ?? "",
                                   PatientCode = pc.PatientCode ?? "",
                                   RequestorFirstName = u.Name ?? "",
                                   RequestorLastName = u.Surname ?? "",
                                   RequestorTitle = u.Title.ToString() ?? "",
                                   RequestorContactPhoneNumber = u.PhoneNumber ?? "",
                                   RequestorImageUrl = u.ProfilePictureId.ToString() ?? "",
                                   DiagnosisDescription = ir.Diagnosis.Description ?? "",
                                   DiagnosisNotes = ir.Diagnosis.Notes ?? "",
                                   TestName = ir.Investigation.Name ?? "",
                                   Specimen = ir.Investigation.Specimen ?? "",
                                   Organism = ir.Investigation.SpecificOrganism ?? "",
                                   InvestigationRequestNote = ir.Notes ?? "",
                                   TestCategory = ir.Investigation.Type ?? "",
                                   RequestorUnit = "In progress", //TODO this is yet to be implemented, even in LaboratoryQueue
                                   ClinicOrWard = "", //TODO How do we get the ward where the investigation was requested or where the patient belong t0?
                                   ProcessingLabPersonnel = creator.FullName ?? "",
                                   ReviewerFullName = "", //TODO Where to get this data from
                                   TestStatus = ir.InvestigationStatus.ToString() ?? "",
                                   Notes = iresults.Notes ?? "",
                                   Conclusion = iresults.Conclusion ?? "",
                                   DateOfResultCollection = iresults.ResultDate,
                                   TimeOfResultCollection = iresults.ResultTime,
                                   DateOfSampleCollection = iresults.SampleCollectionDate,
                                   TimeOfSampleCollection = iresults.SampleTime,
                                   DateRequested = ir.CreationTime,
                                   InvestigationResultId = iresults.Id,
                                   InvestigationResults = iresults.InvestigationComponentResults.Select(x=>new LabInvestigationResultsDto
                                   {
                                       MaxRange = x.RangeMax,
                                       MinRange = x.RangeMin,
                                       Name = x.Name,
                                       Reference = x.Reference,
                                       Result = x.Result
                                   }).ToList()
                               }).FirstOrDefaultAsync();
            return query;
        }

        private async Task ValidateInputs(ViewTestInfoRequestCommand request)
        {
            _ = await _patientRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.PatientId) ?? throw new UserFriendlyException("Patient not found");
            _ = await _investigationRequestRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.InvestigationRequestId) ?? throw new UserFriendlyException("Investigation Request not found");
        }
    }
}


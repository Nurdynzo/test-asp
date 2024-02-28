using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetLaboratoryQueueTestInfoQueryHandler : IGetLaboratoryQueueTestInfoQueryHandler 
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<InvestigationRequest, long> _investigationRequest;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappings;
        private readonly IRepository<User, long> _userRepository;

        public GetLaboratoryQueueTestInfoQueryHandler (
            IRepository<Patient, long> patientRepository,
            IRepository<InvestigationRequest, long> investigationRequest,
            IRepository<PatientCodeMapping, long> patientCodeMappings,
            IRepository<User, long> userRepository)
        {
            _patientRepository = patientRepository;
            _investigationRequest = investigationRequest;
            _patientCodeMappings = patientCodeMappings;
            _userRepository = userRepository;
        }

        public async Task<ViewTestInfoResponse> Handle(ViewTestInfoRequestCommand request)
        {
            await ValidateInputs(request);
            var query = await (from p in _patientRepository.GetAll().Where(x => x.Id.Equals(request.PatientId))
                               join pc in _patientCodeMappings.GetAll() on p.Id equals pc.PatientId
                               join ir in _investigationRequest.GetAll().Include(x => x.Investigation).Include(x => x.Diagnosis)
                                          .Where(x => x.Id.Equals(request.InvestigationRequestId))
                               on p.Id equals ir.PatientId
                               join u in _userRepository.GetAll().AsNoTracking() on ir.CreatorUserId equals u.Id
                               select new ViewTestInfoResponse
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
                                   RequestorUnit = "In progress", //TODO this is yet to be implemented, even in LaboratoryQueue
                                   RequestorImageUrl = u.ProfilePictureId.ToString() ?? "",
                                   DiagnosisDescription = ir.Diagnosis.Description ?? "",
                                   DiagnosisNotes = ir.Diagnosis.Notes ?? "",
                                   TestName = ir.Investigation.Name ?? "",
                                   Specimen = ir.Investigation.Specimen ?? "",
                                   Organism = ir.Investigation.SpecificOrganism ?? "",
                                   TestCategory = ir.Investigation.Type ?? "",
                                   TestStatus = ir.InvestigationStatus.ToString() ?? "",
                                   ClinicOrWard = "", //TODO How do we get the ward where the investigation was requested or where the patient belong t0?
                                   InvestigationRequestNotes = ir.Notes ?? "",
                                   DateRequested = ir.CreationTime
                               }).FirstOrDefaultAsync();
            return query;
        }

        private async Task ValidateInputs(ViewTestInfoRequestCommand request)
        {
            _ = await _patientRepository.GetAll().Where(x=>x.Id == request.PatientId).FirstOrDefaultAsync() ?? throw new UserFriendlyException("Patient not found");
            _ = await _investigationRequest.GetAll().Where(x=>x.Id == request.InvestigationRequestId).FirstOrDefaultAsync() ??
                throw new UserFriendlyException("Investigation Request not found");
        }
    }
}


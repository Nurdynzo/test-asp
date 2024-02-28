using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Utils;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientAppointments.Command
{
    /// <inheritdoc/>
    public class UpdateAppointmentCommandHandler : IUpdateAppointmentCommandHandler
    {
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IRepository<PatientReferralDocument, long> _referralDocumentRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWork;

        /// <summary>
        /// Constructor for the <see cref="DeleteAppointmentCommandHandler"/>
        /// </summary>
        /// <param name="patientAppointmentRepository"></param>
        /// <param name="objectMapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="referralDocumentRepository"></param>
        public UpdateAppointmentCommandHandler(
            IRepository<PatientAppointment, long> patientAppointmentRepository,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWork,
            IRepository<PatientReferralDocument, long> referralDocumentRepository
            )
        {
            _objectMapper = objectMapper;
            _unitOfWork = unitOfWork;
            _patientAppointmentRepository = patientAppointmentRepository;
            _referralDocumentRepository = referralDocumentRepository;
        }

        /// <inheritdoc/>
        public async Task<CreateOrEditPatientAppointmentDto> Handle(CreateOrEditPatientAppointmentDto request)
        {

            var patientAppointment = await _patientAppointmentRepository.FirstOrDefaultAsync((long)request.Id);
            
            await CheckIfAppointmentCanBeUpdated(request, patientAppointment); 

            if (request.StartTime != patientAppointment.StartTime)
            {
                patientAppointment.StartTime = request.StartTime;
            }

            if (request.Type != patientAppointment.Type)
            {
                patientAppointment.Type = request.Type;

                patientAppointment.Title = request.Title;
            }
            if (request.ReferringClinicId != patientAppointment.ReferringClinicId)
            {
                patientAppointment.ReferringClinicId = request.ReferringClinicId;
            }
            patientAppointment.PatientReferralDocumentId = request.PatientReferralId;
            
            var referralDocument = await _referralDocumentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.PatientReferralId);
            if (referralDocument != null && !string.IsNullOrEmpty(request.DiagnosisSummary))
            {
                referralDocument.ReferringHealthCareProvider = request.TransferredClinic;
                referralDocument.DiagnosisSummary = request.DiagnosisSummary;
                await _referralDocumentRepository.UpdateAsync(referralDocument);
            }

            await _patientAppointmentRepository.UpdateAsync(patientAppointment);
            await _unitOfWork.Current.SaveChangesAsync();

            var result = _objectMapper.Map<CreateOrEditPatientAppointmentDto>(patientAppointment);

            return result;
        }

        private async Task CheckIfAppointmentCanBeUpdated(CreateOrEditPatientAppointmentDto request, PatientAppointment patientAppointment)
        {

            if (request.StartTime == patientAppointment.StartTime && request.Type == patientAppointment.Type)
            {
                throw new UserFriendlyException("No change detected");
            }

            await CommonAppointmentValidation.CheckIfAppointmentCreateOrEditRequestIsValid(_patientAppointmentRepository, _objectMapper.Map<PatientAppointment>(request));
        }
    }
}

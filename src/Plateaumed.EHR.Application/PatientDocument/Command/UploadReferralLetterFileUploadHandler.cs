using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.PatientDocument.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Command;

/// <summary>
/// Upload Referral Letter File handler
/// </summary>
public class UploadReferralLetterFileUploadHandler : IUploadReferralLetterFileUploadHandler
{
    private readonly IUploadService _blobService;
    private readonly IRepository<PatientReferralDocument,long> _patientReferralDocumentRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IUnitOfWorkManager _unitOfWork;
    private readonly IRepository<PatientAppointment,long > _patientAppointmentRepository;

    /// <summary>
    /// Constructor for the UploadReferralLetterFileUploadHandler
    /// </summary>
    /// <param name="blobService"></param>
    /// <param name="patientReferralDocumentRepository"></param>
    /// <param name="objectMapper"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="patientAppointmentRepository"></param>
    public UploadReferralLetterFileUploadHandler(IUploadService blobService, 
        IRepository<PatientReferralDocument, long> patientReferralDocumentRepository,
        IObjectMapper objectMapper, 
        IUnitOfWorkManager unitOfWork,
        IRepository<PatientAppointment, long> patientAppointmentRepository)
    {
        _blobService = blobService;
        _patientReferralDocumentRepository = patientReferralDocumentRepository;
        _objectMapper = objectMapper;
        _unitOfWork = unitOfWork;
        _patientAppointmentRepository = patientAppointmentRepository;
    }

    /// <summary>
    /// Handles the UploadReferralLetterFile
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ReferralLetterUploadRequest> Handle(ReferralLetterUploadRequest request)
    {
        request.FileId = Guid.NewGuid();
        var patientReferralDocument = _objectMapper.Map<PatientReferralDocument>(request);
        await _patientReferralDocumentRepository.InsertAsync(patientReferralDocument);
        await _blobService.UploadFile(request.FileId.GetValueOrDefault(), request.File.OpenReadStream(),GetMetaData(request));
        await _unitOfWork.Current.SaveChangesAsync();
        request.Id = patientReferralDocument.Id;
        await UpdateAppointmentLetter(request);
        return request;
    }

    private async Task UpdateAppointmentLetter(ReferralLetterUploadRequest request)
    {
        if (request.AppointmentId is not null)
        {
            var appointment = await _patientAppointmentRepository.GetAsync(request.AppointmentId.Value);
            if (appointment is null)
            {
                throw new UserFriendlyException("Appointment not found");
            }
            appointment.PatientReferralDocumentId = request.Id;
            await _patientAppointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.Current.SaveChangesAsync();
            
        }
    }

    private static Dictionary<string, string> GetMetaData(ReferralLetterUploadRequest request)
    {
        var metadata = new Dictionary<string, string>()
        {
            {
                "FileType", request.File.ContentType
            },
            {
                "PatientId", request.PatientId.ToString()
            },
            {
                "DocumentType", "Referral Letter"
            }
        };
        return metadata;
    }
}
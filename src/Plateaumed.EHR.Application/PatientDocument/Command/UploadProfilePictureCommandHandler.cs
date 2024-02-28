using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.PatientDocument.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Command;

/// <summary>
/// Upload Profile Picture Command Handler
/// </summary>
public class UploadProfilePictureCommandHandler : IUploadProfilePictureCommandHandler
{
    private readonly IUploadService _uploadService;
    private readonly IRepository<Patient,long> _patientRepository;

    /// <summary>
    /// constructor for the UploadProfilePictureCommandHandler 
    /// </summary>
    /// <param name="uploadService"></param>
    /// <param name="patientRepository"></param>
    public UploadProfilePictureCommandHandler(
        IUploadService uploadService,
        IRepository<Patient, long> patientRepository)
    {
        _uploadService = uploadService;
        _patientRepository = patientRepository;
    }

    /// <summary>
    /// Handler for the patient profile picture upload
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<(Stream, string FileType)> Handle(ProfilePictureUploadRequest request)
    {
        if (request.File != null && !request.File.ContentType.Contains("image"))
        {
            throw new UserFriendlyException("Only image files are allowed");
        }
        var patient = await GetPatient(request);
        var pictureId = Guid.NewGuid();
        var pictureUrl = await _uploadService.UploadPublicAccessFileAsync(pictureId.ToString(), request.File.OpenReadStream(), GetMetaData(request));
        patient.ProfilePictureId = pictureId.ToString();
        patient.PictureUrl = pictureUrl;
        await _patientRepository.UpdateAsync(patient);
        return (request.File.OpenReadStream(), request.File.ContentType);
    }

    /// <summary>
    /// Get patient by Id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task<Patient> GetPatient(ProfilePictureUploadRequest request)
    {
        var patient = await _patientRepository.GetAsync(request.PatientId);
        if (patient == null)
        {
            throw new Exception("Patient not found");
        }

        return patient;
    }

    private static Dictionary<string, string> GetMetaData(ProfilePictureUploadRequest request)
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
                "DocumentType", "Picture Profile"
            }
        };
        return metadata;
    }
}
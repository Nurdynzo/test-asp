using System;
using System.IO;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientDocument.Dto;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

/// <summary>
/// Handler interface for the patient profile picture upload
/// </summary>
public interface IUploadProfilePictureCommandHandler: ITransientDependency
{
    /// <summary>
    /// Handler for the patient profile picture upload
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<(Stream, string FileType)> Handle(ProfilePictureUploadRequest request);
}
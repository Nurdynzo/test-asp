using Microsoft.AspNetCore.Http;

namespace Plateaumed.EHR.PatientDocument.Dto;

/// <summary>
/// Picture upload request
/// </summary>
public class ProfilePictureUploadRequest
{
   /// <summary>
   /// file to be uploaded
   /// </summary>
   public IFormFile File { get; set; }

   /// <summary>
   /// Patient Id
   /// </summary>
   public long PatientId { get; set; }
}
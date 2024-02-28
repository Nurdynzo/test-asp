using Microsoft.AspNetCore.Http;

namespace Plateaumed.EHR.Facilities.Dto;

public class UploadFacilityLogoRequest
{
    public IFormFile File { get; set; }
}
using System;
using System.Threading.Tasks;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Query;

public class GetPublicFileQueryHandler : IGetPublicFileQueryHandler
{
    private readonly IUploadService _uploadService;

    public GetPublicFileQueryHandler(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    public Task<string> Handle(string fileId)
    {
        return _uploadService.GetPublicUrlAsync(fileId);
    }
}
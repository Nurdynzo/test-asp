using System;
using System.IO;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Query;

/// <summary>
/// Get File Query Handler
/// </summary>
public class GetFileQueryHandler : IGetFileQueryHandler
{  
    private readonly IUploadService _uploadService;

    /// <summary>
    /// constructor for the GetFileQueryHandler
    /// </summary>
    /// <param name="uploadService"></param>
    public GetFileQueryHandler(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    /// <summary>
    /// Download file by Id
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    public Task<(Stream, string FileType)> Handle(Guid fileId)
    {
        return _uploadService.DownloadAsync(fileId);
    }
}
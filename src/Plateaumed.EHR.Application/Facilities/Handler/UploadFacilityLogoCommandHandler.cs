using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dto;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.Facilities.Handler
{
    /// <inheritdoc />
    public class UploadFacilityLogoCommandHandler : IUploadFacilityLogoCommandHandler
    {
        private readonly IUploadService _uploadService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uploadService"></param>
        public UploadFacilityLogoCommandHandler(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        /// <inheritdoc />
        public async Task<Guid> Handle(UploadFacilityLogoRequest request)
        {
            var logoId = Guid.NewGuid();
            await _uploadService.UploadFile(logoId, request.File.OpenReadStream(), GetMetaData(request));
            return logoId;
        }

        private IDictionary<string, string> GetMetaData(UploadFacilityLogoRequest request)
        {
            return new Dictionary<string, string>
            {
                { "FileName", request.File.FileName },
                { "FileType", request.File.ContentType },
                { "DocumentType", "Facility Logo" }
            };
        }
    }
}
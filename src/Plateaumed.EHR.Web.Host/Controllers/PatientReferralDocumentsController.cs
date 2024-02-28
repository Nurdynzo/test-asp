using System;
using System.IO;
using System.Linq;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Plateaumed.EHR.Storage;

namespace Plateaumed.EHR.Web.Controllers
{
    [Authorize]
    public class PatientReferralDocumentsController : EHRControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxReferralDocumentLength = 5242880; //5MB
        private const string MaxReferralDocumentLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] ReferralDocumentAllowedFileTypes = { "jpeg", "jpg", "png" };

        public PatientReferralDocumentsController(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        public FileUploadCacheOutput UploadReferralDocumentFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count == 0)
                {
                    throw new UserFriendlyException(L("NoFileFoundError"));
                }

                var file = Request.Form.Files.First();
                if (file.Length > MaxReferralDocumentLength)
                {
                    throw new UserFriendlyException(L("Warn_File_SizeLimit", MaxReferralDocumentLengthUserFriendlyValue));
                }

                var fileType = Path.GetExtension(file.FileName).Substring(1);
                if (ReferralDocumentAllowedFileTypes != null && ReferralDocumentAllowedFileTypes.Length > 0 && !ReferralDocumentAllowedFileTypes.Contains(fileType))
                {
                    throw new UserFriendlyException(L("FileNotInAllowedFileTypes", ReferralDocumentAllowedFileTypes));
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var fileToken = Guid.NewGuid().ToString("N");
                _tempFileCacheManager.SetFile(fileToken, new TempFileInfo(file.FileName, fileType, fileBytes));

                return new FileUploadCacheOutput(fileToken);
            }
            catch (UserFriendlyException ex)
            {
                return new FileUploadCacheOutput(new ErrorInfo(ex.Message));
            }
        }

        public string[] GetReferralDocumentFileAllowedTypes()
        {
            return ReferralDocumentAllowedFileTypes;
        }

    }
}
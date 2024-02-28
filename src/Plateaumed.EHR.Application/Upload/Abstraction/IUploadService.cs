using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Upload.Abstraction;

/// <summary>
/// Upload Service interface
/// </summary>
public interface IUploadService : ITransientDependency
{
    /// <summary>
    /// Upload a file
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stream"></param>
    /// <param name="tags"></param>
    /// <returns></returns>
    Task UploadFile(Guid id, Stream stream, IDictionary<string, string> tags = null);

    /// <summary>
    /// Download file with unique id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<(Stream File, string ContentType)> DownloadAsync(Guid id);

    /// <summary>
    /// Update existing file
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stream"></param>
    Task UpdateFile(Guid id, Stream stream);

    /// <summary>
    /// Get public url for public access
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<string> GetPublicUrlAsync(string id);

    /// <summary>
    /// Upload public access file in public container
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stream"></param>
    /// <param name="tags"></param>
    /// <returns></returns>
    Task<string> UploadPublicAccessFileAsync(string id, Stream stream, IDictionary<string, string> tags = null);


}
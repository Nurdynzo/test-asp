using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Runtime.Session;
using Abp.UI;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Plateaumed.EHR.Configuration;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.Upload.Services;

/// <inheritdoc />
public class UploadService : IUploadService
{
    private  BlobContainerClient _containerClient;
    private readonly SettingManager _settingManager;
    private readonly string _containerName;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfigurationRoot _configuration;
   
    private readonly string _connectionString;
    private readonly string _publicContainerName;

    /// <summary>
    /// Constructor for upload service
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="abpSession"></param>
    /// <param name="settingManager"></param>
    public UploadService(IWebHostEnvironment configuration, IAbpSession abpSession, SettingManager settingManager)
    {
        _settingManager = settingManager;
        _configuration = configuration.GetAppConfiguration();
        _connectionString = _configuration["FileStorage:AzureBlobStorage:ConnectionString"];
        _containerName = $"{_configuration["FileStorage:AzureBlobStorage:ContainerName"]}{abpSession.TenantId}";
        _publicContainerName =$"public{_containerName}";
        _blobServiceClient = new BlobServiceClient(_connectionString);
        
    }
    /// <inheritdoc />
    public async Task UploadFile(Guid id, Stream stream, IDictionary<string, string> tags = null)
    {
        if (await IsCloudProvider())
        {
            await ProcessCloudUpload(id, stream, tags);
        }
        else
        {
            throw new UserFriendlyException("Local File Upload is not yet supported");
            //TODO Victor: Implement local upload
        }
       
    }
    
    /// <inheritdoc />
    public async Task UpdateFile(Guid id, Stream stream)
    {
        if (await IsCloudProvider())
        {
            await ProcessFileUpdate(id, stream);
        }
        else
        {
            //TODO Victor: Implement local upload
            throw new UserFriendlyException("Local File Upload is not yet supported");
           
        }
    }

    /// <summary>
    /// Update Uploaded file in the cloud
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stream"></param>
    /// <exception cref="UserFriendlyException"></exception>
    private async Task ProcessFileUpdate(Guid id, Stream stream)
    {
        _containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = _containerClient.GetBlobClient(id.ToString());
        if (!await blobClient.ExistsAsync())
        {
            throw new UserFriendlyException("File does not exist in the cloud");
        }

        await blobClient.UploadAsync(stream, overwrite: true);
    }


    /// <inheritdoc />
    public async Task<(Stream File, string ContentType)> DownloadAsync(Guid id)
    {
        if (await IsCloudProvider())
        {
            return await DownloadCloudFile(id); 
        }
        throw new UserFriendlyException("Local File Download is not yet supported");
        //TODO Victor: Implement local download


    }
    
    /// <inheritdoc />
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<string> UploadPublicAccessFileAsync(string id, Stream stream, IDictionary<string, string> tags = null)
    {
        if (await IsCloudProvider())
        {
            BlobContainerClient container = new BlobContainerClient(_connectionString, _publicContainerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);
            BlobClient blob = container.GetBlobClient(id);
            await blob.UploadAsync(stream,new BlobUploadOptions
            {
                Tags = tags
            });
            return blob.Uri.AbsoluteUri;
           
        }
        throw new UserFriendlyException("Local File Upload is not yet supported");
    }
    
    /// <inheritdoc />
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<string> GetPublicUrlAsync(string id)
    {
        if (await IsCloudProvider())
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(_publicContainerName);
            var blobClient = _containerClient.GetBlobClient(id);
            return blobClient.Uri.AbsoluteUri;
        }
        throw new UserFriendlyException("Local File Upload is not yet supported");
    }

   


    #region Private Methods
    
    /// <summary>
    /// Download file from the cloud
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task<(Stream File, string ContentType)> DownloadCloudFile(Guid id)
    {
        _containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = _containerClient.GetBlobClient(id.ToString());
        var response = await blobClient.OpenReadAsync();
        var properties = await blobClient.GetPropertiesAsync();
        return (response, properties.Value.ContentType);
    }
    
    /// <summary>
    /// Upload file to the cloud
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stream"></param>
    /// <param name="tags"></param>
    private async Task ProcessCloudUpload(Guid id, Stream stream, IDictionary<string, string> tags = null)
    {
        _containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await _containerClient.CreateIfNotExistsAsync();
        var uniqueId = id.ToString();
        var blobClient = _containerClient.GetBlobClient(uniqueId);
        await blobClient.UploadAsync(stream, new BlobUploadOptions
        {
            Tags = tags
        });
    }

    /// <summary>
    /// Check if cloud provider is enabled
    /// </summary>
    /// <returns></returns>
    private async Task<bool> IsCloudProvider() =>
        await _settingManager.GetSettingValueAsync<bool>(AppSettings.HostManagement.IsCloudUpload);
    
   
    #endregion
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Dependency;
using Plateaumed.EHR.Utility.HttpClient;

namespace Plateaumed.EHR.Utility;

public interface IHttpResourceService : ITransientDependency
{ 
    Task<Tuple<T, HttpClientResponse>> GetAsync<T>(string requestUri);

    Task<Tuple<T, HttpClientResponse>> GetAsync<T>(string requestUri, string bearertoken, Dictionary<string, string> headers = null, bool reloadkey = true);  
    
    Task<Tuple<TResponseBody, HttpClientResponse>> PostAsyncAndReturnErrorsAsString<TResponseBody, KRequestBody>(
        string requestUri, KRequestBody resource, string bearertoken, string apiusername = "",
        string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true); 

    Task<Tuple<TReturnEntity, HttpClientResponse>> PostAsync<TReturnEntity, KBodyEntity>(string requestUri, KBodyEntity resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true);
 
    Task<Tuple<XReturnEntity, YReturnEntity, HttpClientResponse>> PostWithMultipleResultTypeAsync<XReturnEntity, YReturnEntity, KBodyEntity>(string requestUri, KBodyEntity resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true);
    Task<Tuple<TReturnEntity, HttpClientResponse>> PutAsync<TReturnEntity, KBodyEntity>(string requestUri, KBodyEntity resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true);
    Task<Tuple<TReturnEntity, HttpClientResponse>> DeleteAsync<TReturnEntity, KBodyEntity>(string requestUri, KBodyEntity resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true);
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Plateaumed.EHR.Utility.HttpClient;
using System.Text.Json; 


namespace Plateaumed.EHR.Utility;

public class HttpResourceService : IHttpResourceService
{
    public ILog _logger;

    public HttpResourceService(ILog logger)
    {
        _logger = logger;
    }

    public async Task<Tuple<T, HttpClientResponse>> PostAsync<T, K>(string requestUri, K resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true)
    { 
        _logger.Write($"Initiating call to url=>{requestUri} Request=>{JsonSerializer.Serialize(resource)}");

        var response = await HttpRequestFactory.Post(requestUri, resource, bearertoken, apiusername, apipassword, headers);

        _logger.Write($"Initiated call to url=>{requestUri} Response=>{JsonSerializer.Serialize(response)}");
        _logger.Write($"Initiating call to {requestUri} returned => {response.StatusCode} ");

        if (response.IsSuccessStatusCode)
        {
            T records = await response.ContentAsTypeAsync<T>();
            _logger.Write($"Success third party result => {JsonSerializer.Serialize(records)}");
            var responseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"Data Fetched Succesfully", isSuccess = true };
            return new Tuple<T, HttpClientResponse>(records, responseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var responseMessage = new HttpClientResponse { Code = StatusCodes.Status401Unauthorized.ToString(), Message = $" consider registering and reauthorisation." };
            _logger.Write($"Unauthorized third party response {responseMessage}");
            return new Tuple<T, HttpClientResponse>(default(T), responseMessage);
        }
        //Specially for SSO user creation which return a 302 response code if user exists already
        else if (response.StatusCode == System.Net.HttpStatusCode.Found)
        {
            var ssoId = await response.ContentAsTypeAsync<string>();
            var responseMessage = new HttpClientResponse { Code = ((int)response.StatusCode).ToString(), Message = $"{ssoId}" };
            _logger.Write($"Not found response {responseMessage}");
            return new Tuple<T, HttpClientResponse>(default(T), responseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        { 
            var badrequest = await response.ContentAsStringAsync();
            _logger.Write($"HttpError {response.StatusCode} : {badrequest}");
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status400BadRequest.ToString(), Message = badrequest };
            return new Tuple<T, HttpClientResponse>(default(T), ResponseMessage);
        }
        else
        {
            string error = await response.ContentAsStringAsync();
            _logger.Write($"{response} : {error}"); 
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"{error}" };
            return new Tuple<T, HttpClientResponse>(default(T), ResponseMessage);
        }
    }

    public async Task<Tuple<TResponseBody, HttpClientResponse>> PostAsyncAndReturnErrorsAsString<TResponseBody, KRequestBody>(string requestUri, KRequestBody resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true)
    { 
        _logger.Write($"Initiating call to url=>{requestUri} Request=>{JsonSerializer.Serialize(resource)}");

        var response = await HttpRequestFactory.Post(requestUri, resource, bearertoken, apiusername, apipassword, headers);

        _logger.Write($"Initiated call to url=>{requestUri} Response=>{JsonSerializer.Serialize(response)}");
        _logger.Write($"Initiating call to {requestUri} returned => {response.StatusCode} ");

        if (response.IsSuccessStatusCode)
        {
            TResponseBody records = await response.ContentAsTypeAsync<TResponseBody>();
            _logger.Write($"Success third party result => {JsonSerializer.Serialize(records)}");
            var responseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"Data Fetched Succesfully", isSuccess = true };
            return new Tuple<TResponseBody, HttpClientResponse>(records, responseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var responseMessage = new HttpClientResponse { Code = StatusCodes.Status401Unauthorized.ToString(), Message = $" consider registering and reauthorisation." };
            _logger.Write($"Unauthorized third party response {responseMessage}");
            return new Tuple<TResponseBody, HttpClientResponse>(default(TResponseBody), responseMessage);
        }
        //Specially for SSO user creation which return a 302 response code if user exists already
        else if (response.StatusCode == System.Net.HttpStatusCode.Found)
        {
            var ssoId = await response.ContentAsTypeAsync<string>();
            var responseMessage = new HttpClientResponse { Code = ((int)response.StatusCode).ToString(), Message = $"{ssoId}" };
            _logger.Write($"Not found response {responseMessage}");
            return new Tuple<TResponseBody, HttpClientResponse>(default(TResponseBody), responseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            var badrequest = await response.ContentAsStringAsync();
            _logger.Write($"HttpError {response.StatusCode} : {badrequest}");
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status400BadRequest.ToString(), Message = badrequest };
            return new Tuple<TResponseBody, HttpClientResponse>(default(TResponseBody), ResponseMessage);
        }
        else
        {
            string error = await response.ContentAsStringAsync();
            _logger.Write($"{response} : {error}");
            var ResponseMessage = new HttpClientResponse { Code = ((int)response.StatusCode).ToString(), Message = $"{error}" };
            return new Tuple<TResponseBody, HttpClientResponse>(default(TResponseBody), ResponseMessage);
        }
    }
 

    public async Task<Tuple<T, HttpClientResponse>> GetAsync<T>(string requestUri, string bearertoken, Dictionary<string, string> headers = null, bool reloadkey = true)
    {
        _logger.Write($"Initiating call to {requestUri}");
        _logger.Write($"Bearer Token {bearertoken}");
        var response = await HttpRequestFactory.Get(requestUri, bearertoken, headers);

#if DEBUG
        Console.WriteLine($"Status: {response.StatusCode}");
#endif
        _logger.Write($"Initiating call to {requestUri} returned => {response.StatusCode}   ");
        if (response.IsSuccessStatusCode)
        {
            T records = await response.ContentAsTypeAsync<T>();
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"Data Fetched Succesfully", isSuccess = true };
            return new Tuple<T, HttpClientResponse>(records, ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status401Unauthorized.ToString(), Message = $" consider registering and reauthorisation." };
            return new Tuple<T, HttpClientResponse>(default(T), ResponseMessage);
        }
        else
        { 
            string error = await response.ContentAsJsonAsync();
            _logger.Write($"{response} : {error}");
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"{error}" };
            return new Tuple<T, HttpClientResponse>(default(T), ResponseMessage);
        }
    }

    public async Task<Tuple<T, HttpClientResponse>> GetAsync<T>(string requestUri)
    {
        _logger.Write($"Initiating call to {requestUri}");
        var response = await HttpRequestFactory.Get(requestUri);

#if DEBUG
        Console.WriteLine($"Status: {response.StatusCode}");
#endif
        _logger.Write($"Initiating call to {requestUri} returned => {response.StatusCode} ");
        if (response.IsSuccessStatusCode)
        {
            T records = await response.ContentAsTypeAsync<T>();
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"Data Fetched Succesfully", isSuccess = true };
            return new Tuple<T, HttpClientResponse>(records, ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status401Unauthorized.ToString(), Message = $" consider registering and reauthorisation." };
            return new Tuple<T, HttpClientResponse>(default(T), ResponseMessage);
        }
        else
        { 
            string error = await response.ContentAsJsonAsync();
            _logger.Write($"{response} : {error}");
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status400BadRequest.ToString(), Message = $"{error}" };
            return new Tuple<T, HttpClientResponse>(default(T), ResponseMessage);
        }
    } 

    public async Task<Tuple<XReturnEntity, YReturnEntity, HttpClientResponse>> PostWithMultipleResultTypeAsync<XReturnEntity, YReturnEntity, KBodyEntity>(string requestUri, KBodyEntity resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true)
    {
        _logger.Write($"Initiating call to {requestUri}");
        var response = await HttpRequestFactory.Post(requestUri, resource, bearertoken, apiusername, apipassword, headers);
#if DEBUG
        Console.WriteLine($"Status: {response.StatusCode}");
#endif
        _logger.Write($"Initiating call to {requestUri} returned => {response.StatusCode} ");
        if (response.IsSuccessStatusCode)
        {
            XReturnEntity recordx;
            try
            {
                recordx = await response.ContentAsTypeAsync<XReturnEntity>();
            }
            catch (Exception) { recordx = default(XReturnEntity); }

            YReturnEntity recordy;
            try
            {
                recordy = await response.ContentAsTypeAsync<YReturnEntity>();
            }
            catch (Exception) { recordy = default(YReturnEntity); }

            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"Data Fetched Succesfully", isSuccess = true };
            return new Tuple<XReturnEntity, YReturnEntity, HttpClientResponse>(recordx, recordy, ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status401Unauthorized.ToString(), Message = $" consider registering and reauthorisation." };
            return new Tuple<XReturnEntity, YReturnEntity, HttpClientResponse>(default(XReturnEntity), default(YReturnEntity), ResponseMessage);
        }
        //Specially for SSO user creation which return a 302 response code if user exists already
        else if (response.StatusCode == System.Net.HttpStatusCode.Found)
        {
            var sso_id = await response.ContentAsTypeAsync<string>();
            var ResponseMessage = new HttpClientResponse { Code = ((int)response.StatusCode).ToString(), Message = $"{sso_id}" };
            return new Tuple<XReturnEntity, YReturnEntity, HttpClientResponse>(default(XReturnEntity), default(YReturnEntity), ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        { 
            var badrequest = await response.ContentAsStringAsync();
            _logger.Write($"HttpError {response.StatusCode} : {badrequest}");
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status400BadRequest.ToString(), Message = badrequest };
            return new Tuple<XReturnEntity, YReturnEntity, HttpClientResponse>(default(XReturnEntity), default(YReturnEntity), ResponseMessage);
        }
        else
        { 
            string error = await response.ContentAsStringAsync();
            _logger.Write($"{response} : {error}");
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"{error}" };
            return new Tuple<XReturnEntity, YReturnEntity, HttpClientResponse>(default(XReturnEntity), default(YReturnEntity), ResponseMessage);
        }
    }

    public async Task<Tuple<TReturnEntity, HttpClientResponse>> PutAsync<TReturnEntity, KBodyEntity>(string requestUri, KBodyEntity resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true)
    {
        _logger.Write($"Initiating call to {requestUri}");
        var response = await HttpRequestFactory.Put(requestUri, resource, bearertoken, apiusername, apipassword, headers);
#if DEBUG
        Console.WriteLine($"Status: {response.StatusCode}");
#endif
        _logger.Write($"Initiating call to {requestUri} returned => {response.StatusCode} ");
        if (response.IsSuccessStatusCode)
        {
            TReturnEntity records = await response.ContentAsTypeAsync<TReturnEntity>();
            _logger.Write($"success thirdparty result => {JsonSerializer.Serialize(records)}");
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"Data Fetched Succesfully", isSuccess = true };
            return new Tuple<TReturnEntity, HttpClientResponse>(records, ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status401Unauthorized.ToString(), Message = $" consider registering and reauthorisation." };
            _logger.Write("unauthorized thirdparty");
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
        //Specially for SSO user creation which return a 302 response code if user exists already
        else if (response.StatusCode == System.Net.HttpStatusCode.Found)
        {
            var sso_id = await response.ContentAsTypeAsync<string>();
            var ResponseMessage = new HttpClientResponse { Code = ((int)response.StatusCode).ToString(), Message = $"{sso_id}" };
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        { 
            var badrequest = await response.ContentAsStringAsync();
            _logger.Write($"HttpError {response.StatusCode} : {badrequest}");
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status400BadRequest.ToString(), Message = badrequest };
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
        else
        { 
            string error = await response.ContentAsStringAsync();
            _logger.Write($"{response} : {error}");
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"{error}" };
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
    }
        
        
    public async Task<Tuple<TReturnEntity, HttpClientResponse>> DeleteAsync<TReturnEntity, KBodyEntity>(string requestUri, KBodyEntity resource, string bearertoken, string apiusername = "", string apipassword = "", Dictionary<string, string> headers = null, bool reloadkey = true)
    {
        _logger.Write($"Initiating call to {requestUri}");
        var response = await HttpRequestFactory.DeleteAsync(requestUri, resource, bearertoken, apiusername, apipassword, headers);
#if DEBUG
        Console.WriteLine($"Status: {response.StatusCode}");
#endif
        _logger.Write($"Initiating call to {requestUri} returned => {response.StatusCode} ");
        if (response.IsSuccessStatusCode)
        {
            TReturnEntity records = await response.ContentAsTypeAsync<TReturnEntity>();
            _logger.Write($"success thirdparty result => {JsonSerializer.Serialize(records)}");
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"Data Fetched Succesfully", isSuccess = true };
            return new Tuple<TReturnEntity, HttpClientResponse>(records, ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status401Unauthorized.ToString(), Message = $" consider registering and reauthorisation." };
            _logger.Write("unauthorized thirdparty");
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
        //Specially for SSO user creation which return a 302 response code if user exists already
        else if (response.StatusCode == System.Net.HttpStatusCode.Found)
        {
            var sso_id = await response.ContentAsTypeAsync<string>();
            var ResponseMessage = new HttpClientResponse { Code = ((int)response.StatusCode).ToString(), Message = $"{sso_id}" };
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        { 
            var badrequest = await response.ContentAsStringAsync();
            _logger.Write($"HttpError {response.StatusCode} : {badrequest}");
            var ResponseMessage = new HttpClientResponse { Code = StatusCodes.Status400BadRequest.ToString(), Message = badrequest };
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
        else
        { 
            string error = await response.ContentAsStringAsync();
            _logger.Write($"{response} : {error}");
            var ResponseMessage = new HttpClientResponse { Code = response.StatusCode.ToString(), Message = $"{error}" };
            return new Tuple<TReturnEntity, HttpClientResponse>(default(TReturnEntity), ResponseMessage);
        }
    }
    
}
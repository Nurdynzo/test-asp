using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Utility.HttpClient
{
    public static class HttpRequestFactory
    {
    
        public static async Task<HttpResponseMessage> Get(string requestUri)
            => await Get(requestUri, "");

        public static async Task<HttpResponseMessage> Get(string requestUri, string bearerToken, Dictionary<string, string> headers = null)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Get)
                .AddRequestUri(requestUri)
                .AddBearerToken(bearerToken)
                .AddHeaders(headers);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Get(string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Get)
                .AddRequestUri(requestUri)
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(string requestUri, object value, Dictionary<string, string> headers = null)
            => await Post(requestUri, value, "");

        public static async Task<HttpResponseMessage> Post(
            string requestUri, object value, string bearerToken, string username = "", string password = "", Dictionary<string, string> headers = null)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBasicAuthorisationHeader(username, password)
                .AddBearerToken(bearerToken)
                .AddHeaders(headers);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(
            string requestUri, Dictionary<string, string> value, string bearerToken, string username = "", string password = "", Dictionary<string, string> headers = null)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(requestUri)
                .AddContent(value)
                .AddBasicAuthorisationHeader(username, password)
                .AddBearerToken(bearerToken)
                .AddHeaders(headers);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(string requestUri, object value)
            => await Put(requestUri, value, "");

        public static async Task<HttpResponseMessage> Put(
            string requestUri, object value, string bearerToken, string username = "", string password = "", Dictionary<string, string> headers = null)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Put)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBearerToken(bearerToken)
                .AddBasicAuthorisationHeader(username, password)
                .AddHeaders(headers); ;

            return await builder.SendAsync();
        }
        
        public static async Task<HttpResponseMessage> Delete(
            string requestUri, object value, string bearerToken, string username = "", string password = "", Dictionary<string, string> headers = null)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Delete)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBearerToken(bearerToken)
                .AddBasicAuthorisationHeader(username, password)
                .AddHeaders(headers); ;

            return await builder.SendAsync();
        }
        
        public static async Task<HttpResponseMessage> DeleteAsync(
            string requestUri, object value, string bearerToken, string username = "", string password = "", Dictionary<string, string> headers = null)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Delete)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBasicAuthorisationHeader(username, password)
                .AddBearerToken(bearerToken)
                .AddHeaders(headers);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(string requestUri, object value)
            => await Patch(requestUri, value, "");

        public static async Task<HttpResponseMessage> Patch(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(new HttpMethod("PATCH"))
                .AddRequestUri(requestUri)
                .AddContent(new PatchContent(value))
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(string requestUri)
            => await Delete(requestUri, "");

        public static async Task<HttpResponseMessage> Delete(
            string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Delete)
                .AddRequestUri(requestUri)
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName)
            => await PostFile(requestUri, filePath, apiParamName, "");

        public static async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(requestUri)
                .AddContent(new FileContent(filePath, apiParamName))
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }
    
    }
}
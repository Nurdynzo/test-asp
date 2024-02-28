using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Utility.HttpClient
{
    public class HttpRequestBuilder
    {
        private HttpMethod method = null;
        private string requestUri = "";
        private HttpContent content = null;
        private string bearerToken = "";
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(0, 20, 00);
        private Dictionary<string, string> headers = null;

        private bool useBasicAuthorisation = false;
        private string base64AuthorisationString;

        public HttpRequestBuilder()
        {
        }

        public HttpRequestBuilder AddHeaders(Dictionary<string, string> headers)
        {
            this.headers = headers;
            return this;
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddContent(Dictionary<string, string> formbodyParameters)
        {
            //var formContent = new MultipartFormDataContent();

            HttpContent DictionaryItems = new FormUrlEncodedContent(formbodyParameters);
            //formContent.Add(DictionaryItems);
            this.content = DictionaryItems;

            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddBasicAuthorisationHeader(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                this.useBasicAuthorisation = true;
                this.base64AuthorisationString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            }

            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = this.method,
                RequestUri = new Uri(this.requestUri)
            };

            if (this.content != null)
                request.Content = this.content;

            if (!string.IsNullOrEmpty(this.bearerToken))
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", this.bearerToken);

            if (useBasicAuthorisation)
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", this.base64AuthorisationString);

            if (this.headers != null)
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(this.acceptHeader))
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(this.acceptHeader));

            // Setup client
            var client = new System.Net.Http.HttpClient();

            client.Timeout = this.timeout;
            
            return await client.SendAsync(request); 
        }
    }
}
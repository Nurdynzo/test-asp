using System;
using System.Net.Http;
using System.Net.Mime;
// using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Utility.HttpClient
{
    public static class HttpResponseExtensions
    {
        public async static Task<T> ContentAsTypeAsync<T>(this HttpResponseMessage response)
        {
            if (response.Content.Headers.ContentType.MediaType != null && response.Content.Headers.ContentType.MediaType != "application/json")
            {
                return default(T);
            }
            var data = await response.Content.ReadAsStringAsync(); 
            var result = string.IsNullOrEmpty(data) ?
                default(T) :
                JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async static Task<string> ContentAsJsonAsync(this HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async static Task<string> ContentAsStringAsync(this HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }
    }
}
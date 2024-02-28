using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Plateaumed.EHR.Utility.HttpClient
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value)
            : base(JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(object value, string mediaType)
            : base(JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), Encoding.UTF8, mediaType)
        {
        }
    }
}
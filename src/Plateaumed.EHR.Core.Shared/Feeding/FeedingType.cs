using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Plateaumed.EHR.Feeding
{
    
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FeedingType
    {
        Content,
        Mode,
    }
}
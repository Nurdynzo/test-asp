using System.Text.Json;
using Plateaumed.EHR.Misc.Json;

namespace Plateaumed.EHR.Misc.Payload
{

    public class RegionPayload
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
    }

    public class RegionsPayload
    {
        public string CountryName { get; set; }
        public string CountryShortCode { get; set; }
        public RegionPayload[] Regions { get; set; }
    }

    public static class  RegionHelper
    {
        public static RegionsPayload[] GetDeserializedRegionData()
        {
            var settings = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<RegionsPayload[]>(RegionsJson.jsonData, settings);
        }
    }
}
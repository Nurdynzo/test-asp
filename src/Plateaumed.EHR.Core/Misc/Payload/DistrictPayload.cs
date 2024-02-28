using Plateaumed.EHR.Misc.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Misc.Payload
{
    public class DistrictPayload
    {
        public string Name { get; set; }
    }

    public class DistrictsPayload
    {
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public DistrictPayload[] Districts { get; set; }
    }

    public static class DistrictsHelper
    {
        public static DistrictsPayload[] GetDeserializedDistrictData()
        {
            var settings = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<DistrictsPayload[]>(DistrictsJson.jsonData, settings);
        }
    }
}

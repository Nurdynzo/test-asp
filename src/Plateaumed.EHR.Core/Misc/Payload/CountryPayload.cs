using Plateaumed.EHR.Misc.Json;
using System.Text.Json;

namespace Plateaumed.EHR.Misc.Payload
{
    public class CountryPayload
    {
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
    }

    
    public static class CountryHelper
    {
        public static CountryPayload[] GetDeserializedCountriesData()
        {

            var settings = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<CountryPayload[]>(CountriesJson.jsonData, settings);
        }
    }
}


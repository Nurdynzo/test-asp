using System.Text.Json;
using Plateaumed.EHR.Misc.Json;

namespace Plateaumed.EHR.Misc.Payload
{
    public class OccupationCategoriesPayload
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public static class OccupationCategoriesHelper
    {
        public static OccupationCategoriesPayload[] GetDeserializedOccupationCategoriesData() => JsonSerializer.Deserialize<OccupationCategoriesPayload[]>(OccupationCategoriesJson.jsonData);
    }
}

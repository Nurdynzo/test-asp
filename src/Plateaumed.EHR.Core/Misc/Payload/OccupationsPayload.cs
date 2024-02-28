using System.Text.Json;
using Plateaumed.EHR.Misc.Json;

namespace Plateaumed.EHR.Misc.Payload
{
    public class OccupationsPayload
    {
        public int id { get; set; }
        public string name { get; set; }

        public int categoryId { get; set; }
    }

    public static class OccupationsHelper 
    {
        public static OccupationsPayload[] GetDeserializedOccupationsData() => JsonSerializer.Deserialize<OccupationsPayload[]>(OccupationsJson.jsonData);
    }
}

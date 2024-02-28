using System.Collections.Generic;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientConsultingRooms
    {
        public List<string> rooms { get; }

        public GetPatientConsultingRooms(List<string> rooms)
        {
            this.rooms = rooms;
        }


    }
}
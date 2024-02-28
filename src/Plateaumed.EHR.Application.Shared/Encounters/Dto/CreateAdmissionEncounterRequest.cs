using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Encounters.Dto;

public class CreateAdmissionEncounterRequest
{
    public long PatientId { get; set; }
    public long FacilityId { get; set; }
    public long? UnitId { get; set; }
    public long? WardId { get; set; }
    public long? WardBedId { set; get; }
    public long? AttendingPhysicianId { get; set; }
    public long? AdmissionId { get; set; }
    public long? AppointmentId { get; set; }
    public ServiceCentreType ServiceCentre { get; set; }
    public EncounterStatusType Status { get; set; }
}

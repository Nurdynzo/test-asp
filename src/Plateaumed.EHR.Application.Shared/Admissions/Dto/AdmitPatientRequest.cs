#nullable enable
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Admissions.Dto;

public class AdmitPatientRequest
{
    public long PatientId { get; set; }
    public long FacilityId { get; set; }
    public long? UnitId { get; set; }
    public OrganizationUnitDto? Unit { get; set; }
    public long? WardId { get; set; }
    public WardDto? Ward { get; set; }
    public long? WardBedId { set; get; }
    public WardBedDto? WardBed { get; set; }
    public long? AttendingPhysicianId { get; set; }
    public StaffMemberDto? AttendingPhysician { get; set; }
    public ServiceCentreType ServiceCentre { get; set; }
    public long? EncounterId { get; set; }
}

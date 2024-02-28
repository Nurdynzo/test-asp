using System;

namespace Plateaumed.EHR.Vaccines.Dto;

public class VaccinationResponseDto
{ 
    public long PatientId { get; set; }
    public long VaccineId { get; set; }
    public GetVaccineResponse Vaccine { get; set; }
    public long VaccineScheduleId { get; set; }
    public VaccineScheduleDto VaccineSchedule { get; set; }
    public bool IsAdministered { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? DateAdministered { get; set; }
    public bool HasComplication { get; set; }
    public string VaccineBrand { get; set; }
    public string VaccineBatchNo { get; set; }
    public string Note { get; set; }
    public long? ProcedureId { get; set; }
    public string ProcedureEntryType { get; set; }
    
    public DateTime CreationTime { get; set; }
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }
    public long Id { get; set; }
}

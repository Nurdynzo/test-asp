using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos;

public class GetPatientWardRoundAndClinicNotesQueryRequest:PagedAndSortedResultRequestDto
{
    public long PatientId { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos;

public class GetInpatientLandingListRequest : PagedAndSortedResultRequestDto
{
    [Required]
    public long WardId { get; set; }
}
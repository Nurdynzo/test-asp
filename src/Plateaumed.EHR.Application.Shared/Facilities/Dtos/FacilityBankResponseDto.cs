using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos;

public class FacilityBankResponseDto :  EntityDto<long?>
{
    public string BankName { get; set; }
    public string BankAccountHolder { get; set; }
    public string BankAccountNumber { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public long FacilityId { get; set; }
}
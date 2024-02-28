using System;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Staff.Dtos;

public class GetStaffMembersResponse
{
    public long Id { get; set; }

    public long StaffMemberId { get; set; }

    public TitleType? Title { get; set; }

    public string Name { get; set; }

    public string MiddleName { get; set; }

    public string Surname { get; set; }

    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }
    
    public string JobTitle { get; set; }

    public string JobLevel { get; set; }

    public string StaffCode { get; set; }

    public string Department { get; set; }

    public string Unit { get; set; }
    public long? UnitId { get; set; }

    public DateTime? ContractStartDate { get; set; }

    public DateTime? ContractEndDate { get; set; }

    public bool IsActive { get; set; }
}

public class GetStaffMembersSimpleResponseDto
{
    public long Id { get; set; }
    
    public long? StaffMemberId { get; set; }

    public TitleType? Title { get; set; }

    public string Name { get; set; }

    public string MiddleName { get; set; }

    public string Surname { get; set; } 
    
    public string StaffCode { get; set; }
}
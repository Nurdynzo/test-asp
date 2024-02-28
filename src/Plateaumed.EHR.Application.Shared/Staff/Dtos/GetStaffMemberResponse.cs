using System;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users.Dto;

namespace Plateaumed.EHR.Staff.Dtos;

public class GetStaffMemberResponse
{
    public UserEditDto User { get; set; }

    public long StaffMemberId { get; set; }

    public string PhoneCode { get; set; }

    public string StaffCode { get; set; }

    public DateTime? ContractStartDate { get; set; }

    public DateTime? ContractEndDate { get; set; }

    public string AdminRole { get; set; }

    public List<JobDto> Jobs { get; set; }
}
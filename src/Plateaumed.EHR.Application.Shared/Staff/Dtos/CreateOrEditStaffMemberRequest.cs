using System;
using Plateaumed.EHR.Authorization.Users.Dto;

namespace Plateaumed.EHR.Staff.Dtos;

public class CreateOrEditStaffMemberRequest
{
    public string StaffCode { get; set; }

    public DateTime? ContractStartDate { get; set; }

    public DateTime? ContractEndDate { get; set; }

    public string AdminRole { get; set; }

    public UserEditDto User { get; set; }
   
    public JobDto Job { get; set; }
}
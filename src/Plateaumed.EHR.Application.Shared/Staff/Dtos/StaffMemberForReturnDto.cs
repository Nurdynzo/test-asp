using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Staff.Dtos;

public class StaffMemberForReturnDto
{
    public long UserId { get; set; }
    public long StaffMemberId { get; set; } 
    public string StaffCode { get; set; } 
    public TitleType? Title { get; set; } 
    public string Name { get; set; } 
    public string MiddleName { get; set; }  
    public string Surname { get; set; } 
    public string UserName { get; set; } 
    public GenderType? Gender { get; set; }
}
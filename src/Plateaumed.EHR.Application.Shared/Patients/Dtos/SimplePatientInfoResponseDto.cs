using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Patients.Dtos;

public class SimplePatientInfoResponseDto
{
    public long Id { get; set; }
    
    public GenderType GenderType { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public TitleType? Title { get; set; }
    
    public string MiddleName { get; set; }
    
    public string EmailAddress { get; set; }
}
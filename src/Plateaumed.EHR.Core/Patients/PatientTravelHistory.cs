using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Country;

namespace Plateaumed.EHR.Patients;

[Table("PatientTravelHistories")]
public class PatientTravelHistory : FullAuditedEntity<long>
{

    [Required]
    public int CountryId { get; set; }
    
    [ForeignKey("CountryId")]
    public Country Country { get; set; }
    
    [Required]
    [StringLength(GeneralStringLengthConstant.MaxStringLength)]
    public string City { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int Duration { get; set; }
    
    [Required] 
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public UnitOfTime Interval { get; set; }
}
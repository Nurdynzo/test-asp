using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using System.Collections.Generic;

namespace Plateaumed.EHR.Misc.Country {
  [Table("Regions")]
  [Audited]
  public class Region: FullAuditedEntity < int > {
    [Required]
    [StringLength(RegionConsts.MaxNameLength, MinimumLength = RegionConsts.MinNameLength)]
    public virtual string Name { get; set;}

    [StringLength(RegionConsts.MaxShortNameLength, MinimumLength = RegionConsts.MinShortNameLength)]
    public virtual string ShortName { get; set;}

    [Required]
    [ForeignKey("CountryId")]
    public Country CountryFk { get; set; }
    public virtual int CountryId { get; set;}

    public ICollection<District> Districts {get; set;}

    public Region(string name, int countryId, string shortName = null) {
      Name = name;
      CountryId = countryId;
      ShortName = shortName;
    }

    public Region() {}
  }
}
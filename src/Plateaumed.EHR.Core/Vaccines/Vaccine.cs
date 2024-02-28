using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Vaccines
{
    [Table("Vaccines")]
    public class Vaccine : FullAuditedEntity<long>
    {
        public long? GroupId { get; set; }

        [ForeignKey("GroupId")]
        public VaccineGroup Group { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public ICollection<VaccineSchedule> Schedules { get; set; } = new List<VaccineSchedule>();
    }
}
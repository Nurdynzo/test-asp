using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plateaumed.EHR.Misc;
using System.Collections;

namespace Plateaumed.EHR.NextAppointments.Dtos
{
    public class StaffMemberJobDto
    {
        public long StaffMemberId { get; set; }
        public long FirstName { get; set; }
        public long LastName { get; set; }

        public long FacilityId { get; set; }

        public long UserId { get; set; }
        public string StaffCode { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public ICollection<StaffJobDto> Jobs { get; set; }
    }

    public class StaffJobDto
    {
        public long? JobLevelId { get; set; }

        public long? DepartmentId { get; set; }
        public long? FacilityId { get; set; }

        public long? UnitId { get; set; }

        public ICollection<ServiceCentreType> ServiceCentre { get; set; } = new List<ServiceCentreType>();
    }
}

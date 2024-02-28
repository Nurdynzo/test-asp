using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Authorization.Delegation
{
    [Table("AppDelegatedUsers")]
    public class DelegatedUser : FullAuditedEntity<long>
    {
        public int TenantId { get; set; }
        public string Email { get; set; }
        public DelegatedUserEnum Status { get; set; }
    }
}

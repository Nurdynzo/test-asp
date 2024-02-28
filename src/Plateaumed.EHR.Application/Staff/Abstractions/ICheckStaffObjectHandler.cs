using Plateaumed.EHR.Staff.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Staff.Abstractions
{
    public interface ICheckStaffObjectHandler
    {
        Task CheckStaffCode(CreateOrEditStaffMemberRequest request, int? tenantId);
    }
}

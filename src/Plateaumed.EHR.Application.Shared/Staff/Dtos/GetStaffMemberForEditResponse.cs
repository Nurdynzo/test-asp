using Plateaumed.EHR.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetStaffMemberForEditResponse
    {
        public UserEditDto User { get; set; }

        public long StaffMemberId { get; set; }

        public string PhoneCode { get; set; }

        public string StaffCode { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public string AdminRole { get; set; }

        public List<GetStaffJob> Jobs { get; set; }

    }
}

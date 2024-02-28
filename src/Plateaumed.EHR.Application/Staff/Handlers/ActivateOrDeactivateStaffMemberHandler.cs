using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plateaumed.EHR.Authorization.Users;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Staff.Handlers
{
    public class ActivateOrDeactivateStaffMemberHandler : IActivateOrDeactivateStaffMemberHandler
    {
        private readonly IRepository<StaffMember, long> _staffRepository;

        public ActivateOrDeactivateStaffMemberHandler(IRepository<StaffMember, long> staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task Handle(ActivateOrDeactivateStaffMemberRequest request)
        {
            var staff = await _staffRepository.GetAll().Where(x => x.UserId == request.Id).Include(x => x.UserFk).FirstOrDefaultAsync();

            if (staff == null)
            {
                throw new UserFriendlyException("Staff member cannot be found");
            }

            staff.UserFk.IsActive = request.IsActive;
            await _staffRepository.UpdateAsync(staff);
        }

    }
}

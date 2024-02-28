using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Staff;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class GetSpecializedProcedureNurseDetailQueryHandler : IGetSpecializedProcedureNurseDetailQueryHandler
    {
        private readonly IRepository<SpecializedProcedureNurseDetail, long> _specializedProcedureNurseDetailRepository;
        private readonly IRepository<StaffMember, long> _staffMemberRepository;
        public GetSpecializedProcedureNurseDetailQueryHandler(
            IRepository<SpecializedProcedureNurseDetail, long> specializedProcedureNurseDetailRepository,
            IRepository<StaffMember, long> staffMemberRepository)
        {
            _specializedProcedureNurseDetailRepository = specializedProcedureNurseDetailRepository;
            _staffMemberRepository = staffMemberRepository;
        }
        public async Task<GetSpecializedProcedureNurseDetailResponse> Handle(long procedureId)
        {
            var query = (from s in _specializedProcedureNurseDetailRepository.GetAll()
                         join scrubNurse in _staffMemberRepository.GetAll()
                             .Include(x => x.UserFk) on s.ScrubStaffMemberId equals scrubNurse.Id into scrubNurseJoin
                         from scrubNurse in scrubNurseJoin.DefaultIfEmpty()
                         join circulatingNurse in _staffMemberRepository.GetAll()
                             .Include(x => x.UserFk) on s.CirculatingStaffMemberId equals circulatingNurse.Id into circulatingNurseJoin
                         from circulatingNurse in circulatingNurseJoin.DefaultIfEmpty()
                         where s.ProcedureId == procedureId
                         select new GetSpecializedProcedureNurseDetailResponse()
                         {
                             Id = s.Id,
                             ProcedureId = s.ProcedureId,
                             TimePatientReceived = s.TimePatientReceived,
                             CirculatingNurseName = scrubNurse!= null ? scrubNurse.UserFk.DisplayName : null,
                             ScrubNurseName = circulatingNurse != null ?circulatingNurse.UserFk.DisplayName:null
                         });

            return await query.FirstOrDefaultAsync();


        }
    }
}

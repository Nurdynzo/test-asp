using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Staff;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class GetSpecializedProcedureOverviewQueryHandler : IGetSpecializedProcedureOverviewQueryHandler
    {
        private readonly IRepository<Admission,long> _admissionRepository;
        private readonly IRepository<Ward,long> _wardRepository;
        private readonly IRepository<PatientEncounter,long> _patientEncounterRepository;
        private readonly IRepository<SpecializedProcedureNurseDetail,long> _specializedProcedureNurseDetailRepository;
        private readonly IRepository<SpecializedProcedure,long> _specializedProcedureRepository;
        private readonly IRepository<Procedure,long> _procedureRepository;
        private readonly IRepository<StaffMember,long> _staffMemberRepository;
        private readonly IRepository<User,long> _userRepository;
        public GetSpecializedProcedureOverviewQueryHandler(
            IRepository<Admission, long> admissionRepository,
            IRepository<PatientEncounter, long> patientEncounterRepository,
            IRepository<SpecializedProcedureNurseDetail, long> specializedProcedureNurseDetailRepository,
            IRepository<SpecializedProcedure, long> specializedProcedureRepository,
            IRepository<Ward, long> wardRepository,
            IRepository<Procedure, long> procedureRepository,
            IRepository<StaffMember, long> staffMemberRepository,
            IRepository<User, long> userRepository)
        {
            _admissionRepository = admissionRepository;
            _patientEncounterRepository = patientEncounterRepository;
            _specializedProcedureNurseDetailRepository = specializedProcedureNurseDetailRepository;
            _specializedProcedureRepository = specializedProcedureRepository;
            _wardRepository = wardRepository;
            _procedureRepository = procedureRepository;
            _staffMemberRepository = staffMemberRepository;
            _userRepository = userRepository;
        }
        public async Task<GetSpecializedProcedureOverviewQueryResponse> Handle(long patientId, long encounterId)
        {
            var query = from e in _patientEncounterRepository.GetAll()
                        where e.Id == encounterId && e.PatientId == patientId
                        select new GetSpecializedProcedureOverviewQueryResponse
                        {
                            Procedure = (from p in _procedureRepository.GetAll()
                                         join s in _specializedProcedureRepository.GetAll() on p.Id equals s.ProcedureId
                                         join u in _userRepository.GetAll() on s.AnaesthetistUserId equals u.Id into uJoin
                                         from u in uJoin.DefaultIfEmpty()
                                         where p.PatientId == patientId && p.EncounterId == encounterId
                                         orderby p.CreationTime
                                         group new
                                         {
                                             p,
                                             u,
                                             s
                                         } by new
                                         {
                                             p.Id,
                                             s.ProcedureName,
                                             DisplayName = (u != null ? (u.Title.HasValue ? u.Title + " " : string.Empty) + u.Name + " " + (!string.IsNullOrEmpty(u.MiddleName)? u.MiddleName.Substring(0, 1) + ". " : string.Empty) + u.Surname : "")
                                         } into g
                                         select new SpecializedProcedureDetails
                                         {
                                             ProcedureName = g.Key.ProcedureName,
                                             Anaesthetist = g.Key.DisplayName,
                                             Anaesthesia = ""//Todo : get anaesthesia
                                         }).ToList(),
                            AdmissionDetails = e.WardId != null && e.AdmissionId != null ?
                                (from w in _wardRepository.GetAll()
                                 join a in _admissionRepository.GetAll() on e.AdmissionId equals a.Id
                                 where w.Id == e.WardId
                                 select new AdmissionDetails
                                 {
                                     Name = w.Name,
                                     DateAdmitted = DateOnly.FromDateTime(a.CreationTime),
                                     LengthStayed = (DateTime.Now - a.CreationTime).Days,
                                 }).FirstOrDefault() : null,
                            ScrubNurse = (from s in _specializedProcedureNurseDetailRepository.GetAll()
                                          join p in _procedureRepository.GetAll() on s.ProcedureId equals p.Id
                                          join n in _staffMemberRepository.GetAll() on s.ScrubStaffMemberId equals n.Id
                                          join u in _userRepository.GetAll() on n.UserId equals u.Id
                                          where p.PatientId == patientId && p.EncounterId == encounterId
                                          select u.DisplayName).FirstOrDefault(),
                            Specialist = "",//Todo : get specialist
                            SpecialistAssistant = "",//Todo : get specialist
                            BloodUnits = ""//Todo : not implemented yet
                        };

            return await query.FirstOrDefaultAsync();


        }
    }
}

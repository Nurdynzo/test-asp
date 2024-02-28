using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers
{
    public class GetPatientInterventionsQueryHandler : IGetPatientInterventionsQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMapping;
        private readonly IRepository<Procedure, long> _procedure;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<User,long> _userRepository;

        public GetPatientInterventionsQueryHandler(
            IRepository<Patient, long> patientRepository,
            IRepository<PatientCodeMapping, long> patientCodeMapping,
            IRepository<Procedure, long> procedure,
            IAbpSession abpSession,
            IRepository<User, long> userRepository)
        {
            _patientRepository = patientRepository;
            _patientCodeMapping = patientCodeMapping;
            _procedure = procedure;
            _abpSession = abpSession;
            _userRepository = userRepository;
        }

        public async Task<GetPatientInterventionsResponseDto> Handle(long patientId, long facilityId)
        {
            await ValidateInput(patientId);

            var query = await (from p in _patientRepository.GetAll().Where(x => x.Id == patientId)
                               join pc in _patientCodeMapping.GetAll() on p.Id equals pc.PatientId
                               where pc.FacilityId == facilityId
                               select new GetPatientInterventionsResponseDto
                               {
                                   PatientId = p.Id,
                                   Age = $"{DateTime.Now.Year - p.DateOfBirth.Year}yrs",
                                   FirstName = p.FirstName,
                                   LastName = p.LastName,
                                   MiddleName = p.MiddleName ?? "",
                                   Gender = p.GenderType.ToString(),
                                   PatientCode = pc.PatientCode ?? "",
                                   PictureUrl = p.PictureUrl ?? "",
                                   Interventions = (from pr in _procedure
                                                        .GetAll()
                                                        .IgnoreQueryFilters()
                                                    join u in _userRepository.GetAll() on pr.DeleterUserId equals u.Id into user
                                                    from u in user.DefaultIfEmpty()
                                                    where pr.TenantId == _abpSession.TenantId && pr.PatientId == p.Id
                                                    select new SelectedProceduresDto
                                                    {
                                                        ProcedureId = pr.Id,
                                                        SelectedProcedure = pr.SelectedProcedures == null ? new List<SelectedProcedureListDto>()
                                                            : JsonConvert.DeserializeObject<List<SelectedProcedureListDto>>(pr.SelectedProcedures),
                                                        IsDeleted = pr.IsDeleted,
                                                        DeletedUser = u != null ? u.DisplayName : "",
                                                        Status = pr.ProcedureStatus
                                                    }).ToList()
                               }).FirstOrDefaultAsync();
            return query;
        }

        private async Task ValidateInput(long patientId)
        {
            _ = await _patientRepository.GetAll().Where(x => x.Id == patientId).FirstOrDefaultAsync() ??
                throw new UserFriendlyException("Patient not found");
        }
    }
}

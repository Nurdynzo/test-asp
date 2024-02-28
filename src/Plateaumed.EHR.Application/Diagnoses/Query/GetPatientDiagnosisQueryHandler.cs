using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses.Query
{
    public class GetPatientDiagnosisQueryHandler : IGetPatientDiagnosisQueryHandler
    {
        private readonly IBaseQuery _baseQuery;
        private readonly IRepository<User, long> _userRepository;

        public GetPatientDiagnosisQueryHandler(
            IBaseQuery baseQuery,
            IRepository<User, long> userRepository)
        {
            _baseQuery = baseQuery;
            _userRepository = userRepository;
        }
        public async Task<List<PatientDiagnosisReturnDto>> Handle(int patientId)
        {
            return await (from diagnosis in _baseQuery.GetPatientDiagnosisBaseQuery(patientId)
                          join user in _userRepository.GetAll() on diagnosis.DeleterUserId equals user.Id into u
                          from user in u.DefaultIfEmpty()
                          orderby diagnosis.Id descending
                          select new PatientDiagnosisReturnDto
                          {
                              Id = diagnosis.Id,
                              TenantId = diagnosis.TenantId,
                              PatientId = diagnosis.PatientId,
                              Sctid = diagnosis.Sctid,
                              Description = diagnosis.Description,
                              Notes = diagnosis.Notes,
                              CreationTime = diagnosis.CreationTime,
                              IsDeleted = diagnosis.IsDeleted,
                              DeletedUser = user == null ? "" : user.DisplayName,

                          })
                .ToListAsync();
        }

       
    }
}

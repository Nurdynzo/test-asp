using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Query;

public class GetPatientMedicationQueryHandler : IGetPatientMedicationQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<User,long> _userRepository;
    
    public GetPatientMedicationQueryHandler(
        IBaseQuery baseQuery,
        IAbpSession abpSession,
        IRepository<User, long> userRepository)
    {
        _baseQuery = baseQuery;
        _abpSession = abpSession;
        _userRepository = userRepository;
    }
    
    public async Task<List<PatientMedicationForReturnDto>> Handle(int patientId, long? procedureId = null)
    {
        var medications = await (from m in _baseQuery
                                     .GetPatientMedications(patientId, procedureId)
                                 join u in _userRepository.GetAll() on m.DeleterUserId equals u.Id into user
                                 from u in user.DefaultIfEmpty()
                                 join du in _userRepository.GetAll() on m.DiscontinueUserId equals du.Id into disUser
                                 from du in disUser.DefaultIfEmpty()
                                 where m.TenantId == _abpSession.TenantId
                                 orderby m.CreationTime descending
                                 select new PatientMedicationForReturnDto
                                 {
                                     Id = m.Id,
                                     PatientId = m.PatientId,
                                     ProcedureId = m.ProcedureId,
                                     Duration = m.Duration,
                                     CreationTime = m.CreationTime,
                                     IsDeleted = m.IsDeleted,
                                     Direction =  m.Direction,
                                     ProductId = m.ProductId,
                                     ProductName = m.ProductName,
                                     ProductSource = m.ProductSource,
                                     DoseUnit = m.DoseUnit,
                                     Frequency = m.Frequency,
                                     ProcedureEntryType = m.ProcedureEntryType.ToString(),
                                     Note = m.Note,
                                     DeletedUser = u != null ? u.DisplayName : "",
                                     IsAdministered = m.IsAdministered,
                                     IsDiscontinued = m.IsDiscontinued,
                                     DiscontinueUser = du != null ? du.DisplayName: ""
                                 }).ToListAsync();
        return medications;

    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Handlers;

public class GetPatientVaccinationQueryHandler : IGetPatientVaccinationQueryHandler
{
    private readonly IRepository<Vaccination, long> _vaccinationRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<User,long> _userRepository;

    public GetPatientVaccinationQueryHandler(
        IRepository<Vaccination, long> vaccinationRepository,
        IObjectMapper objectMapper,
        IAbpSession abpSession,
        IRepository<User, long> userRepository)
    {
        _vaccinationRepository = vaccinationRepository;
        _objectMapper = objectMapper;
        _abpSession = abpSession;
        _userRepository = userRepository;
    }

    public async Task<List<VaccinationResponseDto>> Handle(EntityDto<long> patientId, long? procedureId = null)
    {
        var vaccinationHistories = await (from v in _vaccinationRepository
            .GetAll()
            .IgnoreQueryFilters()
            .Include(v => v.Vaccine)
            .Include(v => v.VaccineSchedule)
            join u in _userRepository.GetAll() on v.DeleterUserId equals u.Id into uJoin
            from u in uJoin.DefaultIfEmpty()
            orderby v.CreationTime descending
            where v.TenantId == _abpSession.TenantId && v.PatientId == patientId.Id &&
                  (procedureId == null || v.ProcedureId == procedureId.Value)
            select new VaccinationResponseDto
                {
                    ProcedureId = v.ProcedureId,
                    VaccineId = v.VaccineId,
                    PatientId = v.PatientId,
                    Vaccine = _objectMapper.Map<GetVaccineResponse>(v.Vaccine),
                    VaccineSchedule = _objectMapper.Map<VaccineScheduleDto>(v.VaccineSchedule),
                    IsAdministered = v.IsAdministered,
                    DueDate = v.DueDate,
                    DateAdministered = v.DateAdministered,
                    HasComplication = v.HasComplication,
                    VaccineBrand = v.VaccineBrand,
                    VaccineBatchNo = v.VaccineBatchNo,
                    Note = v.Note,
                    ProcedureEntryType = v.ProcedureEntryType != null ? v.ProcedureEntryType.ToString(): "",
                    CreationTime = v.CreationTime,
                    IsDeleted = v.IsDeleted,
                    VaccineScheduleId = v.VaccineScheduleId,
                    DeletedUser = u != null ? u.DisplayName : "",
                    Id = v.Id

                })
            .ToListAsync();

        return vaccinationHistories;
    }
}

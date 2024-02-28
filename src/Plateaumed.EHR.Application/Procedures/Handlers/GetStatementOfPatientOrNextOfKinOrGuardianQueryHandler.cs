using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class GetStatementOfPatientOrNextOfKinOrGuardianQueryHandler : IGetStatementOfPatientOrNextOfKinOrGuardianQueryHandler
{
    private readonly IRepository<ProcedureConsentStatement, long> _procedureConsentStatementRepository; 
    private readonly IObjectMapper _objectMapper;
    private readonly IGetCurrentUserFacilityIdQueryHandler _getCurrentUserFacilityIdQueryHandler;
    private readonly IRepository<Facility, long> _facilityRepository; 
    
    public GetStatementOfPatientOrNextOfKinOrGuardianQueryHandler(IRepository<ProcedureConsentStatement, long> procedureConsentStatementRepository, IObjectMapper objectMapper, IGetCurrentUserFacilityIdQueryHandler getCurrentUserFacilityIdQueryHandler, IRepository<Facility, long> facilityRepository)
    {
        _procedureConsentStatementRepository = procedureConsentStatementRepository;
        _objectMapper = objectMapper;
        _getCurrentUserFacilityIdQueryHandler = getCurrentUserFacilityIdQueryHandler;
        _facilityRepository = facilityRepository;
    }
    
    public async Task<StatementOfPatientOrNextOfKinOrGuardianResponseDto> Handle(long procedureId, IAbpSession abpSession)
    { 
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required.");
        
        var procedureDocument = await _procedureConsentStatementRepository
            .GetAll()
            .Include(v => v.Patient)
            .Include(v => v.InterpretedByStaffUser)
            .ThenInclude(v => v.StaffMemberFk)
            .SingleOrDefaultAsync(v => v.ProcedureId == procedureId && v.TenantId == abpSession.TenantId.Value);
        
        var statementResponse = _objectMapper.Map<StatementOfPatientOrNextOfKinOrGuardianResponseDto>(procedureDocument);
        if (statementResponse != null)
        {
            var facilityId = await _getCurrentUserFacilityIdQueryHandler.Handle(procedureDocument.CreatorUserId.Value);
            var facility = await _facilityRepository.GetAll().SingleOrDefaultAsync(v => v.Id == facilityId);

            // set the facility details
            if (facility != null)
            {
                statementResponse.FacilityName = facility.Name;
                statementResponse.FacilityLevel = facility.Level.ToString();
            }
        }

        // return response
        return statementResponse;
    }
}
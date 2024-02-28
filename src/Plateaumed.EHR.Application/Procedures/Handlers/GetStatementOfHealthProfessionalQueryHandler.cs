using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class GetStatementOfHealthProfessionalQueryHandler : IGetStatementOfHealthProfessionalQueryHandler
{
    private readonly IRepository<ProcedureConsentStatement, long> _procedureConsentStatementRepository; 
    private readonly IObjectMapper _objectMapper;
    
    public GetStatementOfHealthProfessionalQueryHandler(IRepository<ProcedureConsentStatement, long> procedureConsentStatementRepository, IObjectMapper objectMapper)
    {
        _procedureConsentStatementRepository = procedureConsentStatementRepository;
        _objectMapper = objectMapper;
    } 
    
    public async Task<StatementOfHealthProfessionalResponseDto> Handle(long procedureId, IAbpSession abpSession)
    {
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required.");
        
        var procedureDocument = await _procedureConsentStatementRepository
            .GetAll()
            .Include(v => v.Patient)
            .SingleOrDefaultAsync(v => v.ProcedureId == procedureId && v.TenantId == abpSession.TenantId.Value);
        
        return _objectMapper.Map<StatementOfHealthProfessionalResponseDto>(procedureDocument);
    }
}
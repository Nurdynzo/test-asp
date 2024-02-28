using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class GetSignConfirmationOfConsentQueryHandler : IGetSignConfirmationOfConsentQueryHandler
{
    private readonly IRepository<ProcedureConsentStatement, long> _procedureConsentStatementRepository; 
    private readonly IObjectMapper _objectMapper;
    
    public GetSignConfirmationOfConsentQueryHandler(IRepository<ProcedureConsentStatement, long> procedureConsentStatementRepository, IObjectMapper objectMapper)
    {
        _procedureConsentStatementRepository = procedureConsentStatementRepository;
        _objectMapper = objectMapper;
    }
    
    public async Task<SignConfirmationOfConsentDto> Handle(long procedureId, IAbpSession abpSession)
    { 
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required.");

        return await _procedureConsentStatementRepository
            .GetAll()
            .Where(v => v.ProcedureId == procedureId && v.TenantId == abpSession.TenantId.Value)
            .Select(s => _objectMapper.Map<SignConfirmationOfConsentDto>(s))
            .SingleOrDefaultAsync();

    }
}
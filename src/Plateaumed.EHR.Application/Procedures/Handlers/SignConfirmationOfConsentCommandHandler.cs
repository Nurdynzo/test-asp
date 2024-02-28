using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class SignConfirmationOfConsentCommandHandler : ISignConfirmationOfConsentCommandHandler
{
    private readonly IRepository<ProcedureConsentStatement, long> _procedureConsentStatementRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    
    public SignConfirmationOfConsentCommandHandler(IRepository<ProcedureConsentStatement, long> procedureConsentStatementRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _procedureConsentStatementRepository = procedureConsentStatementRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(SignConfirmationOfConsentDto requestDto, IAbpSession abpSession)
    {
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required.");
        
        var procedureDocument = await _procedureConsentStatementRepository
            .GetAll().FirstOrDefaultAsync(v => v.ProcedureId == requestDto.ProcedureId && v.TenantId == abpSession.TenantId.Value);

        if (procedureDocument == null)
            throw new UserFriendlyException($"Statement documentation needs to be completed before signing.");

        if (procedureDocument.PatientId != requestDto.PatientId)
            throw new UserFriendlyException($"This document does not belong to this patient."); 
        
        // map request data and set other properties 
        procedureDocument.ConfirmedByConsultantName = requestDto.ConfirmedByConsultantName;
        procedureDocument.ConfirmedByPrimarySpecialistName = requestDto.ConfirmedByPrimarySpecialistName;
        
        // update entity 
        await _procedureConsentStatementRepository.UpdateAsync(procedureDocument);
        
        // save changes to database
        await _unitOfWorkManager.Current.SaveChangesAsync(); 
    }
}
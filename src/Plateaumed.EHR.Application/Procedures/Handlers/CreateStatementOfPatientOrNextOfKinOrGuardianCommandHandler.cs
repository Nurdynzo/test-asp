using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class CreateStatementOfPatientOrNextOfKinOrGuardianCommandHandler : ICreateStatementOfPatientOrNextOfKinOrGuardianCommandHandler
{
    private readonly IRepository<ProcedureConsentStatement, long> _procedureConsentStatementRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;

    public CreateStatementOfPatientOrNextOfKinOrGuardianCommandHandler(IRepository<ProcedureConsentStatement, long> procedureConsentStatementRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _procedureConsentStatementRepository = procedureConsentStatementRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(CreateStatementOfPatientOrNextOfKinOrGuardianDto requestDto, IAbpSession abpSession)
    {
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required.");
        
        var procedureDocument = await _procedureConsentStatementRepository
            .GetAll()
            .FirstOrDefaultAsync(v => v.ProcedureId == requestDto.ProcedureId && v.TenantId == abpSession.TenantId.Value);

        if (procedureDocument == null)
            throw new UserFriendlyException($"Statement of health professional is required before creating the statement of patient.");

        if (procedureDocument.PatientId != requestDto.PatientId)
            throw new UserFriendlyException($"This document does not belong to this patient.");
        
        // map request data and set other properties
        procedureDocument.AdditionalProcedures = JsonConvert.SerializeObject(requestDto.AdditionalProcedures);
        procedureDocument.UsePatientAuthorizedNextOfKinOrGuardian = requestDto.UsePatientAuthorizedNextOfKinOrGuardian;
        procedureDocument.SignatureOfNextOfKinOrGuardian = requestDto.SignatureOfNextOfKinOrGuardian;
        procedureDocument.NextOfKinOrGuardianGovIssuedId = requestDto.NextOfKinOrGuardianGovIssuedId;
        procedureDocument.NextOfKinOrGuardianGovIssuedIdNumber = requestDto.NextOfKinOrGuardianGovIssuedIdNumber;
        procedureDocument.SignatureOfWitness = requestDto.SignatureOfWitness;
        procedureDocument.SignatureOfWitnessGovIssuedId = requestDto.SignatureOfWitnessGovIssuedId;
        procedureDocument.SignatureOfWitnessGovIssuedIdNumber = requestDto.SignatureOfWitnessGovIssuedIdNumber;
        procedureDocument.SecondaryLanguageOfInterpretation = requestDto.SecondaryLanguageOfInterpretation;
        procedureDocument.InterpretedByStaffUserId = requestDto.InterpretedByStaffUserId;
        procedureDocument.SecondarySignatureOfNextOfKinOrGuardian = requestDto.SecondarySignatureOfNextOfKinOrGuardian;
        procedureDocument.SecondaryNextOfKinOrGuardianGovIssuedId = requestDto.SecondaryNextOfKinOrGuardianGovIssuedId;
        procedureDocument.SecondaryNextOfKinOrGuardianGovIssuedIdNumber = requestDto.SecondaryNextOfKinOrGuardianGovIssuedIdNumber;
        procedureDocument.SecondarySignatureOfWitness = requestDto.SecondarySignatureOfWitness;
        procedureDocument.SecondarySignatureOfWitnessGovIssuedId = requestDto.SecondarySignatureOfWitnessGovIssuedId;
        procedureDocument.SecondarySignatureOfWitnessGovIssuedIdNumber = requestDto.SecondarySignatureOfWitnessGovIssuedIdNumber;
        
        // update entity 
        await _procedureConsentStatementRepository.UpdateAsync(procedureDocument);
        
        // save changes to database
        await _unitOfWorkManager.Current.SaveChangesAsync(); 
    }
}
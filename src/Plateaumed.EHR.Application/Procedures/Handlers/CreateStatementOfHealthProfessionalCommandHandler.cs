using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
using Z.EntityFramework.Plus;

namespace Plateaumed.EHR.Procedures.Handlers;

public class CreateStatementOfHealthProfessionalCommandHandler : ICreateStatementOfHealthProfessionalCommandHandler
{
    private readonly IRepository<ProcedureConsentStatement, long> _procedureConsentStatementRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    
    public CreateStatementOfHealthProfessionalCommandHandler(IRepository<ProcedureConsentStatement, long> procedureConsentStatementRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _procedureConsentStatementRepository = procedureConsentStatementRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(CreateStatementOfHealthProfessionalDto requestDto, IAbpSession abpSession)
    {
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required."); 

        var procedureDocument = await _procedureConsentStatementRepository
            .GetAll()
            .FirstOrDefaultAsync(v => v.ProcedureId == requestDto.ProcedureId && v.TenantId == abpSession.TenantId.Value);

        if (procedureDocument == null)
        {
            //map request data and set other properties
            var procedureConsentStatement = _objectMapper.Map<ProcedureConsentStatement>(requestDto); 
            procedureConsentStatement.TenantId = abpSession.TenantId.Value; 
            await _procedureConsentStatementRepository.InsertAsync(procedureConsentStatement);
        }
        else
        {
            // map request data and set other properties 
            procedureDocument.IntendedBenefits = requestDto.IntendedBenefits;
            procedureDocument.FrequentlyOccuringRisks = requestDto.FrequentlyOccuringRisks;
            procedureDocument.ExtraProcedures = requestDto.ExtraProcedures;
            procedureDocument.InformationProvidedToPatient = requestDto.InformationProvidedToPatient;
            procedureDocument.IsRegionalAnaesthesia = requestDto.IsRegionalAnaesthesia;
            procedureDocument.IsLocalAnaesthesia = requestDto.IsLocalAnaesthesia;
            procedureDocument.IsSedationAnaesthesia = requestDto.IsSedationAnaesthesia;
            procedureDocument.ConsultantName = requestDto.ConsultantName;
            procedureDocument.PrimarySpecialistName = requestDto.PrimarySpecialistName;
            
            // update entity
            await _procedureConsentStatementRepository.UpdateAsync(procedureDocument);
        } 
        
        // save changes to database
        await _unitOfWorkManager.Current.SaveChangesAsync(); 
    }
}
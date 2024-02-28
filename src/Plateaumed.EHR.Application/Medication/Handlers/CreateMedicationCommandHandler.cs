using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Handlers;

public class CreateMedicationCommandHandler : ICreateMedicationCommandHandler
{
    private readonly IRepository<AllInputs.Medication, long> _medicationRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;
    private readonly IAbpSession _abpSession;


    public CreateMedicationCommandHandler(IRepository<AllInputs.Medication, long> medicationRepository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IEncounterManager encounterManager,
        IAbpSession abpSession)
    {
        _medicationRepository = medicationRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
        _abpSession = abpSession;
    }
    
    public async Task Handle(CreateMultipleMedicationsDto requestDto)
    {
        var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("Tenant Id cannot be null");
        if(requestDto.Prescriptions.Count <= 0)
            throw new UserFriendlyException($"At least one prescription data is required.");
        
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);

        foreach (var medication in requestDto.Prescriptions.Select(_objectMapper.Map<AllInputs.Medication>))
        {
            medication.TenantId = tenantId;
            medication.EncounterId = requestDto.EncounterId;
            medication.ProcedureEntryType = requestDto.ProcedureEntryType;
            medication.ProcedureId = requestDto.ProcedureId;
            await _medicationRepository.InsertAsync(medication);
        }
        
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Symptom.Abstractions;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.Symptom;

[AbpAuthorize(AppPermissions.Pages_Symptoms)]
public class SymptomAppService : EHRAppServiceBase, ISymptomAppService
{
    private readonly ICreateSymptomCommandHandler _createSymptomCommandHandler;
    private readonly IGetPatientSymptomSummaryQueryHandler _getPatientSymptomSummaryQuery;
    private readonly IDeleteSymptomCommandHandler _deleteSymptomCommandHandler;
    
    public SymptomAppService(ICreateSymptomCommandHandler createSymptomCommandHandler, IGetPatientSymptomSummaryQueryHandler getPatientSymptomSummaryQuery, IDeleteSymptomCommandHandler deleteSymptomCommandHandler)
    {
        _createSymptomCommandHandler = createSymptomCommandHandler;
        _getPatientSymptomSummaryQuery = getPatientSymptomSummaryQuery;
        _deleteSymptomCommandHandler = deleteSymptomCommandHandler;
    }
    
    [AbpAuthorize(AppPermissions.Pages_Symptoms_Create)]
    public async Task CreateSymptom(CreateSymptomDto input) 
        => await _createSymptomCommandHandler.Handle(input);
    
    public async Task<List<PatientSymptomSummaryForReturnDto>> GetPatientSummary(int patientId) 
        => await _getPatientSymptomSummaryQuery.Handle(patientId, AbpSession.TenantId); 

    [AbpAuthorize(AppPermissions.Pages_Symptoms_Delete)]
    public async Task<string> DeleteSymptom(long symptomId) 
        => await _deleteSymptomCommandHandler.Handle(symptomId, AbpSession.UserId);

}
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.PlanItems.Abstractions;
using Plateaumed.EHR.PlanItems.Dtos;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Vaccines.Abstractions;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class GetPlansResultQueryHandler : IGetPlansResultQueryHandler
{
    private readonly IGetInvestigationRequestsQueryHandler _investigationRequestQueryHandler;
    private readonly IGetPatientMedicationQueryHandler _medicationRequestQueryHandler;
    private readonly IGetPatientProceduresQueryHandler _procedureRequestQueryHandler;
    private readonly IGetPatientPlanItemsSummaryQueryHandler _planItemRequestQueryHandler;
    private readonly IAbpSession _abpSession;
    private readonly IGetPatientVaccinationQueryHandler _getPatientVaccinationQueryHandler;
    private readonly IGetPatientReferralQueryHandler _getPatientReferralQueryHandler;
    private readonly IGetPatientConsultQueryHandler _getPatientConsultQueryHandler;
    private readonly IGetPatientAllNextAppointmentQueryHandler _getPatientNextAppointmentQueryHandler;
    
    public GetPlansResultQueryHandler(IGetInvestigationRequestsQueryHandler investigationRequestQueryHandler,
            IGetPatientMedicationQueryHandler medicationRequestQueryHandler,
            IGetPatientProceduresQueryHandler procedureRequestQueryHandler,
            IGetPatientPlanItemsSummaryQueryHandler planItemRequestQueryHandler,
            IAbpSession abpSession,
            IGetPatientVaccinationQueryHandler getPatientVaccinationQueryHandler,
            IGetPatientReferralQueryHandler getPatientReferralQueryHandler,
            IGetPatientConsultQueryHandler getPatientConsultQueryHandler,
            IGetPatientAllNextAppointmentQueryHandler getPatientNextAppointmentQueryHandler)
    {
        _investigationRequestQueryHandler = investigationRequestQueryHandler;
        _medicationRequestQueryHandler = medicationRequestQueryHandler;
        _procedureRequestQueryHandler = procedureRequestQueryHandler;
        _planItemRequestQueryHandler = planItemRequestQueryHandler;
        _abpSession = abpSession;
        _getPatientVaccinationQueryHandler = getPatientVaccinationQueryHandler;
        _getPatientReferralQueryHandler = getPatientReferralQueryHandler;
        _getPatientConsultQueryHandler = getPatientConsultQueryHandler;
        _getPatientNextAppointmentQueryHandler = getPatientNextAppointmentQueryHandler;
    }

    public async Task<Plans> Handle(long patientId, long encounterId, AdmitPatientRequest admission)
    {
        var tenantId = _abpSession.TenantId.GetValueOrDefault();
        var plans = new Plans()
        {
            InvestigationRequest = await _investigationRequestQueryHandler.Handle(new GetInvestigationRequestsRequest()
            {
                PatientId = patientId
            }) ?? new List<GetInvestigationRequestsResponse>(),
            Prescriptions = await _medicationRequestQueryHandler.Handle((int)patientId) ?? new List<PatientMedicationForReturnDto>(),
            Procedures = await _procedureRequestQueryHandler.Handle((int)patientId, "RequestProcedure",null) ?? new List<PatientProcedureResponseDto>(),
            PlanItems = await _planItemRequestQueryHandler.Handle((int)patientId, tenantId, null) ?? new List<PlanItemsSummaryForReturnDto>(),
            Vaccinations = await _getPatientVaccinationQueryHandler.Handle(new EntityDto<long>
            {
                Id = patientId,
            }),
            Referrals = await _getPatientReferralQueryHandler.Handle(encounterId),
            Consults = await _getPatientConsultQueryHandler.Handle(encounterId),
            Appointments = await _getPatientNextAppointmentQueryHandler.Handle(patientId),
            Admission = admission
        };

        return plans;
    }
}

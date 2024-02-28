using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Abp.Runtime.Session;
using Plateaumed.EHR.ReferAndConsults.Handlers;
using Plateaumed.EHR.Encounters.Abstractions;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Symptom.Abstractions;
using System;
using System.Linq;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Diagnoses.Abstraction;

namespace Plateaumed.EHR.Tests.ReferAndConsults.UnitTest;

[Trait("Category", "Unit")]
public class GetConsultationLetterQueryHandlerTest
{
    private readonly IReferAndConsultBasedQuery _basedQueryMock = Substitute.For<IReferAndConsultBasedQuery>();
    private readonly IGetPatientEncounterQueryHandler _patientEncounterQueryHandlerMock
         = Substitute.For<IGetPatientEncounterQueryHandler>();
    private readonly IGetStaffMemberWithUnitAndLevelQueryHandler _staffQueryHandlerMock
         = Substitute.For<IGetStaffMemberWithUnitAndLevelQueryHandler>();
    private readonly IGetPatientSymptomSummaryQueryHandler _symptpomQueryHandlerMock
         = Substitute.For<IGetPatientSymptomSummaryQueryHandler>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandlerMock
         = Substitute.For<IGetPatientPhysicalExamSummaryWithEncounterQueryHandler>();
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQueryMock
         = Substitute.For<IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler>();
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignReviewQueryHandlerMock
         = Substitute.For<IGetReviewAndSavePatientVitalSignQueryHandler>();
    private readonly IGetPatientDiagnosisWithEncounterQueryHandler _getPatientDiagnosisQueryMock
        = Substitute.For<IGetPatientDiagnosisWithEncounterQueryHandler>();


    [Fact]
    public async Task GetConsultationLetterQueryHandler_Handle_Successful_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        var patientEncounter = Util.Common.GetPatientEncounters(encounterId).FirstOrDefault();
        _patientEncounterQueryHandlerMock.Handle(encounterId).ReturnsForAnyArgs(patientEncounter);

        var ids = new EntityDto<long>();
        ids.Id = 1;
        var staffEncounter = Util.Common.GetStaffEncounters(encounterId, staffUserId, patientId).FirstOrDefault();
        var staffMember = Util.Common.GetStaffByUserId(staffUserId);
        _staffQueryHandlerMock.Handle(ids).ReturnsForAnyArgs(staffMember);

        var symptoms = Util.Common.GetPatientSymptoms();
        _symptpomQueryHandlerMock.Handle(Convert.ToInt32(patientId), 1).ReturnsForAnyArgs(symptoms);

        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);

        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));

        _vitalSignReviewQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        var patientInfo = Util.Common.GetPatients(patientId).FirstOrDefault();
        _basedQueryMock.GenerateSummary(patientInfo, symptoms, staffUserId)
            .ReturnsForAnyArgs(Util.Common.GenerateSummary(patientInfo, symptoms, staffUserId));

        _getPatientDiagnosisQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetListOfDianosis(patientId, encounterId));

        var handler = new GetConsultationLetterQueryHandler(_basedQueryMock, _patientEncounterQueryHandlerMock,
            _staffQueryHandlerMock, _symptpomQueryHandlerMock, _abpSessionMock,
            _physicalExaminationQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock,
            _vitalSignReviewQueryHandlerMock, _getPatientDiagnosisQueryMock);

        //Act
        var request = new ConsultRequestDto()
        {
            EncounterId = encounterId,
            ReceivingUnit = "A&E Unit",
            ReceivingConsultant = "Dr. Shola Judy"
        };
        var loginUser = new User()
        {
            Id = 1,
            Name = "Chozzy Ibinato"
        };
        var result = await handler.Handle(request, loginUser);
        long resultId = result == null ? 0 : 1;
        //Assert
        resultId.Equals(staffUserId);
    }

}
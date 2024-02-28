using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using PayPalCheckoutSdk.Orders;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Discharges.Handlers;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Discharges.UnitTest;

[Trait("Category", "Unit")]
public class EditDischargeCommandHandlerTest
{
    private readonly IRepository<Discharge, long> _dischargeRepositoryMock
        = Substitute.For<IRepository<Discharge, long>>();
    private readonly IObjectMapper _objectMapper = Substitute.For<IObjectMapper>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly EHR.PatientAppointments.Abstractions.IBaseQuery _appointmentBaseQueryMock = Substitute.For<EHR.PatientAppointments.Abstractions.IBaseQuery>();
    private readonly IRepository<DischargeMedication, long> _dischargeMedicationRepositoryMock
        = Substitute.For<IRepository<DischargeMedication, long>>();
    private readonly IGetPatientMedicationQueryHandler _medicationQueryHandlerMock = Substitute.For<IGetPatientMedicationQueryHandler>();
    private readonly IGetDischargeMedicationsQueryHandler _getDischargeMedicationQueryHandlerMock = Substitute.For<IGetDischargeMedicationsQueryHandler>();
    private readonly IRepository<DischargePlanItem, long> _dischargePlanItemRepositoryMock
        = Substitute.For<IRepository<DischargePlanItem, long>>();
    private readonly EHR.PlanItems.Abstractions.IBaseQuery _planItemsQueryHandlerMock = Substitute.For<EHR.PlanItems.Abstractions.IBaseQuery>();
    private readonly IGetDischargePlanItemsQueryHandler _getDischargePlanItemIdsMock = Substitute.For<IGetDischargePlanItemsQueryHandler>();

    private readonly IRepository<PatientCauseOfDeath, long> _causeOfDeathRepositoryMock = Substitute.For<IRepository<PatientCauseOfDeath, long>>();
    private readonly IRepository<DischargeNote, long> _noteRepositoryMock = Substitute.For<IRepository<DischargeNote, long>>();

    [Fact]
    public async Task EditDischargeCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var command = GetEditDischargeRequest(1);
        MockDependencies(command);

        //Act
        var handler = new EditDischargeCommandHandler(_dischargeRepositoryMock, _dischargeMedicationRepositoryMock, _dischargePlanItemRepositoryMock,
            _planItemsQueryHandlerMock, _getDischargePlanItemIdsMock, _unitOfWork, _appointmentBaseQueryMock, _abpSessionMock,
            _medicationQueryHandlerMock, _getDischargeMedicationQueryHandlerMock, _causeOfDeathRepositoryMock, _noteRepositoryMock);
        //Act
        var result = await handler.Handle(command);
        var isSuccess = result == null ? false : result.Id > 0 ? true : false;
        //Assert
        isSuccess.ShouldBe(true);
    }

    [Fact]
    public async Task EditDischargeCommandHandler_Handle_With_No_Prescription_Failed_Response()
    {
        //arrange
        var command = Util.Common.GetCreateDischargeRequest_No_Prescription(1);
        MockDependencies(command);
        //Act
        var handler = new EditDischargeCommandHandler(_dischargeRepositoryMock, _dischargeMedicationRepositoryMock, _dischargePlanItemRepositoryMock,
            _planItemsQueryHandlerMock, _getDischargePlanItemIdsMock, _unitOfWork, _appointmentBaseQueryMock, _abpSessionMock,
            _medicationQueryHandlerMock, _getDischargeMedicationQueryHandlerMock, _causeOfDeathRepositoryMock, _noteRepositoryMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("At least one prescription data is required.");
    }


    [Fact]
    public async Task EditDischargeCommandHandler_Handle_With_No_PlanItems_Failed_Response()
    {
        //arrange
        var command = Util.Common.GetCreateDischargeRequest_No_PlanItems(1);
        MockDependencies(command);
        //Act
        var handler = new EditDischargeCommandHandler(_dischargeRepositoryMock, _dischargeMedicationRepositoryMock, _dischargePlanItemRepositoryMock,
            _planItemsQueryHandlerMock, _getDischargePlanItemIdsMock, _unitOfWork, _appointmentBaseQueryMock, _abpSessionMock,
            _medicationQueryHandlerMock, _getDischargeMedicationQueryHandlerMock, _causeOfDeathRepositoryMock, _noteRepositoryMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("At least one plan item is required.");
    }

    [Fact]
    public async Task EditDischargeCommandHandler_Handle_With_No_AppointmentId_Successful_Response()
    {
        //arrange
        var command = Util.Common.GetCreateDischargeRequest_No_AppointmentId(1);
        MockDependencies(command);
        //Act
        var handler = new EditDischargeCommandHandler(_dischargeRepositoryMock, _dischargeMedicationRepositoryMock, _dischargePlanItemRepositoryMock,
            _planItemsQueryHandlerMock, _getDischargePlanItemIdsMock, _unitOfWork, _appointmentBaseQueryMock, _abpSessionMock,
            _medicationQueryHandlerMock, _getDischargeMedicationQueryHandlerMock, _causeOfDeathRepositoryMock, _noteRepositoryMock);
        //Act
        var result = await handler.Handle(command);
        var isSuccess = result == null ? false : result.Id > 0 ? true : false;
        //Assert
        isSuccess.ShouldBe(true);
    }

    //DAMA

    //DECEASED
    private void MockDependencies(CreateDischargeDto request)
    {
        _objectMapper.Map<Discharge>(Arg.Any<CreateDischargeDto>()).Returns(Util.Common.GetNormalDischargeInstance(request,1));
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _dischargeRepositoryMock.InsertAsync(Arg.Any<Discharge>())
            .ReturnsForAnyArgs(Util.Common.GetNormalDischargeInstance(request, 1));
        var appointmentBase = Util.Common.GetAppointmentsBaseQuery();
        _appointmentBaseQueryMock.GetAppointmentsBaseQuery().Returns(appointmentBase);
        var getall = Util.Common.GetDischargeAsQueryable(1, 1, 0).Where(s=>s.Id == request.Id &&
                    s.PatientId == 1 && s.TenantId == 1 && s.IsFinalized == false);
        _dischargeRepositoryMock.GetAll().Returns(getall);

        _noteRepositoryMock.InsertAsync(Arg.Any<DischargeNote>())
            .ReturnsForAnyArgs(Util.Common.GetDischargeNote(1, 1));
        _causeOfDeathRepositoryMock.GetAll().Returns(Util.Common.GetCausesOfDealth(request.Id.Value).BuildMock());
        _noteRepositoryMock.GetAll().Returns(Util.Common.GetDischargeNote(request.Id.Value).BuildMock());

        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);

        var addMed = new DischargeMedication()
        {
            TenantId = 1,
            DischargeId = 1,
            MedicationId = 1,
            CreatorUserId = 1,
            CreationTime = DateTime.UtcNow
        };    
    }
    private static CreateDischargeDto GetEditDischargeRequest(long patientId)
    {
        return Util.Common.GetCreateDischargeRequest(patientId).
            Where(s=>s.DischargeType == DoctorDischarge.DischargeEntryType.DAMA).FirstOrDefault();
    }

    public static List<DischargePlanItemDto> Handle(long dischargeId)
    {
        var res = new List<DischargePlanItemDto>();
        res.Add(new DischargePlanItemDto()
        {
            PlanItemId = 1,
            PatientId = 1,
            DischargeId = dischargeId,
            Description = "",
            CreationTime = DateTime.Now,
            DeletionTime = DateTime.Now
        });
        return res;
    }
}
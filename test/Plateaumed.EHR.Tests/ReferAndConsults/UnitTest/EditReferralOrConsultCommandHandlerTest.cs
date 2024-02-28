using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Microsoft.VisualStudio.TestPlatform.Common;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.ReferAndConsults;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.ReferAndConsults.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.ReferAndConsults.UnitTest;

[Trait("Category", "Unit")]
public class EditReferralOrConsultCommandHandlerTest
{
    private readonly IRepository<AllInputs.PatientReferralOrConsultLetter, long> _referAndConsultRepositoryMock
        = Substitute.For<IRepository<AllInputs.PatientReferralOrConsultLetter, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IEncounterManager _encounterManager = Substitute.For<IEncounterManager>();

    [Fact]
    public async Task EditReferralOrConsultLetterCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var encounterId = 1;
        var command = GetEditReferralConsultRequest(encounterId);
        MockDependencies(command, encounterId);
        //Act
        var handler = EditHandler();
        var result = await handler.Handle(command);
        var _id = result != null ? result.Id : 0;
        //Assert
        _id.ShouldBe(1);
    }



    private EditReferralOrConsultLetterCommandHandler EditHandler()
    {
        return new EditReferralOrConsultLetterCommandHandler(_referAndConsultRepositoryMock, _unitOfWork, _encounterManager);
    }

    private void MockDependencies(CreateReferralOrConsultLetterDto dtoModel, long encounterId)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        PatientReferralOrConsultLetter savedResult = null;
        _referAndConsultRepositoryMock.UpdateAsync(Arg.Do<PatientReferralOrConsultLetter>(result => savedResult = result));
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _referAndConsultRepositoryMock.GetAllList().ReturnsForAnyArgs(Util.Common.GetPatientReferralOrConsult(dtoModel,encounterId));
    }

    private static CreateReferralOrConsultLetterDto GetEditReferralConsultRequest(long encounterId)
    {
        return Util.Common.GetCreateReferralOrConsultRequest(encounterId)
            .FirstOrDefault(s => s.Type == InputType.Referral && s.Id == 1);
    }

}

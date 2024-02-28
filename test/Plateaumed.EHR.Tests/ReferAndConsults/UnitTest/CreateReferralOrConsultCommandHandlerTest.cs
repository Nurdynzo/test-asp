using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.ReferAndConsults;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.ReferAndConsults.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.ReferAndConsults.UnitTest;

[Trait("Category", "Unit")]
public class CreateReferralOrConsultCommandHandlerTest
{
    private readonly IRepository<AllInputs.PatientReferralOrConsultLetter, long> _referAndConsultRepositoryMock
        = Substitute.For<IRepository<AllInputs.PatientReferralOrConsultLetter, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IEncounterManager _encounterManager = Substitute.For<IEncounterManager>();

    [Fact]
    public async Task CreateReferralOrConsultLetterCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var encounterId = 1;
        var command = GetCreateReferralConsultRequest(encounterId);
        MockDependencies();
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var _id = result != null ? result.Id : 0;
        //Assert
        _id.ShouldBe(1);
    }

    [Fact]
    public async Task CreateReferralOrConsultLetterCommandHandler_Handle_ShouldCheckEncounterExists()
    {
        //arrange
        var encounterId = 1;
        var command = GetCreateReferralConsultRequest(encounterId);
        MockDependencies();
        //Act
        var handler = CreateHandler();
        await handler.Handle(command);
        //Assert
        await _encounterManager.Received(1).CheckEncounterExists(command.EncounterId);
        await _referAndConsultRepositoryMock.Received(1)
            .InsertAndGetIdAsync(Arg.Is<AllInputs.PatientReferralOrConsultLetter>(x => x.EncounterId == command.EncounterId));
    }


    private CreateReferralOrConsultLetterCommandHandler CreateHandler()
    {
        return new CreateReferralOrConsultLetterCommandHandler(_referAndConsultRepositoryMock, _unitOfWork, _encounterManager);
    }

    private void MockDependencies()
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _referAndConsultRepositoryMock.InsertAndGetIdAsync(Arg.Any<AllInputs.PatientReferralOrConsultLetter>())
            .ReturnsForAnyArgs(1);
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }

    private static CreateReferralOrConsultLetterDto GetCreateReferralConsultRequest(long encounterId)
    {
        return Util.Common.GetCreateReferralOrConsultRequest(encounterId)
            .FirstOrDefault(s => s.Type == InputType.Referral && s.Id == 0);
    }

}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Staff;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class CreateInvestigationResultReviewerHandlerTests : AppTestBase
    {
        private readonly IRepository<InvestigationResultReviewer, long> _investigationResultReviewer = Substitute.For<IRepository<InvestigationResultReviewer, long>>();
        private readonly IRepository<InvestigationResult, long> _investigationResult = Substitute.For<IRepository<InvestigationResult, long>>();
        private readonly IRepository<StaffMember, long> _staffMember = Substitute.For<IRepository<StaffMember, long>>();
        private readonly IRepository<ElectroRadPulmInvestigationResult, long> _investigationResultERadPulm = Substitute.For<IRepository<ElectroRadPulmInvestigationResult, long>>();

        private readonly IAbpSession _abpSession;

        public CreateInvestigationResultReviewerHandlerTests() => _abpSession = Resolve<IAbpSession>();

        [Fact]
        public async Task HandleGivenInvalidInvestigationResultIdShouldThrow()
        {
            // Arrange
            var request = new InvestigationResultReviewerRequestDto
            {
                InvestigationResultId = 0,
                ReviewerId = 1
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAll().Returns(GetInvestigationResults().BuildMock());
            _staffMember.GetAll().Returns(GetStaffMembers().BuildMock());
            _investigationResultERadPulm.GetAll().Returns(GetElectroRadPulmInvestigationResults().BuildMock());
            var handler = new CreateInvestigationResultReviewerHandler(_investigationResultReviewer, _abpSession, _investigationResult, _staffMember, _investigationResultERadPulm);

            //Act
            
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request, 1));
            //Assert
            exception.Message.ShouldBe("Investigation Result not found");
        }

        [Fact]
        public async Task HandleGivenInvalidIStaffMemberShouldThrow()
        {
            var request = new InvestigationResultReviewerRequestDto
            {
                InvestigationResultId = 1,
                ReviewerId = 0
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAll().Returns(GetInvestigationResults().BuildMock());
            _staffMember.GetAll().Returns(GetStaffMembers().BuildMock());
            _investigationResultERadPulm.GetAll().Returns(GetElectroRadPulmInvestigationResults().BuildMock());

            var handler = new CreateInvestigationResultReviewerHandler(_investigationResultReviewer, _abpSession, _investigationResult, _staffMember, _investigationResultERadPulm);
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request, 1));
            exception.Message.ShouldBe("Staff Member not found");
        }


        [Fact]
        public async Task HandleGivenInvalidTenantIdShouldThrow()
        {
            var request = new InvestigationResultReviewerRequestDto
            {
                InvestigationResultId = 1,
                ReviewerId = 1
            };

            LoginAsHostAdmin();
            _investigationResult.GetAll().Returns(GetInvestigationResults().BuildMock());
            _staffMember.GetAll().Returns(GetStaffMembers().BuildMock());
            _investigationResultERadPulm.GetAll().Returns(GetElectroRadPulmInvestigationResults().BuildMock());

            var handler = new CreateInvestigationResultReviewerHandler(_investigationResultReviewer, _abpSession, _investigationResult, _staffMember, _investigationResultERadPulm);
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request, 1));
            exception.Message.ShouldBe("Tenant not found");
        }

        [Fact]
        public async Task HandleGivenValidRequestShouldSave()
        {
            //Arrange
            var request = new InvestigationResultReviewerRequestDto
            {
                InvestigationResultId = 1,
                ReviewerId = 1
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAll().Returns(GetInvestigationResults().BuildMock());
            _staffMember.GetAll().Returns(GetStaffMembers().BuildMock());
            _investigationResultERadPulm.GetAll().Returns(GetElectroRadPulmInvestigationResults().BuildMock());

            InvestigationResultReviewer savedResult = null;
            var repository = Substitute.For<IRepository<InvestigationResultReviewer, long>>();
            await repository.InsertAsync(Arg.Do<InvestigationResultReviewer>(result => savedResult = result));
            var handler = new CreateInvestigationResultReviewerHandler(repository, _abpSession, _investigationResult, _staffMember, _investigationResultERadPulm);

            // Act
            await handler.Handle(request, 1);

            //Assert
            savedResult.ShouldNotBeNull();
            savedResult.ReviewerId.ShouldBe(request.ReviewerId);
            savedResult.InvestigationResultId.ShouldBe(request.InvestigationResultId);
        }

        [Fact]
        public async Task HandleGivenValidResultReviewerIdShouldUpdate()
        {
            // Arrange
            var request = new InvestigationResultReviewerRequestDto
            {
                InvestigationResultId = 1,
                ReviewerId = 1,
                Id = 1
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAll().Returns(GetInvestigationResults().BuildMock());
            _staffMember.GetAll().Returns(GetStaffMembers().BuildMock());

            _investigationResultReviewer.GetAsync(Arg.Any<long>()).Returns(new InvestigationResultReviewer());
            _investigationResultERadPulm.GetAll().Returns(GetElectroRadPulmInvestigationResults().BuildMock());

            var handler = new CreateInvestigationResultReviewerHandler(_investigationResultReviewer, _abpSession, _investigationResult, _staffMember, _investigationResultERadPulm);

            // Act
            await handler.Handle(request, 1);

            // Assert
            await _investigationResultReviewer.Received(1).UpdateAsync(Arg.Any<InvestigationResultReviewer>());
        }

        private static IQueryable<InvestigationResult> GetInvestigationResults()
                => new List<InvestigationResult>
                {
                    new()
                    {
                        Id = 1,
                        InvestigationId = 1,
                    },
                    new()
                    {
                        Id = 2,
                        InvestigationId = 2,
                    }
                }.AsQueryable();

        private static IQueryable<StaffMember> GetStaffMembers()
            => new List<StaffMember>
            {
                new()
                {
                    Id = 1
                },
                new()
                {
                    Id = 2,
                }
            }.AsQueryable();

        private static IQueryable<ElectroRadPulmInvestigationResult> GetElectroRadPulmInvestigationResults()
           => new List<ElectroRadPulmInvestigationResult>
           {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    TenantId = 1,
                    InvestigationId = 1
                }
           }.AsQueryable();

    }
}


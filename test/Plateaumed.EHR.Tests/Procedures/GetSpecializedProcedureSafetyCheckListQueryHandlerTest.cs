using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Handlers;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.Procedures
{
    [Trait("Category", "Unit")]
    public class GetSpecializedProcedureSafetyCheckListQueryHandlerTest
    {
        private readonly IRepository<SpecializedProcedureSafetyCheckList, long> _specializedProcedureSafetyCheckListRepository;
        private readonly IRepository<Patient,long> _patientRepository;
        private readonly IRepository<Procedure,long> _procedureRepository;
        private readonly IUnitOfWorkManager _unitOfWork;
        private readonly IGetSpecializedProcedureSafetyCheckListQueryHandler _getSpecializedProcedureSafetyCheckListQueryHandler;
        public GetSpecializedProcedureSafetyCheckListQueryHandlerTest()
        {
            _specializedProcedureSafetyCheckListRepository = Substitute.For<IRepository<SpecializedProcedureSafetyCheckList, long>>();
            _patientRepository = Substitute.For<IRepository<Patient, long>>();
            _procedureRepository = Substitute.For<IRepository<Procedure, long>>();
            _unitOfWork = Substitute.For<IUnitOfWorkManager>();
            _getSpecializedProcedureSafetyCheckListQueryHandler = new GetSpecializedProcedureSafetyCheckListQueryHandler(
                _specializedProcedureSafetyCheckListRepository,
                _patientRepository,
                _procedureRepository,
                _unitOfWork);
        }
        [Fact]
        public async Task Handle_With_Valid_PatientId_And_ProcedureId_Should_Return_SpecializedProcedureSafetyCheckList()
        {
            //Arrange
            var patientId = 1;
            var procedureId = 1;
            _specializedProcedureSafetyCheckListRepository.GetAll()
                .Returns(GetSpecializedProcedureCheckList().BuildMock());
            (_patientRepository.GetAsync(Arg.Any<long>())).Returns(Task.FromResult(new Patient()));
            (_procedureRepository.GetAsync(Arg.Any<long>())).Returns(Task.FromResult(new Procedure()));
            //Act
            var result = await _getSpecializedProcedureSafetyCheckListQueryHandler.Handle(patientId, procedureId);
            //Assert
            result.CheckLists.ShouldNotBeEmpty();
            result.PatientId.ShouldBe(1);
            result.ProcedureId.ShouldBe(1);
            await _unitOfWork.Current.Received(0).SaveChangesAsync();
        }
        [Fact]
        public async Task Handle_With_Invalid_PatientId_And_ProcedureId_Should_Throw_UserFriendlyException()
        {
            //Arrange
            var patientId = 0;
            var procedureId = 0;
            (_patientRepository.GetAsync(Arg.Any<long>())).ThrowsForAnyArgs(new UserFriendlyException("Patient not found"));
            _procedureRepository.GetAsync(Arg.Any<long>()).ThrowsForAnyArgs(new UserFriendlyException("Procedure not found"));
            //Act
            var result = await Assert.ThrowsAsync<UserFriendlyException>(async () => await _getSpecializedProcedureSafetyCheckListQueryHandler.Handle(patientId, procedureId));
            //Assert
            result.Message.ShouldBe("Patient not found");
        }
        private IQueryable<SpecializedProcedureSafetyCheckList> GetSpecializedProcedureCheckList()
        {
            return new List<SpecializedProcedureSafetyCheckList>
            {
               new ()
               {
                   PatientId = 1,
                   ProcedureId = 1,
                   CheckLists = SafetyCheckList.GetDefaultList()
               }

            }.AsQueryable();
        }
    }

}

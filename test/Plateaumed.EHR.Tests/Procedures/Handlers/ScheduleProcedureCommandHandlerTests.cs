using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using MockQueryable.NSubstitute;
using Newtonsoft.Json;
using NSubstitute; 
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Procedures.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures;

[Trait("Category", "Unit")]
public class ScheduleProcedureCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_NoProcedureWasSelected_ShouldThrow()
    {
        // Arrange
        var procedureRepository = Substitute.For<IRepository<Procedure, long>>();
        var procedureResponse = new List<Procedure>{new()
        {
            TenantId = 1,
            Id = 1
        }}.AsQueryable().BuildMock();
        
        var scheduleRepository = Substitute.For<IRepository<ScheduleProcedure, long>>();
        var scheduleResponse = new List<ScheduleProcedure>().AsQueryable().BuildMock();
        var session = Substitute.For<IAbpSession>();
        
        procedureRepository.GetAll().Returns(procedureResponse);
        scheduleRepository.GetAll().Returns(scheduleResponse);
        session.TenantId.Returns(1);
        
        var testCreateData = CreateTestData();
        testCreateData.Procedures = new List<SelectedProcedureDto>();

        var handler = new ScheduleProcedureCommandHandler(procedureRepository, scheduleRepository, _unitOfWork, _objectMapper);
        
        // Act
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(testCreateData, session));
        
        // Assert
        exception.Message.ShouldBe("Add at least one item."); 
    }
    
        [Fact]
        public async Task Handle_AlreadyScheduled_ShouldThrow()
        {
            // Arrange
            var procedureRepository = Substitute.For<IRepository<Procedure, long>>();
            var procedureResponse = new List<Procedure>{new()
            {
                TenantId = 1,
                Id = 1
            }}.AsQueryable().BuildMock();
            
            var scheduleRepository = Substitute.For<IRepository<ScheduleProcedure, long>>();
            var scheduleResponse= new List<ScheduleProcedure>{new()
            {
                TenantId = 1,
                Id = 1,
                SnowmedId = 1,
                ProcedureId = 1
                
            }}.AsQueryable().BuildMock();
            
            var session = Substitute.For<IAbpSession>();
            
            procedureRepository.GetAll().Returns(procedureResponse);
            scheduleRepository.GetAll().Returns(scheduleResponse);
            session.TenantId.Returns(1);
            
            var testCreateData = CreateTestData();
    
            var handler = new ScheduleProcedureCommandHandler(procedureRepository, scheduleRepository, _unitOfWork, _objectMapper);
            
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(testCreateData, session));
            
            // Assert
            exception.Message.ShouldBe("'Dressing of wound' Has already been scheduled."); 
        }
    
    private static ScheduleProcedureDto CreateTestData()
    {
        return new ScheduleProcedureDto
        {
            Duration = "Hours",
            Time = "5",
            ProcedureId = 1,
            ProposedDate = DateTime.Today,
            RoomId = 2,
            IsProcedureInSameSession = true,
            Procedures = new List<SelectedProcedureDto>()
            {
                new()
                {
                    SnowmedId = 1,
                    ProcedureName = "Dressing of wound"
                }   
            }
        };
    }
}
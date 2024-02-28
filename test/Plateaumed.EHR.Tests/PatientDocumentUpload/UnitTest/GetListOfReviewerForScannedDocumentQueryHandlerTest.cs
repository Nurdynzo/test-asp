using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PatientDocument.Query;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientDocumentUpload.UnitTest;

[Trait("Category", "Unit")]
public class GetListOfReviewerForScannedDocumentQueryHandlerTest
{
    private readonly IRepository<User,long> _userRepository = Substitute.For<IRepository<User,long>>();
    private readonly IRepository<UserRole,long> _userRoleRepository = Substitute.For<IRepository<UserRole,long>>();
    private readonly IRepository<Role> _roleRepository = Substitute.For<IRepository<Role>>();

    [Fact]
    public async Task Handle_Should_Return_List_Of_Reviewer()
    {
        // Arrange
        _userRepository.GetAll().Returns(GetUserAsQueryable().BuildMock());
        _roleRepository.GetAll().Returns(GetRoleAsQueryable().BuildMock());
        _userRoleRepository.GetAll().Returns(GetUserRoleAsQueryable().BuildMock());
        var handler = new GetListOfReviewerForScannedDocumentQueryHandler(_userRepository, _userRoleRepository , _roleRepository);
        
        //act
        var result = await handler.Handle();
        
        //assert
        result.Count.ShouldBe(2);
        result.ShouldContain(x => x.Id == 1);
        result.ShouldContain(x => x.Id == 2);

    }

    private static IQueryable<Role> GetRoleAsQueryable()
    {
        return new List<Role>()
        {
            new()
            {
                Id = 1,
                Name = StaticRoleNames.JobRoles.FrontDesk
                
            }
        }.AsQueryable();
    }

    private static IQueryable<UserRole> GetUserRoleAsQueryable()
    {
        return new List<UserRole>()
        {
            new()
            {
                Id = 1,
                UserId = 1,
                RoleId = 1
                
            },
            new()
            {
                Id = 2,
                UserId = 2,
                RoleId = 1
                
            }
        }.AsQueryable();
    }

    private static IQueryable<User> GetUserAsQueryable()
    {
        return new List<User>()
        {
            new ()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test",
                
            },
            new ()
            {
                Id = 2,
                Name = "Test2",
                Surname = "Test2",
                
            }
        }.AsQueryable();
    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Query;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest;

[Trait("Category","Unit")]
public class GetPatientTravelHistoryQueryHandlerTest
{
    private readonly IRepository<PatientTravelHistory,long> _patientTravelHistoryRepositoryMock
        = Substitute.For<IRepository<PatientTravelHistory,long>>();
    private readonly IRepository<Country> _countryRepositoryMock 
        = Substitute.For<IRepository<Country>>();
    private readonly IRepository<Region> _regionRepositoryMock
        = Substitute.For<IRepository<Region>>();
    private readonly IRepository<User,long> _userRepositoryMock
        = Substitute.For<IRepository<User,long>>();

    [Fact]
    public async Task Handler_With_Given_Valid_PatientId_Should_Returns_PatientDetailsQueryResponse()
    {
        //arrange
        _patientTravelHistoryRepositoryMock.GetAll().Returns(GetPatientTravelHistoryAsQueryable().BuildMock());
        _countryRepositoryMock.GetAll().Returns(GetCountries().BuildMock());
        _regionRepositoryMock.GetAll().Returns(GetRegions().BuildMock());
        _userRepositoryMock.GetAll().Returns(GetUsers().BuildMock());
        
        //act
        var handler = new GetPatientTravelHistoryQueryHandler(
            _patientTravelHistoryRepositoryMock,
            _countryRepositoryMock,
            _regionRepositoryMock,
            _userRepositoryMock);
        var result = await handler.Handle(1);
        
        //assert

        result.ShouldNotBeNull();
        result.LastCreatedBy.ShouldBe("Dr. Test");
        result.LastDateCreated.ShouldBe(DateTime.Today.AddDays(-3));
        result.PatientTravelHistory.Count.ShouldBe(2);
    }

    private IQueryable<User> GetUsers()
    {
        return new List<User>()
        {
            new ()
            {
                Id = 1,
                Title = TitleType.Dr,
                Name = "Test"
            }
        }.AsQueryable();
    }

    private IQueryable<Region> GetRegions()
    {
        return new List<Region>()
        {
            new ()
            {
                Id = 1,
                Name = "Region 1"
            },
            new ()
            {
                Id = 2,
                Name = "Region 2"
            }
        }.AsQueryable();
    }

    private IQueryable<Country> GetCountries()
    {
        return new List<Country>()
        {
            new Country( 
                "Nigeria",
                "234",
                "234",
                "NGN",
                "Naira",
                "NGN", 
                "₦")
            {
                Id = 1
            },
            new Country( 
                "Nigeria",
                "234",
                "234",
                "NGN",
                "Naira",
                "NGN", 
                "₦")
            {
                Id = 2
            }
            
        }.AsQueryable();
    }

    private IQueryable<PatientTravelHistory> GetPatientTravelHistoryAsQueryable()
    {
        return new List<PatientTravelHistory>()
        {
            new()
            {
                PatientId = 1,
                CountryId = 1,
                City = "Test City 1",
                Duration = 1,
                Date = DateTime.Today.AddDays(-6),
                CreatorUserId = 1,
                Interval = UnitOfTime.Month,
                CreationTime = DateTime.Today.AddDays(-6)
            },
            new()
            {
                PatientId = 1,
                CountryId = 2,
                City = "Test City 2",
                Duration = 3,
                Date = DateTime.Today.AddDays(-3),
                CreatorUserId = 1,
                Interval = UnitOfTime.Week,
                CreationTime = DateTime.Today.AddDays(-3)
            }
        }.AsQueryable();
    }
}
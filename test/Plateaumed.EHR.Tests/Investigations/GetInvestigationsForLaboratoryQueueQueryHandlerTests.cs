using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetInvestigationsForLaboratoryQueueQueryHandlerTests
    {
        private readonly IRepository<Patient, long> _patients = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<InvestigationRequest, long> _investigationRequest = Substitute.For<IRepository<InvestigationRequest, long>>();
        private readonly IRepository<InvestigationPricing, long> _investigationPricing = Substitute.For<IRepository<InvestigationPricing, long>>();
        private readonly IRepository<User, long> _users = Substitute.For<IRepository<User, long>>();
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappings = Substitute.For<IRepository<PatientCodeMapping, long>>();

        [Fact]
        public async Task HandleGetInvestigationsForLaboratoryQueueQueryHandlerShouldReturnAllItems()
        {
            _investigationRequest.GetAll().Returns(GetInvestigationRequests().BuildMock());
            _patients.GetAll().Returns(GetPatients().BuildMock());
            _investigationPricing.GetAll().Returns(GetInvestigationPricing().BuildMock());
            _users.GetAll().Returns(GetUsers().BuildMock());
            _patientCodeMappings.GetAll().Returns(GetPatientCodeMappings().BuildMock());

            var handler = new GetInvestigationsForLaboratoryQueueQueryHandler(_patients, _investigationRequest, _investigationPricing, _users, _patientCodeMappings);
            var request = new GetInvestigationsForLaboratoryQueueRequest 
            {
                InvestigationCategory = "",
                PatientName = "",
                OrderBy = "",
                Status = ""
            };

            var result = await handler.Handle(request);

            result.TotalCount.ShouldBe(3);
            result.Items.Count.ShouldBe(3);
        }

        [Fact]
        public async Task HandleGetInvestigationsForLaboratoryQueueQueryHandlerFilterByPatientNameShouldReturnSingleItem()
        {
            _investigationRequest.GetAll().Returns(GetInvestigationRequests().BuildMock());
            _patients.GetAll().Returns(GetPatients().BuildMock());
            _investigationPricing.GetAll().Returns(GetInvestigationPricing().BuildMock());
            _users.GetAll().Returns(GetUsers().BuildMock());
            _patientCodeMappings.GetAll().Returns(GetPatientCodeMappings().BuildMock());

            var handler = new GetInvestigationsForLaboratoryQueueQueryHandler(_patients, _investigationRequest, _investigationPricing, _users, _patientCodeMappings);
            var request = new GetInvestigationsForLaboratoryQueueRequest
            {
                InvestigationCategory = "",
                PatientName = "Test1",
                OrderBy = "",
                Status = ""
            };

            var result = await handler.Handle(request);

            result.TotalCount.ShouldBe(1);
            result.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task HandleGetInvestigationsForLaboratoryQueueQueryHandlerFilterByCategoryShouldReturnSingleItem()
        {
            _investigationRequest.GetAll().Returns(GetInvestigationRequests().BuildMock());
            _patients.GetAll().Returns(GetPatients().BuildMock());
            _investigationPricing.GetAll().Returns(GetInvestigationPricing().BuildMock());
            _users.GetAll().Returns(GetUsers().BuildMock());
            _patientCodeMappings.GetAll().Returns(GetPatientCodeMappings().BuildMock());

            var handler = new GetInvestigationsForLaboratoryQueueQueryHandler(_patients, _investigationRequest, _investigationPricing, _users, _patientCodeMappings);
            var request = new GetInvestigationsForLaboratoryQueueRequest
            {
                InvestigationCategory = "Chemistry",
                PatientName = "",
                OrderBy = "",
                Status = ""
            };

            var result = await handler.Handle(request);

            result.TotalCount.ShouldBe(1);
            result.Items.Count.ShouldBe(1);
        }


        private IQueryable<InvestigationRequest> GetInvestigationRequests()
        {
            return new List<InvestigationRequest>
            {
                new()
                {
                    Id = 1,
                    InvestigationId = 1,
                    TenantId = 1,
                    PatientId = 1,
                    CreatorUserId = 1,
                    Notes = "Test Investigation Price",
                    CreationTime = DateTime.Now,
                    Investigation = GetInvestigations().Where(x=>x.Id == 1).FirstOrDefault()
                },
                new()
                {
                    Id = 2,
                    InvestigationId = 2,
                    TenantId = 1,
                    PatientId = 2,
                    CreatorUserId = 1,
                    Notes = "Test Investigation Price2",
                    CreationTime = DateTime.Now,
                    Investigation = GetInvestigations().Where(x=>x.Id == 2).FirstOrDefault()
                },
                new()
                {
                    Id = 3,
                    InvestigationId = 3,
                    TenantId = 1,
                    PatientId = 3,
                    CreatorUserId = 1,
                    Notes = "Test Investigation Price3",
                    CreationTime = DateTime.Now,
                    Investigation = GetInvestigations().Where(x=>x.Id == 3).FirstOrDefault()
                }
            }.AsQueryable();
        }

        private IQueryable<Investigation> GetInvestigations()
        {
            return new List<Investigation>
            {
                new()
                {
                    Id = 1,
                    Name = "Blood Culture",
                    Type = "Microbiology"
                },
                new()
                {
                    Id = 2,
                    Name = "Full Blood Count (FBC)",
                    Type = "Haematology"
                },
                new()
                {
                    Id = 3,
                    Name = "Electrolytes",
                    Type = "Chemistry"
                }
            }.AsQueryable();
        }

        private IQueryable<Patient> GetPatients()
        {
            return new List<Patient>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Test1",
                    LastName = "Test_1",
                    MiddleName = "",
                    PatientCodeMappings = new List<PatientCodeMapping>
                    {
                        new PatientCodeMapping
                        {
                            PatientCode = $"1",
                            PatientId = 1,
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    FirstName = "Test2",
                    LastName = "Test_2",
                    MiddleName = "",
                    PatientCodeMappings = new List<PatientCodeMapping>
                    {
                        new PatientCodeMapping
                        {
                            PatientCode = $"12",
                            PatientId = 2,
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    FirstName = "Test3",
                    LastName = "Test_3",
                    MiddleName = "",
                    PatientCodeMappings = new List<PatientCodeMapping>
                    {
                        new PatientCodeMapping
                        {
                            PatientCode = $"123",
                            PatientId = 3,
                        }
                    }
                },


            }.AsQueryable();
        }

        private IQueryable<InvestigationPricing> GetInvestigationPricing()
        {
            return new List<InvestigationPricing>
            {
                new()
                {
                    Id = 1,
                    InvestigationId = 1,
                    TenantId = 1,
                    Amount = new Money
                    {
                        Amount = 100,
                        Currency = "GBP"
                    }
                },
                new()
                {
                    Id = 2,
                    InvestigationId = 2,
                    TenantId = 1,
                    Amount = new Money
                    {
                        Amount = 200,
                        Currency = "USD"
                    }
                },
                new()
                {
                    Id = 3,
                    InvestigationId = 3,
                    TenantId = 1,
                    Amount = new Money
                    {
                        Amount = 300,
                        Currency = "GBP"
                    }
                }

            }.AsQueryable();
        }

        private IQueryable<User> GetUsers()
        {
            return new List<User>
            {
                new()
                {
                    Id = 1,
                    Surname = "Test",
                    Name = "Tested",
                    TenantId = 1
                }
            }.AsQueryable();
        }

        private IQueryable<PatientCodeMapping> GetPatientCodeMappings()
        {
            return new List<PatientCodeMapping>
            {
                new()
                {
                    PatientCode = "1",
                    FacilityId =1,
                    PatientId =1,
                },
                new()
                {
                    PatientCode = "2",
                    FacilityId =1,
                    PatientId =2,
                },
                new()
                {
                    PatientCode = "3",
                    FacilityId =1,
                    PatientId =3,
                }
            }.AsQueryable();
        }
    }
}


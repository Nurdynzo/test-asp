using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.VitalSigns.Handlers;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.VitalSigns;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.VitalSigns
{
    public class GetGcsScoringQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenMissingPatient_ShouldThrowException()
        {
            // Arrange
            const int patientId = 1;
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(patientId).Returns(Task.FromResult((Patient)null));

            var scoringRepository = Substitute.For<IRepository<GCSScoring, long>>();
            var handler = new GetGcsScoringQueryHandler(scoringRepository, patientRepository, _objectMapper);
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(patientId));
            // Assert
            exception.Message.ShouldBe("Patient not found");
        }

        [Theory]
        [InlineData(0, new[] { 4, 2 })]
        [InlineData(1, new[] { 4, 2 })]
        [InlineData(2, new[] { 3, 1 })]
        [InlineData(4, new[] { 3, 1 })]
        [InlineData(5, new[] { 2, 6 })]
        [InlineData(10, new[] { 2, 6 })]
        public async Task Handle_GivenPatientAge_ShouldFilterGcsScoringRange(int age, int[] rangeScores)
        {
            // Arrange
            const int patientId = 1;
            var patient = new Patient { Id = patientId, DateOfBirth = DateTime.Now.AddYears(-age) };

            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(patientId).Returns(Task.FromResult(patient));

            var scoringRepository = Substitute.For<IRepository<GCSScoring, long>>();
            var gcsScorings = GetData();
            scoringRepository.GetAll().Returns(gcsScorings);

            var handler = new GetGcsScoringQueryHandler(scoringRepository, patientRepository, _objectMapper);
            // Act
            var response = await handler.Handle(patientId);
            // Assert
            response.ShouldNotBeNull();
            response[0].Ranges.Count.ShouldBe(rangeScores.Length);
            response[0].Ranges.Select(x => x.Score).ShouldBe(rangeScores);
        }

        private static IQueryable<GCSScoring> GetData()
        {
            return new List<GCSScoring>
            {
                new()
                {
                    Name = "Score 1",
                    Ranges = new List<GCSScoringRange>
                    {
                        new()
                        {
                            AgeMin = 0,
                            AgeMax = 2,
                            Score = 4
                        },
                        new()
                        {
                            AgeMin = 2,
                            AgeMax = 5,
                            Score = 3
                        },
                        new()
                        {
                            AgeMin = 5,
                            AgeMax = null,
                            Score = 2
                        },
                        new()
                        {
                            AgeMin = 0,
                            AgeMax = 2,
                            Score = 2
                        },
                        new()
                        {
                            AgeMin = 2,
                            AgeMax = 5,
                            Score = 1
                        },
                        new()
                        {
                            AgeMin = 5,
                            AgeMax = null,
                            Score = 6
                        },
                    },
                }
            }.AsQueryable().BuildMockDbSet();
        }
    }
}
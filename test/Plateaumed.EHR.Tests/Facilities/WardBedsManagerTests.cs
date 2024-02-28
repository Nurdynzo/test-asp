using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Facilities
{
    [Trait("Category", "Unit")]
    public class WardBedsManagerTests
    {
        [Fact]
        public async Task OccupyWardBed_Given_ValidInput_ShouldUpdate()
        {
            var wardBed = new WardBed
            {
                Id = 1,              
                PatientEncounter = new PatientEncounter
                {
                    Id = 1
                }
            };

            var repository = Substitute.For<IRepository<WardBed, long>>();
            repository.GetAsync(1).Returns(wardBed);           

            var handler = CreateWardBedsManager(repository);
            await handler.OccupyWardBed(1, 1);
            await repository.Received(1).UpdateAsync(wardBed);
            wardBed.EncounterId.ShouldBe(1);
        }

        [Fact]
        public async Task OccupyWardBed_Given_InvalidEncounterId_ShouldThrow()
        {
            var repository = Substitute.For<IRepository<WardBed, long>>();
            repository.GetAsync(1).Returns(GetWardBed());

            var handler = CreateWardBedsManager(repository);

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.OccupyWardBed(0, 1));
            //Assert
            exception.Message.ShouldBe("Invalid WardBed Id");
        }

        [Fact]
        public async Task OccupyWardBed_Given_InvalidWardBed_ShouldThrowError()
        {
            var repository = Substitute.For<IRepository<WardBed, long>>();
            repository.GetAsync(1).Returns(GetWardBed());

            var handler = CreateWardBedsManager(repository);

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.OccupyWardBed(1, 0));
            //Assert
            exception.Message.ShouldBe("Invalid EncounterId");
        }

        [Fact]
        public async Task DeOccupyWardBed_Given_ValidEcncounterId_ShouldUpdate()
        {
            var wardBed = new WardBed
            {
                Id = 1,
                EncounterId = 1,
                PatientEncounter = new PatientEncounter
                {
                    Id = 1
                }
            };

            var repository = Substitute.For<IRepository<WardBed, long>>();
            repository.GetAsync(1).Returns(wardBed);
            var handler = CreateWardBedsManager(repository);
            await handler.DeOccupyWardBed(1);
            await repository.Received(1).UpdateAsync(wardBed);
            wardBed.EncounterId.ShouldBe(null);
        }

        [Fact]
        public async Task DeOccupyWardBed_Given_InvalidEcncounterId_ShouldThrow()
        {
            var repository = Substitute.For<IRepository<WardBed, long>>();
            repository.Get(1).Returns(GetWardBed());

            var handler = CreateWardBedsManager(repository);

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.DeOccupyWardBed(0));
            //Assert
            exception.Message.ShouldBe($"WardBed with ID {0} not found");
        }        

        private static WardBedsManager CreateWardBedsManager(IRepository<WardBed, long> repository)
        {          
            var encounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            encounterRepository.GetAsync(1).Returns(new PatientEncounter()
            {
                Id = 1,
                PatientId = 1
            });
            return new WardBedsManager(repository, encounterRepository);
        }

        private static WardBed GetWardBed() =>
            new()
            {
                Id = 1,
                EncounterId = 1,
                PatientEncounter = new PatientEncounter
                {
                    Id = 1
                }
            };
    }
}


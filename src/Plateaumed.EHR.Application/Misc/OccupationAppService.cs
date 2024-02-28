

using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access occupations
    /// </summary>
    public class OccupationAppService : IOccupationAppService
    {
        private readonly IRepository<Occupation, long> _occupationRepository;

        /// <param name="occupationRepository"></param>
        public OccupationAppService(
            IRepository<Occupation, long> occupationRepository
        )
        {
            _occupationRepository = occupationRepository;
        }


        /// <inheritdoc/>
        public async Task<List<OccupationDto>> GetOccupations()
        {
            var occupations = await _occupationRepository.GetAll()
                .Select(x =>
                new OccupationDto { Id = x.Id, Name = x.Name }
                ).ToListAsync();

            return occupations;
        }
    }
}

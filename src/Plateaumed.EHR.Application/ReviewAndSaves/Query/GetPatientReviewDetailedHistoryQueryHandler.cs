using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Newtonsoft.Json;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Query
{
    /// <inheritdoc />
    public class GetPatientReviewDetailedHistoryQueryHandler : IGetPatientReviewDetailedHistoryQueryHandler
    {
        private readonly IRepository<PatientReviewDetailedHistory, long> _reviewDetailedHistoryRepository;
        private readonly IObjectMapper _objectMapper;

        public GetPatientReviewDetailedHistoryQueryHandler(IRepository<PatientReviewDetailedHistory, long> reviewDetailedHistoryRepository, IObjectMapper objectMapper)
        {
            _reviewDetailedHistoryRepository = reviewDetailedHistoryRepository;
            _objectMapper = objectMapper;
        }

        public async Task<PagedResultDto<ReviewDetailedHistoryReturnDto>> Handle(long patientId)
        {
            var filteredRecords = _reviewDetailedHistoryRepository
                .GetAll()
                .Include(u => u.Patient)
                .Include(u => u.UserFk)
                .Include(u => u.PatientEncounter)
                .Where(u => u.PatientId == patientId)
                .OrderByDescending(x=>x.CreationTime);

            var totalCount = await filteredRecords.CountAsync();

            var recordList = await filteredRecords.ToListAsync();

            var results = recordList.Select(rec =>
            {
                var patient = _objectMapper.Map<CreateOrEditPatientDto>(rec.Patient);
                var user = _objectMapper.Map<UserEditDto>(rec.UserFk);
                var getResponse = new ReviewDetailedHistoryReturnDto()
                {
                    Id = rec.Id,
                    CreationTime = rec.CreationTime,
                    Title = rec.Title,
                    ShortDescription = rec.ShortDescription,
                    FirstVisitNote = rec.Note != null
                        ? JsonConvert.DeserializeObject<FirstVisitNoteDto>(rec.Note)
                        : new FirstVisitNoteDto(),
                    IsAutoSaved = rec.IsAutoSaved
                };
                return getResponse;
            }).ToList();

            return new PagedResultDto<ReviewDetailedHistoryReturnDto>(totalCount, results);
        }
    }
}
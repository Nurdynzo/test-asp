using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateReviewOfSystemsDataRequestHandler : ICreateReviewOfSystemsDataRequestHandler
    {
        private readonly IRepository<ReviewOfSystem, long> _reviewOfSystemsRepository;
        private readonly IObjectMapper _mapper;
        public CreateReviewOfSystemsDataRequestHandler(IRepository<ReviewOfSystem, long> reviewOfSystemsRepository,
            IObjectMapper mapper)
        {
            _reviewOfSystemsRepository = reviewOfSystemsRepository;
            _mapper = mapper;
        }

        public async Task Handle(ReviewOfSystemsInputDto inputs)
        {
            var reviewOfSystemsData = inputs.ReviewOfSystemsInputs.Select(x => new ReviewOfSystem
            {
                PatientId = inputs.PatientId,
                Name = x.Name,
                Category = x.Category,
                SnomedId = x.SnomedId
            }).ToList();
            await _reviewOfSystemsRepository.InsertRangeAsync(reviewOfSystemsData);
        }
    }
}

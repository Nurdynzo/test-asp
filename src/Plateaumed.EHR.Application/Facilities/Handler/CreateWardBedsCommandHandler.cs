using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class CreateWardBedsCommandHandler : ICreateWardBedsCommandHandler
    {
        private readonly IRepository<WardBed, long> _wardBedRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;

        public CreateWardBedsCommandHandler(
          IRepository<WardBed, long> wardBedRepository,
          IObjectMapper objectMapper,
          IAbpSession abpSession
          )
        {
            _wardBedRepository = wardBedRepository;
            _objectMapper = objectMapper;
            _abpSession = abpSession;
        }

        public async Task Handle(CreateOrEditWardBedDto request)
        {
            if (request.Count <= 0) 
                throw new UserFriendlyException("Count must be greater than zero.");

            await InsertWardBeds(request);
        }

        private async Task InsertWardBeds(CreateOrEditWardBedDto request)
        {
            for (int i = 1; i <= request.Count; i++)
            {
                var wardBed = MapWardBed(request, i);

                await _wardBedRepository.InsertAsync(wardBed);
            }
        }

        private WardBed MapWardBed(CreateOrEditWardBedDto request, int bedNumber)
        {
            var wardBed = _objectMapper.Map<WardBed>(request);
            wardBed.BedNumber = $"{request.BedTypeName} {bedNumber}";

            if (_abpSession.TenantId != null)
                wardBed.TenantId = (int)_abpSession.TenantId; 

            return wardBed;
        }
    }
}

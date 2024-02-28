using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class CreateWardsCommandHandler : ICreateWardsCommandHandler
    {
        private readonly IRepository<Ward, long> _wardRepository;
        private readonly IRepository<BedType, long> _bedTypeRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;
        private readonly IUnitOfWorkManager _unitOfWork;

        public CreateWardsCommandHandler(IRepository<Ward, long> wardRepository,
            IRepository<BedType, long> bedTypeRepository,
            IObjectMapper objectMapper,
            IAbpSession abpSession, IUnitOfWorkManager unitOfWork)
        {
            _wardRepository = wardRepository;
            _bedTypeRepository = bedTypeRepository;
            _objectMapper = objectMapper;
            _abpSession = abpSession;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateOrEditWardDto request)
        {
            var ward = _objectMapper.Map<Ward>(request);

            if (_abpSession.TenantId != null)
            {
                ward.TenantId = _abpSession.TenantId.Value;
            }

            var wardBeds = new List<WardBed>();

            await MapBedTypes(request);

            if (request.WardBeds?.Count > 0)
            {
                foreach (var wardBedDto in request.WardBeds)
                {
                    var count = wardBedDto.Count;
                    if (count > 0)
                    {
                        for (var i = 0; i < count; i++)
                        {
                            var wardBed = MapWardBed(wardBedDto, request.FacilityId, i + 1);
                            wardBeds.Add(wardBed);
                        }
                    }
                }
            }

            ward.WardBeds = wardBeds;

            await _wardRepository.InsertAsync(ward);
        }

        private async Task MapBedTypes(CreateOrEditWardDto request)
        {
            foreach (var bedDto in request.WardBeds)
            {
                BedType bedType;
                if (bedDto.BedTypeId == null)
                {
                    if (await _bedTypeRepository.GetAll().AnyAsync(b =>
                            b.Name == bedDto.BedTypeName && b.FacilityId == request.FacilityId))
                        throw new UserFriendlyException("Bed type name already exists");

                    bedType = await _bedTypeRepository.InsertAsync(new BedType
                    {
                        Name = bedDto.BedTypeName,
                        TenantId = _abpSession.TenantId,
                        FacilityId = request.FacilityId,
                    });
                    await _unitOfWork.Current.SaveChangesAsync();
                    bedDto.BedTypeId = bedType.Id;
                }
                else
                {
                    bedType = await _bedTypeRepository.FirstOrDefaultAsync(bedDto.BedTypeId.Value) ??
                              throw new UserFriendlyException("Bed type does not exist");
                    bedType.Name = bedDto.BedTypeName;
                }
            }
        }
        
        private WardBed MapWardBed(CreateOrEditWardBedDto dto, long facilityId, int count)
        {
            var wardBed = _objectMapper.Map<WardBed>(dto);
            wardBed.BedNumber = $"{dto.BedTypeName} {count}";
            return wardBed;
        }
    }
}

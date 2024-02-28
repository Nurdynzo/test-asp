using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class UpdateWardsCommandHandler : IUpdateWardsCommandHandler
    {
        private readonly IRepository<Ward, long> _wardRepository;
        private readonly IRepository<BedType, long> _bedTypeRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;

        public UpdateWardsCommandHandler(
            IRepository<Ward, long> wardRepository,
            IRepository<BedType, long> bedTypeRepository,
            IObjectMapper objectMapper,
            IAbpSession abpSession)
        {
            _wardRepository = wardRepository;
            _bedTypeRepository = bedTypeRepository;
            _objectMapper = objectMapper;
           _abpSession = abpSession;
        }
        public async Task Handle(CreateOrEditWardDto request)
        {

            var ward = await _wardRepository.GetAllIncluding(x => x.WardBeds)
                           .FirstOrDefaultAsync(x => x.Id == request.Id)
                       ?? throw new UserFriendlyException("Ward does not exist");

            var wardBedsInput = request.WardBeds ?? new List<CreateOrEditWardBedDto>();

            ward.Name = request.Name;
            ward.Description = request.Description;
            ward.IsActive = request.IsActive;

            foreach (var wb in ward.WardBeds)
            {
                var matched = wardBedsInput.FirstOrDefault(i => wb.BedTypeId == i.BedTypeId);
                if (matched != null)
                {
                    wb.BedNumber = matched.Count.ToString();
                    wb.IsActive = matched.IsActive;
                    wardBedsInput.Remove(matched);
                }
                else
                {
                    ward.WardBeds.Remove(wb);
                }
            }

            if (wardBedsInput.Count > 0)
            {
                var wardBeds = wardBedsInput.Select(dto => MapWardBed(dto, request.FacilityId, wardBedsInput.Count));
                wardBeds.ToList().ForEach(w => ward.WardBeds.Add(w));
            }

            await _wardRepository.UpdateAsync(ward);
        }

        private WardBed MapWardBed(CreateOrEditWardBedDto dto, long facilityId, int count)
        {


            BedType bedType;
            if (dto.BedTypeId == null)
            {
                if (_bedTypeRepository.FirstOrDefault(b => b.Name == dto.BedTypeName && b.FacilityId == facilityId) != null)
                    throw new UserFriendlyException("Bed type name already exists");

                bedType = _bedTypeRepository.Insert(new BedType
                {
                    Name = dto.BedTypeName,
                    TenantId = _abpSession.TenantId,
                    FacilityId = facilityId,


                });
            }
            else
            {
                bedType = _bedTypeRepository.FirstOrDefault(dto.BedTypeId.Value);
            }


            var wardBed = _objectMapper.Map<WardBed>(dto);
            wardBed.BedNumber = $"{dto.BedTypeName} {count} ";
            wardBed.BedType = bedType ?? throw new UserFriendlyException("Bed type does not exist");

            return wardBed;
        }
    }
}

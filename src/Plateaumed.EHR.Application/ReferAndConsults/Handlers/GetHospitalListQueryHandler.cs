using System.Threading.Tasks;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Plateaumed.EHR.Facilities.Abstractions;
using Abp.ObjectMapping;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.ReferAndConsults.Handlers;

public class GetHospitalListQueryHandler : IGetHospitalListQueryHandler
{
    private readonly IGetAllFacilitiesQueryHandler _facilityQueryHandler;
    private readonly IObjectMapper _objectMapper;

    public GetHospitalListQueryHandler(
        IGetAllFacilitiesQueryHandler facilityQueryHandler,
            IObjectMapper objectMapper
        )
    {
        _facilityQueryHandler = facilityQueryHandler;
        _objectMapper = objectMapper;
    }

    public async Task<List<HospitalDto>> Handle()
    {
        var facilities = await _facilityQueryHandler.Handle();

        return facilities.Select(f => new HospitalDto()
        {
            FacilityId = f.Id,
            Name = f.Name,
            EmailAddress = f.EmailAddress,
            PhoneNumber = f.PhoneNumber,
            Website = f.Website,
            Address = f.Address,
            City = f.City
        }).ToList();
    }

}

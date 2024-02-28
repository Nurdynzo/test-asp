using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IPhysicalExaminationsAppService
{
    Task<GetPhysicalExaminationHeadersResponse> GetHeaders(GetPhysicalExaminationHeadersRequest request);
    Task<List<GetPhysicalExaminationListResponse>> GetList(GetPhysicalExaminationListRequest request);
    Task<GetPhysicalExaminationResponse> Get([Required] long id);
}
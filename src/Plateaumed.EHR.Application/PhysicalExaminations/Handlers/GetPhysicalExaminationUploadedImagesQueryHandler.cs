using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPhysicalExaminationUploadedImagesQueryHandler : IGetPhysicalExaminationUploadedImagesQueryHandler
{
    private readonly IRepository<PatientPhysicalExaminationImageFile, long> _repositoryPhysicalExaminaionImageQuery;



    public GetPhysicalExaminationUploadedImagesQueryHandler(
        IRepository<PatientPhysicalExaminationImageFile, long> repositoryPhysicalExaminaionImageQuery)
    {
        _repositoryPhysicalExaminaionImageQuery = repositoryPhysicalExaminaionImageQuery;
    }

    public async Task<List<UploadedImageDto>> Handle(long patientPhysicalExaminationId)
    {
        return await _repositoryPhysicalExaminaionImageQuery.GetAll()
           .Where(x => x.PatientPhysicalExaminationId == patientPhysicalExaminationId && x.IsDeleted == false)
           .OrderBy(v => v.CreationTime)
           .Select(v => new UploadedImageDto()
           {
               Id = v.PatientPhysicalExaminationId,
               FileName = v.FileName,
               FileUrl = v.FileUrl
           }).ToListAsync();
    }
}

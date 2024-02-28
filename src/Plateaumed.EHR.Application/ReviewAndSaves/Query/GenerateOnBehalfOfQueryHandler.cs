using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class GenerateOnBehalfOfQueryHandler : IGenerateOnBehalfOfQueryHandler
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQuery;
    private readonly IGetJobTitlesQueryHandler _jobTitleQueryHandler;

    public GenerateOnBehalfOfQueryHandler(IDoctorReviewAndSaveBaseQuery basedQuery,
        IGetJobTitlesQueryHandler jobTitleQueryHandler
        )
    {
        _basedQuery = basedQuery;
        _jobTitleQueryHandler = jobTitleQueryHandler;
    }

    public async Task<List<GetStaffMembersSimpleResponseDto>> Handle(long userId, long facilityId)
    {
        if (userId == 0)
            throw new UserFriendlyException("User Id is required.");


        if (facilityId == 0)
            throw new UserFriendlyException("Facility Id is required.");

        //Get the login staff profile and job details
        var staffInfo = await _basedQuery.GetStaffByUserId(userId);
        if (staffInfo.Jobs == null)
            throw new UserFriendlyException("No job profile found.");
        var job = staffInfo.Jobs.Where(j => j.FacilityId == facilityId).FirstOrDefault() ??
            throw new UserFriendlyException("No job profile found.");

        //Get all job titles
        var allJobTitles = await _jobTitleQueryHandler.Handle(new GetAllJobTitlesInput()
        {
            FacilityId = facilityId
        });

        //Filter the job titles to doctor and return the jobtitle id
        var jobTitleId = allJobTitles.Items
            .Where(s=>s.Name == StaticRoleNames.JobRoles.Doctor)
            .Select(x=>x.Id)
            .FirstOrDefault();

        if (job.UnitId == null)
            throw new UserFriendlyException("No unit found for this staff.");
        if (jobTitleId == 0)
            throw new UserFriendlyException("No doctor job title found.");

        //Get all available doctors within the same unit as the login staff
        var allDoctors = await _basedQuery.GetDoctorsByUnit((long)job.UnitId, jobTitleId);
        var staffs = allDoctors != null ? allDoctors.Select(staff => new GetStaffMembersSimpleResponseDto()
        {
            Id = staff.Id,
            Title = staff.Title,
            Name = staff.Name,
            MiddleName = staff.MiddleName,
            Surname = staff.Surname,
            StaffCode = staff.StaffCode
        }).ToList() : new List<GetStaffMembersSimpleResponseDto>();

        return staffs;
    }

}

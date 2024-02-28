using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class GetPatientVitalsSummaryQueryHandler : IGetPatientVitalsSummaryQueryHandler
{
    private readonly IRepository<PatientVital, long> _patientVitalRepository;
    private readonly IRepository<User,long> _userRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IAbpSession _abpSession;

    public GetPatientVitalsSummaryQueryHandler(
        IRepository<PatientVital, long> patientVitalRepository,
        IObjectMapper objectMapper,
        IAbpSession abpSession, IRepository<User, long> userRepository)
    {
        _patientVitalRepository = patientVitalRepository;
        _objectMapper = objectMapper;
        _abpSession = abpSession;
        _userRepository = userRepository;
    }
    
    public async Task<List<PatientVitalsSummaryResponseDto>> Handle(long patientId, long? procedureId = null, long? encounterId = null)
    {
        //this queries can still be optimized
        var dates = await _patientVitalRepository
            .GetAll()
            .IgnoreQueryFilters()
            .Where(hcv => hcv.PatientId == patientId && hcv.TenantId == _abpSession.TenantId)
            .WhereIf(encounterId.HasValue, v => v.EncounterId == encounterId.Value)
            .WhereIf(procedureId.HasValue, v => v.ProcedureId == procedureId.Value)
            .OrderByDescending(hcv => hcv.CreationTime) 
            .Select(hcv => new { DateCreated = hcv.CreationTime })
            .ToListAsync();
        
        dates.Sort((a, b) => DateTime.Compare(a.DateCreated, b.DateCreated));

        var vitals = await (from v in _patientVitalRepository
        .GetAll()
        .IgnoreQueryFilters()
        .Include(v => v.Patient)
        .Include(v => v.VitalSign)
        .Include(v => v.MeasurementSite)
        .Include(v => v.MeasurementRange)
        .Include(v => v.CreatorUser)
        .Include(v => v.LastModifierUser)
        join u in _userRepository.GetAll() on v.DeleterUserId equals u.Id into user
        from u in user.DefaultIfEmpty()
            where v.PatientId == patientId && v.TenantId == _abpSession.TenantId &&
                  (encounterId == null || v.EncounterId == encounterId) &&
                  (procedureId == null || v.ProcedureId == procedureId)
                  orderby v.CreationTime descending
            select new PatientVitalResponseDto
                {
                    Id = v.Id,
                    VitalSign = _objectMapper.Map<GetSimpleVitalSignsResponse>(v.VitalSign),
                    MeasurementSite = _objectMapper.Map<MeasurementSiteDto>(v.MeasurementSite),
                    MeasurementRange = _objectMapper.Map<MeasurementRangeDto>(v.MeasurementRange),
                    CreationTime = v.CreationTime,
                    LastModificationTime = v.LastModificationTime,
                    CreatorUser = _objectMapper.Map<GetStaffMembersSimpleResponseDto>(v.CreatorUser),
                    LastModifierUser = _objectMapper.Map<GetStaffMembersSimpleResponseDto>(v.LastModifierUser),
                    VitalReading = v.VitalReading,
                    PatientVitalType = v.PatientVitalType.ToString(),
                    Position = v.VitalPosition.ToString(),
                    ProcedureEntryType = v.ProcedureEntryType.ToString(),
                    IsDeleted = v.IsDeleted,
                    DeletedUser = u == null ? null : u.DisplayName,
                    PatientId = v.PatientId,
                    ProcedureId = v.ProcedureId,
                    MeasurementRangeId = v.MeasurementRangeId,
                    MeasurementSiteId = v.MeasurementSiteId,
                    VitalSignId = v.VitalSignId
                })
        .ToListAsync();
        
        var uniqueDates = new List<DateTime>();
        
        foreach (var date in dates)
            if (!IsDateInArray(date.DateCreated, uniqueDates))
                uniqueDates.Add(date.DateCreated);
        
        var data = new List<PatientVitalsSummaryResponseDto>();
        foreach (var date in uniqueDates)
        { 
            // Create a new instance
            var newData = new PatientVitalsSummaryResponseDto
            {
                Date = date,
                PatientVitals = new List<PatientVitalResponseDto>(),
                Time = date
            };

            for (int index = 0; index < vitals.Count; index++)
            { 
                var item = vitals[index];
                
                // Check if vitals belong to date and then append to the list
                DateTime vitalDate = item.CreationTime;
                if (date.Date == vitalDate.Date)
                {
                    if (date <= vitalDate && vitalDate <= date.AddSeconds(5))
                    {
                        // Set time range to 1 minute 
                        newData.Time = date;
                        newData.Date = date;

                        var patientVital = _objectMapper.Map<PatientVitalResponseDto>(item);
                        patientVital.OverThreshold = IsOverThreshold(patientVital.VitalSign.Sign, patientVital.VitalReading);
                        newData.PatientVitals.Add(patientVital); 
                    }
                }
            }

            if (newData.PatientVitals.Count > 0)
            {
                data.Add(newData);
            }
        }
        
        data.Sort((a, b) => DateTime.Compare(b.Time, a.Time));
        return data;
    }
 
    private bool IsDateInArray(DateTime date, List<DateTime> dates)
    {
        // return dates.Contains(date);
        string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss tt");
        return dates.Select(d => d.ToString("yyyy-MM-dd HH:mm:ss tt")).Contains(formattedDate);
    } 
    
    private bool IsOverThreshold(string vitalSign, double vitalReading)
    {
        var overThreshold = false;
        if (vitalSign.ToLower() == "Temperature".ToLower())
        {
            if (vitalReading > 38)
                overThreshold = true;
        }
        else if (vitalSign.ToLower() == "Resp rate".ToLower())
        {
            if (vitalReading < 12 || vitalReading > 25)
                overThreshold = true;
        }
        else if (vitalSign.ToLower() == "BP sys".ToLower())
        {
            if (vitalReading <= 90 || vitalReading >= 140)
                overThreshold = true;
        }
        else if (vitalSign.ToLower() == "BP dias".ToLower())
        {
            if (vitalReading <= 60 || vitalReading >= 90)
                overThreshold = true;
        }
        else if (vitalSign.ToLower() == "Heart Rate".ToLower())
        {
            if (vitalReading < 60 || vitalReading > 100)
                overThreshold = true;
        }
        else if (vitalSign.ToLower() == "SPO2".ToLower())
        {
            if (vitalReading < 60 || vitalReading > 100)
                overThreshold = true;
        }
        else if (vitalSign.ToLower() == "GCS".ToLower())
        {
            if (vitalReading < 9)
                overThreshold = true;
        }

        return overThreshold;
    } 
}

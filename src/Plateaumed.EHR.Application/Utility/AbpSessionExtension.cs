using Microsoft.AspNetCore.Http;

namespace Plateaumed.EHR.Utility;

public static class AbpSessionExtension
{
   
    
    public static long? GetFacilityId(this IHttpContextAccessor contextAccessor)
    {
        var facilityId =  contextAccessor.HttpContext?.User.FindFirst(AppConsts.FacilityId)?.Value;
        if (!string.IsNullOrEmpty(facilityId)) return long.Parse(facilityId);
        return null;
    }
}
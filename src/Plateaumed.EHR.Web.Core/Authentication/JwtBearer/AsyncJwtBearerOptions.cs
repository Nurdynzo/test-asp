using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Plateaumed.EHR.Web.Authentication.JwtBearer
{
    public class AsyncJwtBearerOptions : JwtBearerOptions
    {
        public readonly List<IAsyncSecurityTokenValidator> AsyncSecurityTokenValidators;
        
        private readonly EHRAsyncJwtSecurityTokenHandler _defaultAsyncHandler = new EHRAsyncJwtSecurityTokenHandler();

        public AsyncJwtBearerOptions()
        {
            AsyncSecurityTokenValidators = new List<IAsyncSecurityTokenValidator>() {_defaultAsyncHandler};
        }
    }

}

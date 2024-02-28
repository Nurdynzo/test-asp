using System.Collections.Generic;
using Abp.Application.Services;

namespace Plateaumed.EHR.Misc
{
    public interface ITitlesAppService: IApplicationService
    {

        public List<string> GetTitles();
    }
}

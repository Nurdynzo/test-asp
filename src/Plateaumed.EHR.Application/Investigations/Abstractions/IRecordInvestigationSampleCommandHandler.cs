﻿using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Abstractions
{
    public interface IRecordInvestigationSampleCommandHandler: ITransientDependency
    {
        Task Handle(RecordInvestigationSampleRequest request);
    }
}


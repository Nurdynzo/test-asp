﻿using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IUpdatePatientImplantCommandHandler : ITransientDependency
    {
        Task Handle(CreatePatientImplantCommandRequestDto request);
    }
}

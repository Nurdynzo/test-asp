﻿using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface ICreateFacilityBankCommandHandler : ITransientDependency
    {
        Task<FacilityBank> Handle(CreateOrEditBankRequest facilityBankDetailsDto);
    }
}

﻿using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Abstractions;

public interface IEditNextAppointmentCommandHandler : ITransientDependency
{
    Task<CreateNextAppointmentDto> Handle(CreateNextAppointmentDto requestDto, long loginUserId);
}
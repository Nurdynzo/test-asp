using System;
using Abp.Dependency;

namespace Plateaumed.EHR.DateUtils;

public interface IDateTimeProvider : ITransientDependency
{
    DateTime Now { get; }
}
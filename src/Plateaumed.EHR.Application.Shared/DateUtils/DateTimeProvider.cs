using System;

namespace Plateaumed.EHR.DateUtils;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}
using System;
using Abp.Dependency;

namespace Plateaumed.EHR.Utility;

public interface ILog : ITransientDependency
{
    void Error(Exception ex, string logId = "");

    void Info(object data, string message, string logId = "");

    void Write(string msg, string logId = "");
}
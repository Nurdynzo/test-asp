using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Plateaumed.EHR.Utility;

public class Log : ILog
{
    private readonly ILogger<Log> _logger;

    public Log(ILogger<Log> logger)
    {
        _logger = logger;
    }
    
    public void Error(Exception ex, string logId = "")
    {
        _logger.LogError($"{logId}::{DateTime.Now.ToString("dd|MMMM|yyyy|hh|mm|ss")} Message: {ex.Message} || {ex.InnerException?.Message} ");
        _logger.LogError($"{logId}::{DateTime.Now.ToString("dd|MMMM|yyyy|hh|mm|ss")} Message: {ex.StackTrace}");
    }

    public void Info(object data, string message, string logId = "")
    {
        _logger.LogInformation($"{logId}::{DateTime.Now.ToString("dd|MMMM|yyyy|hh|mm|ss")}||{message} || data=> {(data!=null ?JsonConvert.SerializeObject(data):"")}");
    }

    public void Write(string msg, string logId = "")
    {
        _logger.LogInformation($"{logId}::{DateTime.Now.ToString("dd|MMMM|yyyy|hh|mm|ss")}||{msg}");
    }
}
using System.Collections.Generic;
using Abp.Dependency;
using Microsoft.AspNetCore.Http;
namespace Plateaumed.EHR.CsvUpload.Abstraction
{
    public interface ICsvService : ITransientDependency
    {
        List<T> ProcessCsvFile<T>(IFormFile requestFormFile);
    }
}

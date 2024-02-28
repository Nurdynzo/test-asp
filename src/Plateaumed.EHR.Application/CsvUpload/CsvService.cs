using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Plateaumed.EHR.CsvUpload.Abstraction;
namespace Plateaumed.EHR.CsvUpload
{
    public class CsvService : ICsvService
    {
        public List<T> ProcessCsvFile<T>(IFormFile requestFormFile)
        {
            using var streamReader = new StreamReader(requestFormFile.OpenReadStream());
            using var csv = new CsvHelper.CsvReader(streamReader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<T>();
            var items = records.ToList();
            return items;
        }
    }
}

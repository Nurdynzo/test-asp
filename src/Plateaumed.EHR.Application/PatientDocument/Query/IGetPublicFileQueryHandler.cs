using System;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PatientDocument.Query;

public interface IGetPublicFileQueryHandler: ITransientDependency
{
    Task<string> Handle(string fileId);
}
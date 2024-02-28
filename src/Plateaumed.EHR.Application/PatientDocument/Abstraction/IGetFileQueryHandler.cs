using System;
using System.IO;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

/// <inheritdoc />
public interface IGetFileQueryHandler: ITransientDependency
{
    /// <summary>
    /// Download file by Id
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    Task<(Stream, string FileType)> Handle(Guid fileId);
}
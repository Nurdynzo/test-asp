using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IEmailStatementCommandHandler : ITransientDependency
{
    Task Handle(EmailStatementDto requestDto, IAbpSession abpSession);
}
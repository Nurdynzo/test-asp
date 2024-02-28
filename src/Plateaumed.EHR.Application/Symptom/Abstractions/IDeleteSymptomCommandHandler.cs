using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.Symptom.Abstractions;

public interface IDeleteSymptomCommandHandler : ITransientDependency
{
    Task<string> Handle(long symptomId, long? userId);
}
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.WoundDressing.Abstractions;

public interface IDeleteWoundDressingCommandHandler : ITransientDependency
{
    Task Handle(long woundDressingId);
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Feeding.Dtos;

namespace Plateaumed.EHR.Feeding.Abstractions;

public interface IGetPatientFeedingSummaryQueryHandler : ITransientDependency
{
    Task<List<FeedingSummaryForReturnDto>> Handle(int patientId);
}
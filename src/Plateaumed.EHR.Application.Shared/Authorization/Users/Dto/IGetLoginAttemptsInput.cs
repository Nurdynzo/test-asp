using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}
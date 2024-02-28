using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines
{
    [AbpAuthorize(AppPermissions.Pages_Medications)]
    public class VaccineAppService : EHRAppServiceBase, IVaccineAppService
    {
        private readonly IGetAllVaccinesQueryHandler _getAllVaccines;
        private readonly IGetVaccineQueryHandler _getVaccine;
        private readonly IGetAllVaccineGroupsQueryHandler _getAllVaccineGroups;
        private readonly IGetVaccineGroupQueryHandler _getVaccineGroup;
        private readonly ICreateVaccinationCommandHandler _createVaccinationCommandHandler;
        private readonly ICreateVaccinationHistoryCommandHandler _createVaccinationHistoryCommandHandler;
        private readonly IDeletePatientVaccinationCommandHandler _deletePatientVaccinationCommandHandler;
        private readonly IGetPatientVaccinationQueryHandler _getPatientVaccinationQueryHandler;
        private readonly IGetPatientVaccinationHistoryQueryHandler _getPatientVaccinationHistoryQueryHandler;

        public VaccineAppService(
            IGetAllVaccinesQueryHandler getAllVaccines,
            IGetVaccineQueryHandler getVaccine,
            IGetAllVaccineGroupsQueryHandler getAllVaccineGroups,
            IGetVaccineGroupQueryHandler getVaccineGroup,
            ICreateVaccinationCommandHandler createVaccinationCommandHandler,
            ICreateVaccinationHistoryCommandHandler createVaccinationHistoryCommandHandler,
            IDeletePatientVaccinationCommandHandler deletePatientVaccinationCommandHandler,
            IGetPatientVaccinationHistoryQueryHandler getPatientVaccinationHistoryQueryHandler,
            IGetPatientVaccinationQueryHandler getPatientVaccinationQueryHandler)
        {
            _getAllVaccines = getAllVaccines;
            _getVaccine = getVaccine;
            _getAllVaccineGroups = getAllVaccineGroups;
            _getVaccineGroup = getVaccineGroup;
            _createVaccinationCommandHandler = createVaccinationCommandHandler;
            _createVaccinationHistoryCommandHandler = createVaccinationHistoryCommandHandler;
            _deletePatientVaccinationCommandHandler = deletePatientVaccinationCommandHandler;
            _getPatientVaccinationHistoryQueryHandler = getPatientVaccinationHistoryQueryHandler;
            _getPatientVaccinationQueryHandler = getPatientVaccinationQueryHandler;
        }

        public async Task<List<GetAllVaccinesResponse>> GetAll()
        {
            return await _getAllVaccines.Handle();
        }

        public async Task<GetVaccineResponse> GetVaccine(EntityDto<long> request)
        {
            return await _getVaccine.Handle(request);
        }

        public async Task<List<GetAllVaccineGroupsResponse>> GetAllVaccineGroups()
        {
            return await _getAllVaccineGroups.Handle();
        }
        
        public async Task<GetVaccineGroupResponse> GetVaccineGroup(EntityDto<long> request)
        {
            return await _getVaccineGroup.Handle(request);
        }
        
        public async Task CreateVaccination(CreateMultipleVaccinationDto input) 
            => await _createVaccinationCommandHandler.Handle(input);
        
        public async Task CreateVaccinationHistory(CreateMultipleVaccinationHistoryDto input) 
            => await _createVaccinationHistoryCommandHandler.Handle(input);
        
        public async Task<List<VaccinationResponseDto>> GetPatientVaccination(EntityDto<long> patientId, long? procedureId = null) 
            => await _getPatientVaccinationQueryHandler.Handle(patientId, procedureId);
        public async Task DeleteVaccination(long id)
            => await _deletePatientVaccinationCommandHandler.Handle(id);
        
        public async Task<List<VaccinationHistoryResponseDto>> GetPatientVaccinationHistory(EntityDto<long> patientId) 
            => await _getPatientVaccinationHistoryQueryHandler.Handle(patientId); 
    }
}

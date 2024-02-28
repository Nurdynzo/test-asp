using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Dependency;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.Organizations;

namespace Plateaumed.EHR.NextAppointments.Abstractions;

public interface INextAppointmentBaseQuery : ITransientDependency
{
    Task<List<NextAppointmentUnitReturnDto>> GetAllPossibleUnitsAndClinics(long userId, long patientId, int? tenantId, long facilityId, long encounterId);
    Task<NextAppointmentReturnDto> GetNextAppointmentById(long nextAppointmentId);
    Task<List<NextAppointmentReturnDto>> GetNextAppointmentByDoctorId(long userId);
    Task<List<NextAppointmentReturnDto>> GetNextAppointmentByPatientId(long patientId);
    Task<List<NextAppointmentReturnDto>> GetDoctorAndPatientAppointment(long patientId, long doctorUserId);
    Task<List<OperationTimeDto>> GetOperationUnitTime(long unitId);
    Task<OrganizationUnitExtended> GetOrganizationUnit(long unitId);
    Task<StaffMemberJobDto> GetStaffFacilities(long doctorUserId, long facilityId);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;

namespace Plateaumed.EHR.Discharges.Handlers;
public class CreateDischargedPlanItemsHandler : ICreateDischargedPlanItemsHandler
{
    private readonly IRepository<DischargePlanItem, long> _dischargePlanItemRepository;
    private readonly PlanItems.Abstractions.IBaseQuery _planItemsQueryHandler;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAbpSession _abpSession;

    public CreateDischargedPlanItemsHandler(IUnitOfWorkManager unitOfWorkManager,
            IRepository<DischargePlanItem, long> dischargePlanItemRepository,
            PlanItems.Abstractions.IBaseQuery planItemsQueryHandler,
            IAbpSession abpSession)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _dischargePlanItemRepository = dischargePlanItemRepository;
        _planItemsQueryHandler = planItemsQueryHandler;
        _abpSession = abpSession;
    }
    public async Task<List<DischargePlanItemDto>> Handle(List<CreateDischargePlanItemDto> requestDto, long dischargeId, long patientId)
    {
        if (dischargeId == 0)
            throw new UserFriendlyException("Discharge Id is required.");

        if (patientId == 0)
            throw new UserFriendlyException("PatientId Id is required.");

        if (requestDto != null && requestDto.Count > 0)
        {
            var dischargePlanItem = new List<DischargePlanItem>();
            var planItems = new List<DischargePlanItemDto>();
            var patientPlanItems = _planItemsQueryHandler.GetPatientPlanItemsBaseQuery((int)patientId);
            foreach (var item in requestDto)
            {
                //Validate the item
                var planitem = patientPlanItems.Where(s => s.PatientId == patientId && s.Id == item.PlanItemId).FirstOrDefault();
                if (planitem != null && planitem.Id > 0)
                {
                    //Add item to list
                    dischargePlanItem.Add(
                        new DischargePlanItem
                        {
                            TenantId = _abpSession.TenantId.GetValueOrDefault(),
                            DischargeId = dischargeId,
                            PlanItemId = item.PlanItemId,
                            CreatorUserId = _abpSession.UserId,
                            CreationTime = DateTime.UtcNow
                        });
                    planItems.Add(
                        new DischargePlanItemDto
                        {
                            DischargeId = dischargeId,
                            PlanItemId = planitem.Id,
                            PatientId = planitem.PatientId,
                            Description = planitem.Description,
                            CreationTime = planitem.CreationTime,
                            DeletionTime = planitem.DeletionTime
                        });
                }
            }

            if (dischargePlanItem.Count > 0)
            {
                foreach (var item in dischargePlanItem)
                    await _dischargePlanItemRepository.InsertAsync(item);
                await _unitOfWorkManager.Current.SaveChangesAsync();
                return planItems;
            }
        }
        return null;
    }
}



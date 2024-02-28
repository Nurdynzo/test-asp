using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetInvestigationsForLaboratoryQueueQueryHandler : IGetInvestigationsForLaboratoryQueueQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<InvestigationRequest, long> _investigationRequest;
        private readonly IRepository<InvestigationPricing, long> _investigationPricing;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMapping;

        public GetInvestigationsForLaboratoryQueueQueryHandler(IRepository<Patient, long> patientRepository,
            IRepository<InvestigationRequest, long> investigationRequest,
            IRepository<InvestigationPricing, long> investigationPricing,
            IRepository<User, long> userRepository,
            IRepository<PatientCodeMapping, long> patientCodeMapping
            )
        {
            _patientRepository = patientRepository;
            _investigationRequest = investigationRequest;
            _investigationPricing = investigationPricing;
            _userRepository = userRepository;
            _patientCodeMapping = patientCodeMapping;
        }

        public async Task<PagedResultDto<InvestigationsForLaboratoryQueueResponse>> Handle(GetInvestigationsForLaboratoryQueueRequest request)
        {
            var category = !string.IsNullOrWhiteSpace(request.InvestigationCategory) ? request.InvestigationCategory.ToLower() : null;
            var status = !string.IsNullOrWhiteSpace(request.Status) ? request.Status.ToLower() : null;
            var sort = !string.IsNullOrWhiteSpace(request.OrderBy) ? request.OrderBy.ToLower() : "";
            var q1 = await (from p in _patientRepository.GetAll()
                            join i in _investigationRequest.GetAll().Include(x => x.Investigation) on p.Id equals i.PatientId
                            where category == null ? i.Investigation.Type != null : i.Investigation.Type.ToLower().Contains(category)
                            where status == null ? i.InvestigationStatus == null || i.InvestigationStatus != null : i.InvestigationStatus.ToString().ToLower().Contains(status)
                            join pc in _patientCodeMapping.GetAll() on p.Id equals pc.PatientId
                            join ipi in _investigationPricing.GetAll().AsNoTracking() on i.Investigation equals ipi.Investigation into iandip
                            from ip in iandip.DefaultIfEmpty()
                            join u in _userRepository.GetAll().AsNoTracking() on i.LastModifierUserId.HasValue ? i.LastModifierUserId : i.CreatorUserId.Value equals u.Id
                            select new
                            {
                                Patient = p,
                                InvestigationRequest = i,
                                InvestigationPrice = ip,
                                Requester = u,
                                PatientCode = pc
                            }).ToListAsync();

            q1 = sort.Contains(SortLaboratoryQueueLandingList.Earliest_Investigations) ?
               q1.OrderByDescending(x => x.InvestigationRequest.CreationTime).ToList() :
               q1.OrderBy(x => x.InvestigationRequest.CreationTime).ToList();

            var grouped = (from q in q1
                           group q by q.Patient.Id
                           into groupedResponse
                           select new
                           {
                               Patient = groupedResponse.Where(x => x.Patient.Id == groupedResponse.Key).Select(x => new PatientDetail
                               {
                                   PatientId = x.Patient.Id,
                                   FirstName = x.Patient.FirstName,
                                   LastName = x.Patient.LastName,
                                   MiddleName = x.Patient.MiddleName ?? "",
                                   PatientDisplayName = x.Patient.FirstName + " " + (!string.IsNullOrWhiteSpace(x.Patient.MiddleName) ? x.Patient.MiddleName + " " : string.Empty) + x.Patient.LastName,
                                   PatientImageUrl = x.Patient.PictureUrl ?? "",
                                   Age = $"{DateTime.Now.Year - x.Patient.DateOfBirth.Year}yrs",
                                   Gender = x.Patient.GenderType.ToString(),
                                   PatientCode = x.PatientCode.PatientCode ?? "",
                                   LastModifiedTime = x.InvestigationRequest.LastModificationTime ?? x.InvestigationRequest.CreationTime,
                                   CreationTime = x.InvestigationRequest.CreationTime
                               }).FirstOrDefault(),
                               Investigations = groupedResponse.Select(x => new InvestigationResponseList
                               {
                                   Amount = x.InvestigationPrice?.Amount?.ToMoneyDto(),
                                   InvestigationName = x.InvestigationRequest?.Investigation?.Name ?? "",
                                   Status = x.InvestigationRequest?.InvestigationStatus?.ToString() ?? "",
                                   Organism = x.InvestigationRequest.Investigation?.SpecificOrganism ?? "",
                                   Specimen = x.InvestigationRequest.Investigation?.Specimen ?? "",
                                   InvestigationNote = x.InvestigationRequest.Notes ?? "",
                                   InvestigationCategory = x.InvestigationRequest.Investigation?.Type ?? "",
                                   DateCreatedOrLastModified = x.InvestigationRequest.LastModificationTime ?? x.InvestigationRequest.CreationTime,
                                   InvestigationId = x.InvestigationRequest.InvestigationId,
                                   InvestigationRequestId = x.InvestigationRequest.Id,
                                   CreatorOrModifierInfo = new ModifierOrCreatorDetailDto()
                                   {
                                       Name = x.Requester.DisplayName ?? "",
                                       FirstName = x.Requester.Name,
                                       LastName =x.Requester.Surname,
                                       Title = x.Requester.Title.ToString() ?? "",
                                       Unit = "" //TODO: Link with Encounter Manager
                                   },
                               }).ToList(),
                           }).WhereIf(request.PatientName != null, x => x.Patient.PatientDisplayName.ToLower().Contains(request.PatientName.ToLower()))
                             .Select(x => new InvestigationsForLaboratoryQueueResponse
                             {
                                 InvestigationItems = x.Investigations,
                                 PatientDetail = x.Patient
                             }).ToList();

            var items = grouped.Skip(request.SkipCount)
                        .Take(request.MaxResultCount)
                        .ToList();

            return new PagedResultDto<InvestigationsForLaboratoryQueueResponse>(items.Count, items);
        }
    }
}
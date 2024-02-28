
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityBankInsuranceKYCDetails
    {
        public string Name { get; set; }

        public string BankName { get; set; }

        public string BankAccountHolder { get; set; }

        public string BankAccountNumber { get; set; }

        public List<CreateOrEditFacilityInsurerDto> Insurers { get; set; }

        public List<GetFacilityDocumentForViewDto> KYCDocuments { get; set; }
    }
}

using System;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class InvestigationResponseList
    {
        public string InvestigationName { get; set; }
        public string InvestigationNote { get; set; }
        public string Specimen { get; set; }
        public string Organism { get; set; }
        public MoneyDto Amount { get; set; }
        public DateTime DateCreatedOrLastModified { get; set; }
        public string InvestigationCategory { get; set; }
        public long InvestigationId { get; set; }
        public long InvestigationRequestId { get; set; }
        public ModifierOrCreatorDetailDto CreatorOrModifierInfo { get; set; }
        public string Status { get; set; }
    }
    
}


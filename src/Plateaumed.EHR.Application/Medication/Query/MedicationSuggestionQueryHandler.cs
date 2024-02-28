using System.Threading.Tasks;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Medication.Query;

public class MedicationSuggestionQueryHandler : IMedicationSuggestionQueryHandler
{
    
    public MedicationSuggestionQueryHandler()
    {
        
    }
    
    public MedicationSuggestionForReturnDto Handle()
    {
        var result = new MedicationSuggestionForReturnDto
        {
            Dose = MedicationSuggestionList.Dose,
            Unit = MedicationSuggestionList.Unit,
            Frequency = MedicationSuggestionList.Frequency,
            Direction = MedicationSuggestionList.Direction,
            Duration = MedicationSuggestionList.Duration
        };

        return result;
    } 
}

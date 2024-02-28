using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.ReferAndConsults.Query.BaseQueryHelper;

public class ReferAndConsultBasedQuery : IReferAndConsultBasedQuery
{

    public ReferAndConsultBasedQuery()
    {
    }

    public List<string> GenerateSummary(Patient patient, List<PatientSymptomSummaryForReturnDto> symptoms, long userId)
    {
        var summary = symptoms == null ? null : symptoms.Where(x => x.CreatorUserId == userId &&
                                                                    x.SymptomEntryType == Symptom.SymptomEntryType.Suggestion)
            .Select(s => s.SuggestionSummary).ToList();

        var allNotes = new List<string>();
        allNotes.Add($"We request that you kindly review {patient.DisplayName} who presented with a history of: ");
        foreach (var note in summary)
        {
            allNotes.Add(note);
        }
        return allNotes;
    }


}

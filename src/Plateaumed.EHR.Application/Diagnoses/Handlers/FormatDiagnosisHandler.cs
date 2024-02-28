using System.Collections.Generic;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses.Handlers
{
        public class FormatDiagnosisHandler : IFormatDiagnosisHandler
    {
            public string FormatDiagnoses(CreateDiagnosisDto createDiagnosis)
            {
                List<string> clinicalDiagnoses = new List<string>();
                List<string> differentialDiagnoses = new List<string>();

                foreach (var diagn in createDiagnosis.SelectedDiagnoses)
                {
                    if (diagn.Type == DiagnosisType.Clinical)
                    {
                        clinicalDiagnoses.Add(diagn.Name);
                    }
                    else if (diagn.Type == DiagnosisType.Differential)
                    {
                        differentialDiagnoses.Add(diagn.Name);
                    }
                }

                string clinicalDiagnosesText = string.Join(", ", clinicalDiagnoses);
                string differentialDiagnosesText = string.Join(", ", differentialDiagnoses);

                string combinedDiagnoses = clinicalDiagnosesText;

                if (!string.IsNullOrEmpty(differentialDiagnosesText))
                {
                    combinedDiagnoses += " R/O " + differentialDiagnosesText;
                }

                return combinedDiagnoses;
            }
        }

    
}

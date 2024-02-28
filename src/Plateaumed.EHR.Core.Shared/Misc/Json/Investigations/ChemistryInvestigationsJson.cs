namespace Plateaumed.EHR.Misc.Json.Investigations
{
    public class ChemistryInvestigationsJson
    {
        public static readonly string jsonData = /*lang=json*/ @"[
        {
            ""Name"": ""Electrolytes, Urea & Creatinine"",
            ""ShortName"": ""E/U/Cr"",
            ""SnomedId"": 444164000,
            ""Synonyms"": ""Measurement of urea, sodium, potassium, chloride, bicarbonate and creatinine"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Sodium"",
                    ""SnomedId"": ""39972003"",
                    ""Synonyms"": ""Measurement of urea, sodium, potassium, chloride, bicarbonate and creatinine"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 130,
                            ""MaxRange"": 146
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 130,
                            ""MaxRange"": 146
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Potassium"",
                    ""SnomedId"": ""59573005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3,
                            ""MaxRange"": 5
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 3,
                            ""MaxRange"": 5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Bicarbonate"",
                    ""SnomedId"": ""88645003"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 18,
                            ""MaxRange"": 30
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 18,
                            ""MaxRange"": 30
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Chloride"",
                    ""SnomedId"": ""46511006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 95,
                            ""MaxRange"": 110
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 95,
                            ""MaxRange"": 110
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Urea"",
                    ""SnomedId"": ""105010007"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 1.7,
                            ""MaxRange"": 8.4
                        }
                    ],
                    ""SummaryNotes"": ""Keep Urea and Urea (BUN) on the same card. Name should switch as unit changes"",
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Urea (BUN)"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": """",
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 4.8,
                            ""MaxRange"": 23.5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Creatinine"",
                    ""SnomedId"": ""70901006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 0.5,
                            ""MaxRange"": 1.2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": """",
                            ""MinRange"": 44.2,
                            ""MaxRange"": 132.6
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Bilirubin"",
            ""ShortName"": ""Total bilirubin"",
            ""SnomedId"": 166610007,
            ""Synonyms"": ""SB - Serum bilirubin"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Total Bilirubin"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""SB - Serum bilirubin"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""Other"": ""Premature infants, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 2
                        },
                        {
                            ""Other"": ""Premature infants, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 34
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 8
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 137
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 12
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 205
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 16
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 274
                        },
                        {
                            ""Other"": ""Neonates, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 2
                        },
                        {
                            ""Other"": ""Neonates, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 34
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 1.4,
                            ""MaxRange"": 8.7
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 24,
                            ""MaxRange"": 149
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 1.5,
                            ""MaxRange"": 12
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 26,
                            ""MaxRange"": 205
                        },
                        {
                            ""Other"": ""Children"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0.3,
                            ""MaxRange"": 1.2
                        },
                        {
                            ""Other"": ""Children"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 5,
                            ""MaxRange"": 12
                        },
                        {
                            ""Other"": ""Adults"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0.3,
                            ""MaxRange"": 1
                        },
                        {
                            ""Other"": ""Adults"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 3.42,
                            ""MaxRange"": 17.1
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Direct/Conjugated Bilirubin"",
                    ""SnomedId"": ""313856005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 3.42
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 0.1,
                            ""MaxRange"": 1
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Direct/conjugated bilirubin"",
            ""ShortName"": ""Direct/conjugated bilirubin"",
            ""SnomedId"": 313856005,
            ""Synonyms"": ""Serum conjugated bilirubin level"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Direct/Conjugated Bilirubin"",
                    ""SnomedId"": ""313856005"",
                    ""Synonyms"": ""Serum conjugated bilirubin level"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 3.42
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 0.1,
                            ""MaxRange"": 1
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""CSF analysis"",
            ""ShortName"": ""CSF analysis"",
            ""SnomedId"": 167727000,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Opening pressure"",
                    ""SnomedId"": ""167712000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Cerebrospinal fluid"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmH₂O"",
                            ""MinRange"": 70,
                            ""MaxRange"": 180
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Appearance"",
                    ""SnomedId"": ""365665000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 2,
                    ""Suggestions"": [
                        {
                            ""Result"": ""Clear"",
                            ""SnomedId"": ""167707001"",
                            ""Normal"": true
                        },
                        {
                            ""Result"": ""Turbid"",
                            ""SnomedId"": ""167710008""
                        },
                        {
                            ""Result"": ""Cloudy"",
                            ""SnomedId"": ""1145323003""
                        },
                        {
                            ""Result"": ""Blood-stained"",
                            ""SnomedId"": ""167708006""
                        },
                        {
                            ""Result"": ""Xanthochromic"",
                            ""SnomedId"": ""167709003""
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""RBC count"",
                    ""SnomedId"": ""104111000"",
                    ""Synonyms"": ""CSF cell count"",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""/mm3"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1
                        }
                    ],
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""WBC count"",
                    ""SnomedId"": ""104112007"",
                    ""Synonyms"": ""CSF cell count"",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""28"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""cells/μL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 30
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""cells/μL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 20
                        },
                        {
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""cells/μL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 10
                        },
                        {
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""cells/μL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Glucose"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 2.5,
                            ""MaxRange"": 4.4
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 50,
                            ""MaxRange"": 80
                        }
                    ],
                    ""SummaryNotes"": ""Normal levels are about 2/3 the concentration of blood glucose."",
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Total protein"",
                    ""SnomedId"": ""84130004"",
                    ""Synonyms"": null,
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 15,
                            ""MaxRange"": 45
                        },
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": 2,
                            ""AgeMaxUnit"": ""Month"",
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""g/L"",
                            ""MinRange"": 0.2,
                            ""MaxRange"": 1.1
                        },
                        {
                            ""AgeMin"": 2,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": 4,
                            ""AgeMaxUnit"": ""Month"",
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""g/L"",
                            ""MinRange"": 0.09,
                            ""MaxRange"": 0.78
                        },
                        {
                            ""AgeMin"": 4,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": 14,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""g/L"",
                            ""MinRange"": 0.09,
                            ""MaxRange"": 0.33
                        },
                        {
                            ""AgeMin"": 14,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 18,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""g/L"",
                            ""MinRange"": 0.17,
                            ""MaxRange"": 0.36
                        },
                        {
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""g/L"",
                            ""MinRange"": 0.15,
                            ""MaxRange"": 0.45
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Prealbumin"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 2,
                            ""MaxRange"": 7
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Albumin"",
                    ""SnomedId"": ""313773000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 56,
                            ""MaxRange"": 76
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Alpha1 globulin"",
                    ""SnomedId"": ""269906005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 2,
                            ""MaxRange"": 7
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Alpha2 globulin"",
                    ""SnomedId"": ""269906005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 4,
                            ""MaxRange"": 12
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Beta globulin"",
                    ""SnomedId"": ""269906005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 8,
                            ""MaxRange"": 18
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Gamma globulin"",
                    ""SnomedId"": ""269906005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 3,
                            ""MaxRange"": 12
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF Oligoclonal bands"",
                    ""SnomedId"": ""413017004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": null,
                            ""MinRange"": 0,
                            ""MaxRange"": 1
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""CSF chloride"",
                    ""SnomedId"": ""167742002"",
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 110,
                            ""MaxRange"": 125
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Urinalysis"",
            ""ShortName"": ""Urinalysis"",
            ""SnomedId"": 27171005,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Appearance"",
                    ""SnomedId"": ""365430005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 6,
                    ""Suggestions"": [
                        {
                            ""Result"": ""Yellow"",
                            ""SnomedId"": 46800005,
                            ""Normal"": true
                        },
                        {
                            ""Result"": ""Dark Yellow"",
                            ""SnomedId"": 720001001
                        },
                        {
                            ""Result"": ""Discoloured Urine"",
                            ""SnomedId"": 102867009
                        },
                        {
                            ""Result"": ""Porter coloured urine"",
                            ""SnomedId"": 720002008
                        },
                        {
                            ""Result"": ""Reddish colour urine"",
                            ""SnomedId"": 720003003
                        },
                        {
                            ""Result"": ""Turbid urine"",
                            ""SnomedId"": 167238004
                        },
                        {
                            ""Result"": ""Clear urine"",
                            ""SnomedId"": 167236000
                        },
                        {
                            ""Result"": ""Pale Urine"",
                            ""SnomedId"": 162135003
                        },
                        {
                            ""Result"": ""Cloudy Urine"",
                            ""SnomedId"": 7766007
                        },
                        {
                            ""Result"": ""Abnormal urine odor"",
                            ""SnomedId"": 8769003
                        },
                        {
                            ""Result"": ""Urine normal odor"",
                            ""SnomedId"": 13103009
                        },
                        {
                            ""Result"": ""Urine smell ammoniacal"",
                            ""SnomedId"": 167248002
                        },
                        {
                            ""Result"": ""Urine smell sweet"",
                            ""SnomedId"": 449151000124103
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Odour"",
                    ""SnomedId"": ""10579003"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 6,
                    ""Suggestions"": [
                        {
                            ""Result"": ""Urinoid"",
                            ""Normal"": true
                        },
                        { ""Result"": ""Sulfuric"" },
                        { ""Result"": ""Strong"" },
                        { ""Result"": ""Honey"" },
                        { ""Result"": ""Fruity/sweet"" },
                        { ""Result"": ""Fecal"" },
                        { ""Result"": ""Burnt sugar"" },
                        { ""Result"": ""Ammoniacal"" },
                        { ""Result"": ""Pungent or fetid"" },
                        { ""Result"": ""Onions, garlic, asparagus"" }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Specific Gravity (USG)"",
                    ""SnomedId"": ""252386004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": null,
                            ""MinRange"": 1.002,
                            ""MaxRange"": 1.035
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Osmolality (O)"",
                    ""SnomedId"": ""78888000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mOsm/kg"",
                            ""MinRange"": 50,
                            ""MaxRange"": 1200
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""pH"",
                    ""SnomedId"": ""271348005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": null,
                            ""MinRange"": 4.5,
                            ""MaxRange"": 8
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Protein"",
                    ""SnomedId"": ""270999004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/day"",
                            ""MinRange"": 0,
                            ""MaxRange"": 150
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Albumin"",
                    ""SnomedId"": ""271000000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/day"",
                            ""MinRange"": 0,
                            ""MaxRange"": 30
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Red blood cells"",
                    ""SnomedId"": ""104122001"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""cells/high-power field"",
                            ""MinRange"": 0,
                            ""MaxRange"": 5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""White blood cells"",
                    ""SnomedId"": ""104123006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""cells/high-power field"",
                            ""MinRange"": 0,
                            ""MaxRange"": 5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Glucose"",
                    ""SnomedId"": ""69376001"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Bilirubin"",
                    ""SnomedId"": ""252384001"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 3,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Urobilinogen"",
                    ""SnomedId"": ""75306008"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0.1,
                            ""MaxRange"": 1
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Ketones"",
                    ""SnomedId"": ""271347000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 3,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Nitrites"",
                    ""SnomedId"": 250419008,
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 3,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Casts"",
                    ""SnomedId"": ""167335004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Suggestions"": [
                        {
                            ""Result"": ""Cellular casts"",
                            ""SnomedId"": 250445004
                        },
                        {
                            ""Result"": ""Broad"",
                            ""SnomedId"": 102844007
                        },
                        {
                            ""Result"": ""Fatty"",
                            ""SnomedId"": 102841004
                        },
                        {
                            ""Result"": ""Granular"",
                            ""SnomedId"": 102843001
                        },
                        {
                            ""Result"": ""Waxy"",
                            ""SnomedId"": 102837003
                        },
                        {
                            ""Result"": ""Hyaline casts"",
                            ""SnomedId"": 167338002
                        }
                    ],
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Crystals"",
                    ""SnomedId"": ""167340007"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Suggestions"": [
                        {
                            ""Result"": ""Calcium oxalate crystal"",
                            ""SnomedId"": 167342004
                        },
                        {
                            ""Result"": ""Cysteine crystals"",
                            ""SnomedId"": 167345002
                        },
                        {
                            ""Result"": ""Leucine crystals"",
                            ""SnomedId"": 167346001
                        },
                        {
                            ""Result"": ""Phosphate crystals"",
                            ""SnomedId"": 167344003
                        },
                        {
                            ""Result"": ""Tyrosine crystals"",
                            ""SnomedId"": 167347005
                        },
                        {
                            ""Result"": ""Uric acid crystals"",
                            ""SnomedId"": 167343009
                        }
                    ],
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Urine proteins"",
            ""ShortName"": ""Urine proteins"",
            ""SnomedId"": 270999004,
            ""Synonyms"": ""Urine total protein level"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Protein"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Urine total protein level"",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/day"",
                            ""MinRange"": 0,
                            ""MaxRange"": 150
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Albumin"",
                    ""SnomedId"": ""271000000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/day"",
                            ""MinRange"": 0,
                            ""MaxRange"": 30
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Urine glucose"",
            ""ShortName"": ""Urine glucose"",
            ""SnomedId"": 30994003,
            ""Synonyms"": ""Urinary glucose"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Glucose"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Urinary glucose"",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 3,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 1,
                            ""MaxRange"": 15
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/24 hours"",
                            ""MinRange"": 0,
                            ""MaxRange"": 500
                        }
                    ],
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Urine electrolytes"",
            ""ShortName"": ""Urine electrolytes"",
            ""SnomedId"": 14830009,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Sodium"",
                    ""SnomedId"": ""104935006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/24hrs"",
                            ""MinRange"": 100,
                            ""MaxRange"": 260
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Chloride"",
                    ""SnomedId"": ""14663000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/24hrs"",
                            ""MinRange"": 80,
                            ""MaxRange"": 250
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Potassium"",
                    ""SnomedId"": ""49833001"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/24hrs"",
                            ""MinRange"": 25,
                            ""MaxRange"": 100
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Phosphate"",
                    ""SnomedId"": ""271262001"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/24hrs"",
                            ""MinRange"": 0.4,
                            ""MaxRange"": 1.3
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Calcium"",
                    ""SnomedId"": ""73668004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/24hrs"",
                            ""MinRange"": 100,
                            ""MaxRange"": 300
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Urine pregnancy test"",
            ""ShortName"": ""Urine pregnancy test"",
            ""SnomedId"": 167252002,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Human chorionic gonadotropin (hCG)"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 3,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Random blood glucose"",
            ""ShortName"": ""RBG"",
            ""SnomedId"": 271061004,
            ""Synonyms"": ""Random blood sugar"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Random glucose"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Random blood sugar"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 70,
                            ""MaxRange"": 140
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.9,
                            ""MaxRange"": 7.8
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Fasting blood glucose"",
            ""ShortName"": ""FBS"",
            ""SnomedId"": 271062006,
            ""Synonyms"": ""Fasting blood glucose"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Fasting glucose"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Fasting blood glucose"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 70,
                            ""MaxRange"": 100
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.9,
                            ""MaxRange"": 5.6
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Calcium"",
            ""ShortName"": ""Calcium"",
            ""SnomedId"": 271240001,
            ""Synonyms"": ""Serum calcium level"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Total Calcium"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Serum calcium level"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 2.1,
                            ""MaxRange"": 2.55
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 8,
                            ""MaxRange"": 10
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Ionized"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 1.4,
                            ""MaxRange"": 2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 5.6,
                            ""MaxRange"": 8
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""D-dimer Assay"",
            ""ShortName"": ""D-dimer"",
            ""SnomedId"": 70648006,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""D-dimer"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 0.5
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/ml"",
                            ""MinRange"": 220,
                            ""MaxRange"": 500
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Liver function test"",
            ""ShortName"": ""LFT"",
            ""SnomedId"": 26958001,
            ""Synonyms"": ""Hepatic function panel"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Total Bilirubin"",
                    ""SnomedId"": ""166610007"",
                    ""Synonyms"": ""Hepatic function panel"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""Other"": ""Premature infants, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 2
                        },
                        {
                            ""Other"": ""Premature infants, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 34
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 8
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 137
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 12
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 205
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 16
                        },
                        {
                            ""Other"": ""Premature infants"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 274
                        },
                        {
                            ""Other"": ""Neonates, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 2
                        },
                        {
                            ""Other"": ""Neonates, cord blood"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""0"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 34
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 1.4,
                            ""MaxRange"": 8.7
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 24,
                            ""MaxRange"": 149
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 1.5,
                            ""MaxRange"": 12
                        },
                        {
                            ""Other"": ""Neonates"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 26,
                            ""MaxRange"": 205
                        },
                        {
                            ""Other"": ""Children"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0.3,
                            ""MaxRange"": 1.2
                        },
                        {
                            ""Other"": ""Children"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 5,
                            ""MaxRange"": 12
                        },
                        {
                            ""Other"": ""Adults"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0.3,
                            ""MaxRange"": 1
                        },
                        {
                            ""Other"": ""Adults"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""μmol/L"",
                            ""MinRange"": 3.42,
                            ""MaxRange"": 17.1
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Direct/Conjugated Bilirubin"",
                    ""SnomedId"": ""313856005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": """",
                            ""MinRange"": 0,
                            ""MaxRange"": 6
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 0.3
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Total Protein"",
                    ""SnomedId"": ""270992008"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/L"",
                            ""MinRange"": 64,
                            ""MaxRange"": 83
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": """",
                            ""MinRange"": """",
                            ""MaxRange"": """"
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 6.4,
                            ""MaxRange"": 8.3
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Albumin"",
                    ""SnomedId"": ""26758005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/L"",
                            ""MinRange"": 32,
                            ""MaxRange"": 55
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 3.2,
                            ""MaxRange"": 5.5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Aspartate aminotransferase (AST)"",
                    ""SnomedId"": ""26091008"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 37
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Alkaline phosphatase (ALP)"",
                    ""SnomedId"": ""57056007"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 211
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""AST/ALT Ratio"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": """",
                            ""MinRange"": 0,
                            ""MaxRange"": 1
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""ɣ–glutamyl transpedtidase (GGT)"",
                    ""SnomedId"": ""313849004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 11,
                            ""MaxRange"": 55
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Lactate Dehydrogenase (LDH)"",
                    ""SnomedId"": ""313854008"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 53
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""PT"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""secs"",
                            ""MinRange"": 11,
                            ""MaxRange"": 15
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""PT/INR"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": """",
                            ""MinRange"": 0.8,
                            ""MaxRange"": 1.1
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Alanine aminotransferase (ALT)"",
                    ""SnomedId"": ""56935002"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 40
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Total protein"",
            ""ShortName"": ""Total protein"",
            ""SnomedId"": 270992008,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Total Protein"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/L"",
                            ""MinRange"": 64,
                            ""MaxRange"": 83
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": """",
                            ""MinRange"": """",
                            ""MaxRange"": """"
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 6.4,
                            ""MaxRange"": 8.3
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Lipid profile"",
            ""ShortName"": ""Lipid profile"",
            ""SnomedId"": 16254007,
            ""Synonyms"": ""Lipid panel"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Total Cholesterol"",
                    ""SnomedId"": ""121868005"",
                    ""Synonyms"": ""Lipid panel"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 100.5,
                            ""MaxRange"": 208.8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 2.6,
                            ""MaxRange"": 5.4
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""LDL Cholesterol"",
                    ""SnomedId"": ""113079009"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 150.8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 3.9
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""VLDL"",
                    ""SnomedId"": ""104585005"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 0,
                            ""MaxRange"": 50.2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1.3
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""HDL Cholesterol"",
                    ""SnomedId"": ""28036006"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 25.1,
                            ""MaxRange"": 67.4
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.65,
                            ""MaxRange"": 1.742
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Triglycerides"",
                    ""SnomedId"": ""14740000"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 39,
                            ""MaxRange"": 194.9
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.44,
                            ""MaxRange"": 2.2
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Thyroid panel"",
            ""ShortName"": ""Thyroid panel"",
            ""SnomedId"": 35650009,
            ""Synonyms"": ""Thyroid function test"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""TSH"",
                    ""SnomedId"": ""313440008"",
                    ""Synonyms"": ""Thyroid function test"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""uIU/mL"",
                            ""MinRange"": 0.45,
                            ""MaxRange"": 4.5
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Pregnant"",
                            ""Unit"": ""uIU/mL"",
                            ""MinRange"": 0.2,
                            ""MaxRange"": 3
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""μU/mL"",
                            ""MinRange"": 2,
                            ""MaxRange"": 10
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Free T4"",
                    ""SnomedId"": ""313837000"",
                    ""Synonyms"": ""TFT - Thyroid function test"",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/dL"",
                            ""MinRange"": 0.8,
                            ""MaxRange"": 1.8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Pregnant"",
                            ""Unit"": ""ng/dL"",
                            ""MinRange"": 0.5,
                            ""MaxRange"": 1.6
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""pmol/L"",
                            ""MinRange"": 9,
                            ""MaxRange"": 23
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Total T4"",
                    ""SnomedId"": ""313862000"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""μg/dL"",
                            ""MinRange"": 5,
                            ""MaxRange"": 12
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 57,
                            ""MaxRange"": 148
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mcg/dl"",
                            ""MinRange"": 5.4,
                            ""MaxRange"": 11.5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Total T3"",
                    ""SnomedId"": ""401105001"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/dL"",
                            ""MinRange"": 60,
                            ""MaxRange"": 180
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 0.9,
                            ""MaxRange"": 2.8
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Reverse T3"",
                    ""SnomedId"": ""51583006"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/dL"",
                            ""MinRange"": 10,
                            ""MaxRange"": 24
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Free T3"",
                    ""SnomedId"": ""104994008"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 1.4,
                            ""MaxRange"": 4.4
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Pregnant"",
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 2,
                            ""MaxRange"": 3.8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""pmol/L"",
                            ""MinRange"": 3.5,
                            ""MaxRange"": 7.5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""T3 Uptake/TBG"",
                    ""SnomedId"": ""39966003"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 150,
                            ""MaxRange"": 360
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Serum pregnancy test"",
            ""ShortName"": ""Serum hCG"",
            ""SnomedId"": 166434005,
            ""Synonyms"": ""Serum pregnancy test (B-human chorionic gonadotropin)"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""hCG"",
                    ""SnomedId"": ""166434005"",
                    ""Synonyms"": ""Serum pregnancy test (B-human chorionic gonadotropin)"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": null,
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 5
                        }
                    ],
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""CSF chloride"",
            ""ShortName"": ""CSF chloride"",
            ""SnomedId"": 167742002,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""CSF chloride"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""CSF"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 110,
                            ""MaxRange"": 125
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Creatinine clearance"",
            ""ShortName"": ""Creatinine clearance"",
            ""SnomedId"": 784580008,
            ""Synonyms"": ""Estimation of creatinine clearance"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Creatinine clearance"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Estimation of creatinine clearance"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""mL/min"",
                            ""MinRange"": 97,
                            ""MaxRange"": 137
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""mL/s"",
                            ""MinRange"": 1.6,
                            ""MaxRange"": 2.3
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": null,
                            ""Unit"": ""mL/min"",
                            ""MinRange"": 88,
                            ""MaxRange"": 128
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": null,
                            ""Unit"": ""mL/s"",
                            ""MinRange"": 1.5,
                            ""MaxRange"": 2.1
                        }
                    ],
                    ""SummaryNotes"": ""Male"",
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Estimated glomerular filtration rate"",
            ""ShortName"": ""eGFR"",
            ""SnomedId"": 80274001,
            ""Synonyms"": ""GFR - glomerular filtration rate"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""eGFR"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""GFR - glomerular filtration rate"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mL/min/1.73 m2"",
                            ""MinRange"": 90,
                            ""MaxRange"": 120
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""2hr post-prandial blood glucose"",
            ""ShortName"": ""2HPP blood glucose"",
            ""SnomedId"": 88856000,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""2HPP"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""2 hours post-food blood sugar level"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 70,
                            ""MaxRange"": 140
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.9,
                            ""MaxRange"": 7.8
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Oral glucose tolerance test"",
            ""ShortName"": ""OGTT"",
            ""SnomedId"": 113076002,
            ""Synonyms"": ""Glucose challenge test"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Fasting blood glucose"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Glucose challenge test"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 70,
                            ""MaxRange"": 110
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.9,
                            ""MaxRange"": 6.1
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""OGTT (1 hour)"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 70,
                            ""MaxRange"": 160
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.9,
                            ""MaxRange"": 8.9
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""OGTT (2 hours)"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 70,
                            ""MaxRange"": 155
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.9,
                            ""MaxRange"": 8.6
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""OGTT (3 hours)"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 70,
                            ""MaxRange"": 140
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.9,
                            ""MaxRange"": 7.8
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Glycated haemoglobin (HbA1c)"",
            ""ShortName"": ""HbA1c"",
            ""SnomedId"": 43396009,
            ""Synonyms"": ""HBA1c (hemoglobin A1c) level"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""HbA1C"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""HBA1c (hemoglobin A1c) level"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": null,
                            ""MaxRange"": 6.5
                        }
                    ],
                    ""SummaryNotes"": ""Prior reference range= 6.0 - 8.3"",
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Electrolytes"",
            ""ShortName"": ""Electrolytes"",
            ""SnomedId"": 20109005,
            ""Synonyms"": ""Serum electrolytes"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Sodium"",
                    ""SnomedId"": ""39972003"",
                    ""Synonyms"": ""Serum electrolytes"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 135,
                            ""MaxRange"": 145
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 135,
                            ""MaxRange"": 145
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Potassium"",
                    ""SnomedId"": ""59573005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 3.5,
                            ""MaxRange"": 5
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 3.5,
                            ""MaxRange"": 5
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Bicarbonate"",
                    ""SnomedId"": ""88645003"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 18,
                            ""MaxRange"": 30
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 18,
                            ""MaxRange"": 30
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Chloride"",
                    ""SnomedId"": ""46511006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 95,
                            ""MaxRange"": 110
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 95,
                            ""MaxRange"": 110
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Urea"",
            ""ShortName"": ""Urea"",
            ""SnomedId"": 273967009,
            ""Synonyms"": ""Serum urea level"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Urea"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Serum urea level"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 1.7,
                            ""MaxRange"": 8.4
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Urea (BUN)"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": """",
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 4.8,
                            ""MaxRange"": 23.5
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Creatinine"",
            ""ShortName"": ""Creatinine"",
            ""SnomedId"": 113075003,
            ""Synonyms"": ""Serum creatinine"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Creatinine"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Serum creatinine"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 0.5,
                            ""MaxRange"": 1.2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": """",
                            ""MinRange"": 44.2,
                            ""MaxRange"": 106.1
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Uric acid"",
            ""ShortName"": ""Uric acid"",
            ""SnomedId"": 275740009,
            ""Synonyms"": ""Serum uric acid"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Uric acid"",
                    ""SnomedId"": null,
                    ""Synonyms"": ""Serum uric acid"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 4,
                            ""MaxRange"": 8.5
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.24,
                            ""MaxRange"": 0.51
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 2.7,
                            ""MaxRange"": 7.3
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.16,
                            ""MaxRange"": 0.43
                        },
                        {
                            ""Gender"": null,
                            ""AgeMin"": 28,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 2.5,
                            ""MaxRange"": 5.5
                        },
                        {
                            ""Gender"": null,
                            ""AgeMin"": 28,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.12,
                            ""MaxRange"": 0.32
                        },
                        {
                            ""Gender"": null,
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""28"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 2,
                            ""MaxRange"": 6.2
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Uric acid"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/24hrs"",
                            ""MinRange"": 250,
                            ""MaxRange"": 750
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mmol/day"",
                            ""MinRange"": 1.48,
                            ""MaxRange"": 4.43
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Phosphorus"",
            ""ShortName"": ""Phosphorus"",
            ""SnomedId"": 8364005,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Phosphorus"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 3,
                            ""MaxRange"": 4.5
                        },
                        {
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.97,
                            ""MaxRange"": 1.45
                        },
                        {
                            ""AgeMin"": 28,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 4.5,
                            ""MaxRange"": 6.5
                        },
                        {
                            ""AgeMin"": 28,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 1.45,
                            ""MaxRange"": 2.1
                        },
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""28"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mg/dl"",
                            ""MinRange"": 4.3,
                            ""MaxRange"": 9.3
                        },
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""28"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 1.4,
                            ""MaxRange"": 3
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Magnesium"",
            ""ShortName"": ""Magnesium"",
            ""SnomedId"": 63571001,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Magnesium"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 1.3,
                            ""MaxRange"": 2.1
                        },
                        {
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.65,
                            ""MaxRange"": 1.05
                        },
                        {
                            ""AgeMin"": 28,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 1.4,
                            ""MaxRange"": 1.7
                        },
                        {
                            ""AgeMin"": 28,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.7,
                            ""MaxRange"": 0.85
                        },
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""28"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mEq/L"",
                            ""MinRange"": 1.4,
                            ""MaxRange"": 2
                        },
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""28"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""mmol/L"",
                            ""MinRange"": 0.7,
                            ""MaxRange"": 1
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Gamma glutamyl transferase"",
            ""ShortName"": ""GGT"",
            ""SnomedId"": 313849004,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""GGT"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 55
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Acid phosphatase"",
            ""ShortName"": ""Acid phosphatase"",
            ""SnomedId"": 271233002,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Total Acid Phosphatase"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng /mL"",
                            ""MinRange"": 2.5,
                            ""MaxRange"": 3.7
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""µg/L"",
                            ""MinRange"": 2.5,
                            ""MaxRange"": 3.7
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 3
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Prostatic Acid Phosphatase"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/ml"",
                            ""MinRange"": 0,
                            ""MaxRange"": 2.5
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Creatine kinase (CK-MB)"",
            ""ShortName"": ""CK-MB"",
            ""SnomedId"": 166672007,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""CK-MB"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 0,
                            ""MaxRange"": 4
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Amylase"",
            ""ShortName"": ""Amylase"",
            ""SnomedId"": 89659001,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Amylase"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""units/L"",
                            ""MinRange"": 30,
                            ""MaxRange"": 220
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Amylase"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Urine"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""units/L"",
                            ""MinRange"": 24,
                            ""MaxRange"": 400
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Cholinesterase"",
            ""ShortName"": ""Cholinesterase"",
            ""SnomedId"": 86761000,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Cholinesterase"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""U/mL"",
                            ""MinRange"": 8,
                            ""MaxRange"": 18
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""kU/L"",
                            ""MinRange"": 8,
                            ""MaxRange"": 18
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Hormone assay"",
            ""ShortName"": """",
            ""SnomedId"": 122445005,
            ""Synonyms"": ""Hormone measurement"",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Follicle Stimulating Hormone (FSH)"",
                    ""SnomedId"": ""273971007"",
                    ""Synonyms"": ""Hormone measurement"",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 1.37,
                            ""MaxRange"": 9.9
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 1.37,
                            ""MaxRange"": 9.9
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Ovulatory peak (Day 14)"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 6.17,
                            ""MaxRange"": 17.2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Ovulatory peak (Day 14)"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 6.17,
                            ""MaxRange"": 17.2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Luteal phase (Day 15-28)"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 1.09,
                            ""MaxRange"": 9.2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Luteal phase (Day 15-28)"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 1.09,
                            ""MaxRange"": 9.2
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Post-menopausal"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 19.3,
                            ""MaxRange"": 100.6
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Post-menopausal"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 19.3,
                            ""MaxRange"": 100.6
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 1.42,
                            ""MaxRange"": 15.4
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 1.42,
                            ""MaxRange"": 15.4
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Luteinizing Hormone (LH)"",
                    ""SnomedId"": ""69527006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 1.68,
                            ""MaxRange"": 15
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 1.68,
                            ""MaxRange"": 15
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Ovulatory peak (Day 14)"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 21.9,
                            ""MaxRange"": 56.6
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Ovulatory peak (Day 14)"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 21.9,
                            ""MaxRange"": 56.6
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Luteal phase (Day 15-28)"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 0.61,
                            ""MaxRange"": 16.3
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Luteal phase (Day 15-28)"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 0.61,
                            ""MaxRange"": 16.3
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Post-menopausal"",
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 14.2,
                            ""MaxRange"": 52.3
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Post-menopausal"",
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 14.2,
                            ""MaxRange"": 52.3
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""IU/L"",
                            ""MinRange"": 1.24,
                            ""MaxRange"": 7.8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""mIU/mL"",
                            ""MinRange"": 1.24,
                            ""MaxRange"": 7.8
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Estradiol (E2)"",
                    ""SnomedId"": ""166448005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)"",
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 20,
                            ""MaxRange"": 350
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)"",
                            ""Unit"": ""pmol/L"",
                            ""MinRange"": 73.43,
                            ""MaxRange"": 1284.97
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Ovulatory peak (Day 14)"",
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 150,
                            ""MaxRange"": 750
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Ovulatory peak (Day 14)"",
                            ""Unit"": ""pmol/L"",
                            ""MinRange"": 550.7,
                            ""MaxRange"": 2753.5
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Luteal phase (Day 15-28)"",
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 30,
                            ""MaxRange"": 450
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Luteal phase (Day 15-28)"",
                            ""Unit"": ""pmol/L"",
                            ""MinRange"": 110.14,
                            ""MaxRange"": 1652.1
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Post-menopausal"",
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 20
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Post-menopausal"",
                            ""Unit"": ""pmol/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 73.43
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 10,
                            ""MaxRange"": 50
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": null,
                            ""Unit"": ""pg/mL"",
                            ""MinRange"": 36.71,
                            ""MaxRange"": 183.57
                        },
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Progesterone"",
                    ""SnomedId"": ""270972001"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0,
                            ""MaxRange"": 50,
                            ""Unit"": ""ng/dl"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0,
                            ""MaxRange"": 0.5,
                            ""Unit"": ""ng/ml"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1.59,
                            ""Unit"": ""nmol/L"",
                            ""Other"": ""Follicular phase (Day 6-13 of menstrual cycle)""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 300,
                            ""MaxRange"": 2500,
                            ""Unit"": ""ng/dl"",
                            ""Other"": ""Luteal phase (Day 15-28)""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 3,
                            ""MaxRange"": 25,
                            ""Unit"": ""ng/ml"",
                            ""Other"": ""Luteal phase (Day 15-28)""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 9.54,
                            ""MaxRange"": 79.5,
                            ""Unit"": ""nmol/L"",
                            ""Other"": ""Luteal phase (Day 15-28)""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0,
                            ""MaxRange"": 40,
                            ""Unit"": ""ng/dl"",
                            ""Other"": ""Post-menopausal""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0,
                            ""MaxRange"": 0.4,
                            ""Unit"": ""ng/ml"",
                            ""Other"": ""Post-menopausal""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1.27,
                            ""Unit"": ""nmol/L"",
                            ""Other"": ""Post-menopausal""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""MinRange"": 10,
                            ""MaxRange"": 50,
                            ""Unit"": ""ng/dl"",
                            ""Other"": ""Male""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0.1,
                            ""MaxRange"": 0.5,
                            ""Unit"": ""ng/ml"",
                            ""Other"": ""Male""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 0.32,
                            ""MaxRange"": 1.59,
                            ""Unit"": ""nmol/L"",
                            ""Other"": ""Male""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 725,
                            ""MaxRange"": 22,
                            ""Unit"": ""ng/dl"",
                            ""Other"": ""Pregnancy""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 7.25,
                            ""MaxRange"": 229,
                            ""Unit"": ""ng/ml"",
                            ""Other"": ""Pregnancy""
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""MinRange"": 23.06,
                            ""MaxRange"": 728.23,
                            ""Unit"": ""nmol/L"",
                            ""Other"": ""Pregnancy""
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Prolactin"",
                    ""SnomedId"": ""313442000"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Non-pregnant Adult Female"",
                            ""Unit"": ""ng/ml"",
                            ""MinRange"": 3,
                            ""MaxRange"": 27
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Female"",
                            ""Other"": ""Pregnant Adult Female"",
                            ""Unit"": ""ng/ml"",
                            ""MinRange"": 20,
                            ""MaxRange"": 400
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": ""Male"",
                            ""Other"": ""Adult Male"",
                            ""Unit"": ""ng/ml"",
                            ""MinRange"": 3,
                            ""MaxRange"": 13
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Cortisol"",
                    ""SnomedId"": ""271535005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": 17,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": ""Morning (8a.m)"",
                            ""Unit"": ""mcg/dl"",
                            ""MinRange"": 5,
                            ""MaxRange"": 23
                        },
                        {
                            ""AgeMin"": 17,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": ""Morning (8a.m)"",
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 138,
                            ""MaxRange"": 635
                        },
                        {
                            ""AgeMin"": 16,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": ""Evening (4p.m)"",
                            ""Unit"": ""mcg/dl"",
                            ""MinRange"": 3,
                            ""MaxRange"": 13
                        },
                        {
                            ""AgeMin"": 16,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": ""Evening (4p.m)"",
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 83,
                            ""MaxRange"": 359
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 16,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": ""Morning (8a.m)"",
                            ""Unit"": ""mcg/dl"",
                            ""MinRange"": 3,
                            ""MaxRange"": 21
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 16,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": ""Morning (8a.m)"",
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 83,
                            ""MaxRange"": 579.31
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 16,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": ""Morning (8a.m)"",
                            ""Unit"": ""μg/dl"",
                            ""MinRange"": 3,
                            ""MaxRange"": 21
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 16,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": ""Evening (4p.m)"",
                            ""Unit"": ""mcg/dl"",
                            ""MinRange"": 3,
                            ""MaxRange"": 10
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 16,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": ""Evening (4p.m)"",
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 83,
                            ""MaxRange"": 275.86
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 16,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": ""Evening (4p.m)"",
                            ""Unit"": ""μg/dl"",
                            ""MinRange"": 3,
                            ""MaxRange"": 10
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": ""Neonates"",
                            ""Unit"": ""mcg/dl"",
                            ""MinRange"": 1,
                            ""MaxRange"": 24
                        }
                    ],
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Anti-mullerian Hormone (AMH)"",
                    ""SnomedId"": ""711358004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/ml"",
                            ""MinRange"": 2,
                            ""MaxRange"": 6.8
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""pmol/l"",
                            ""MinRange"": 14.28,
                            ""MaxRange"": 48.55
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Male Sex Hormones"",
            ""ShortName"": 271222000,
            ""SnomedId"": 271222000,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Dehydroepiandrosterone (DHEA)"",
                    ""SnomedId"": ""166468004"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": 24,
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 2500
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": 24,
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1990
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 2,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 3,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 630
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 2,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 3,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 850
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 4,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 5,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 950
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 4,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 5,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1030
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 7,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 60,
                            ""MaxRange"": 1930
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 7,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1790
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 7,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 9,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 100,
                            ""MaxRange"": 2080
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 7,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 9,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 140,
                            ""MaxRange"": 2350
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 10,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 11,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 320,
                            ""MaxRange"": 3080
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 10,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 11,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 430,
                            ""MaxRange"": 3780
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 12,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 13,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 570,
                            ""MaxRange"": 4100
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 12,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 13,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 890,
                            ""MaxRange"": 6210
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 14,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 15,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 930,
                            ""MaxRange"": 6040
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 14,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 15,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 1220,
                            ""MaxRange"": 7010
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 16,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 17,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 1170,
                            ""MaxRange"": 6520
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 16,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 17,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 1420,
                            ""MaxRange"": 9000
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 40,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 1330,
                            ""MaxRange"": 7780
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 40,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 1330,
                            ""MaxRange"": 7780
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 40,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 67,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 630,
                            ""MaxRange"": 4700
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 40,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": 67,
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""ng/L"",
                            ""MinRange"": 630,
                            ""MaxRange"": 4700
                        }
                    ],
                    ""SummaryNotes"": ""Varies by age and gender"",
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Testosterone"",
                    ""SnomedId"": ""270973006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/dL"",
                            ""MinRange"": 300,
                            ""MaxRange"": 1000
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 10,
                            ""MaxRange"": 35
                        }
                    ],
                    ""SummaryNotes"": ""Male"",
                    ""Type"": ""Chemistry""
                },
                {
                    ""Name"": ""Testosterone"",
                    ""SnomedId"": ""270973006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/dL"",
                            ""MinRange"": 15,
                            ""MaxRange"": 70
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""nmol/L"",
                            ""MinRange"": 0.5,
                            ""MaxRange"": 2.4
                        }
                    ],
                    ""SummaryNotes"": ""Female"",
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""C-Reactive Protein"",
            ""ShortName"": ""CRP"",
            ""SnomedId"": 135842001,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""C-Reactive Protein"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 8,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/dL"",
                            ""MinRange"": 0,
                            ""MaxRange"": 1
                        },
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mg/L"",
                            ""MinRange"": 0,
                            ""MaxRange"": 10
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        },
        {
            ""Name"": ""Prostate Specific Antigen"",
            ""ShortName"": ""PSA"",
            ""SnomedId"": 63476009,
            ""Synonyms"": """",
            ""Type"": ""Chemistry"",
            ""Components"": [
                {
                    ""Name"": ""Prostate Specific Antigen"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Serum"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""ng/ml"",
                            ""MinRange"": 0,
                            ""MaxRange"": 4
                        }
                    ],
                    ""Type"": ""Chemistry""
                }
            ]
        }]";
    }
}
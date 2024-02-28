namespace Plateaumed.EHR.Misc.Json.Investigations
{
    public class MicrobiologyInvestigationsJson
    {
        public static readonly string jsonData = /*lang=json*/ @"[
        {
            ""Name"": ""Blood culture"",
            ""SnomedId"": ""30088009"",
            ""Specimen"": ""Whole blood"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Stool MCS/culture"",
            ""SnomedId"": ""117028002"",
            ""Specimen"": ""Faecal sample"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Ova"",
                    ""SnomedId"": ""167640000"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Protozoa"",
                    ""SnomedId"": ""417396000"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Amoebae"",
                    ""SnomedId"": ""416145003"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Flagellates"",
                    ""SnomedId"": ""243652000"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Eosinophils"",
                    ""SnomedId"": ""365601007"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Macrophages"",
                    ""SnomedId"": ""58986001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Fat"",
                    ""SnomedId"": ""66187002"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Striped muscle fibres"",
                    ""SnomedId"": ""167632006"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Undigested food"",
                    ""SnomedId"": ""276391004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Crystals"",
                    ""SnomedId"": ""365687009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Mucus in feces"",
                    ""SnomedId"": ""271864008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Frothy stool"",
                    ""SnomedId"": ""449191000124109"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Acholic stool"",
                    ""SnomedId"": ""70396004"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Bright red feces"",
                    ""SnomedId"": ""449331000124107"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Dark stools"",
                    ""SnomedId"": ""35064005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Normal"",
                    ""SnomedId"": ""167605001"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Green"",
                    ""SnomedId"": ""167609007"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Meconium"",
                    ""SnomedId"": ""167608004"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Tarry"",
                    ""SnomedId"": ""269899009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Yellow"",
                    ""SnomedId"": ""167607009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Orange feces"",
                    ""SnomedId"": ""449281000124103"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Pale feces"",
                    ""SnomedId"": ""267056008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Red stools"",
                    ""SnomedId"": ""64412006"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Stool color abnormal"",
                    ""SnomedId"": ""271863002"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Mucus in stool"",
                    ""SnomedId"": ""271864008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Abnormal consistency of stool"",
                    ""SnomedId"": ""422784003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Change in stool consistency"",
                    ""SnomedId"": ""229209009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Creamy stool"",
                    ""SnomedId"": ""449201000124107"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Dry stool"",
                    ""SnomedId"": ""424278001"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Feces consistency: normal"",
                    ""SnomedId"": ""167614006"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""High vaginal swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=258521001"",
            ""Specimen"": ""High vaginal swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": true,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""High vaginal swab: fungal organism isolated"",
                    ""SnomedId"": ""168350006""
                },
                {
                    ""Result"": ""High vaginal swab: white cells seen"",
                    ""SnomedId"": ""168351005""
                },
                {
                    ""Result"": ""HVS culture - gardnerella vaginalis"",
                    ""SnomedId"": ""168349006""
                },
                {
                    ""Result"": ""Clue cells"",
                    ""SnomedId"": ""250440009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Trichomonas vaginalis cells"",
                    ""SnomedId"": ""168348003"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Vaginal swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=258520000"",
            ""Specimen"": ""Vaginal swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": true,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Clue cells"",
                    ""SnomedId"": ""250440009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Trichomonas vaginalis cells"",
                    ""SnomedId"": ""168348003"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""STD wet prep"",
            ""SnomedId"": ""34149001"",
            ""Specimen"": ""Vaginal swab, Urethral swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": true,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Clue cells"",
                    ""SnomedId"": ""250440009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Trichomonas vaginalis cells"",
                    ""SnomedId"": ""168348003"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Urine MCS/culture"",
            ""SnomedId"": ""117010004"",
            ""Specimen"": ""Mid-stream urine"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Crystals"",
                    ""SnomedId"": ""365688004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Urine casts"",
                    ""SnomedId"": ""5277004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Squamous cells"",
                    ""SnomedId"": ""80554009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Dark Yellow"",
                    ""SnomedId"": ""720001001"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Discoloured Urine"",
                    ""SnomedId"": ""102867009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Porter coloured urine"",
                    ""SnomedId"": ""720002008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Reddish colour urine"",
                    ""SnomedId"": ""720003003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Turbid urine"",
                    ""SnomedId"": ""167238004"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear urine"",
                    ""SnomedId"": ""167236000"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Pale Urine"",
                    ""SnomedId"": ""162135003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Cloudy Urine"",
                    ""SnomedId"": ""7766007"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Abnormal urine odor"",
                    ""SnomedId"": ""8769003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine normal odor"",
                    ""SnomedId"": ""13103009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine smell ammoniacal"",
                    ""SnomedId"": ""167248002"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine smell sweet"",
                    ""SnomedId"": ""449151000124103"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Pleural fluid MCS/culture"",
            ""SnomedId"": ""167944006"",
            ""Specimen"": ""Pleural fluid"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Clear"",
                    ""SnomedId"": ""281278005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""cloudy or turbid"",
                    ""SnomedId"": ""81858005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Ear swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=309166000"",
            ""Specimen"": ""Ear swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Eye swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=445160003"",
            ""Specimen"": ""Eye swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Nose swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=447339001"",
            ""Specimen"": ""Nose swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Cerebrospinal fluid MCS/culture"",
            ""SnomedId"": ""252399001"",
            ""Specimen"": ""CSF"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },                
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null,
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
                    ]
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
                    ]
                }
            ]
        },
        {
            ""Name"": ""Sputum MCS/culture"",
            ""SnomedId"": ""167995008"",
            ""Specimen"": ""Saliva"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""cloudy or turbid"",
                    ""SnomedId"": ""81858005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear"",
                    ""SnomedId"": ""281278005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Aspirate MCS/culture"",
            ""SnomedId"": ""61594008:123038009=119295008"",
            ""Specimen"": ""Abscess aspirate"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Cloudy or turbid"",
                    ""SnomedId"": ""81858005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear"",
                    ""SnomedId"": ""281278005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Peritoneal fluid MCS/culture |"",
            ""SnomedId"": ""61594008:123038009=168139001"",
            ""Specimen"": ""Peritoneal fluid"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""cloudy or turbid"",
                    ""SnomedId"": ""81858005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""bloody"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear"",
                    ""SnomedId"": ""281278005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Wound swab MCS/culture"",
            ""SnomedId"": ""401294003"",
            ""Specimen"": ""Wound swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear"",
                    ""SnomedId"": ""281278005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Body fluid MCS/culture"",
            ""SnomedId"": ""117027007"",
            ""Specimen"": ""Body fluid"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""cloudy or turbid"",
                    ""SnomedId"": ""81858005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear"",
                    ""SnomedId"": ""281278005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Throat swab MCS/culture"",
            ""SnomedId"": ""117015009"",
            ""Specimen"": ""Throat swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Mucus cells"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Yeast cells"",
                    ""SnomedId"": ""717159009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Medical check - blood group, genotype, urinalysis, stool analysis, Mantoux test"",
            ""SnomedId"": ""225886003"",
            ""Specimen"": ""Whole Blood"",
            ""Components"": [
                {
                    ""Name"": ""Blood Group"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": null,
                    ""Type"": ""Haematology"",
                    ""Suggestions"": [
                        {
                            ""Result"": ""A"",
                            ""SnomedId"": ""112144000""
                        },
                        {
                            ""Result"": ""AB"",
                            ""SnomedId"": ""165743006""
                        },
                        {
                            ""Result"": ""B"",
                            ""SnomedId"": ""112149005""
                        },
                        {
                            ""Result"": ""O"",
                            ""SnomedId"": ""58460004""
                        }
                    ]
                },
                {
                    ""Name"": ""Hb genotype"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 2,
                    ""Type"": ""Haematology"",
                    ""Suggestions"": [
                        {
                            ""Result"": ""Hb AA"",
                            ""SnomedId"": ""31299006""
                        },
                        {
                            ""Result"": ""Hb AS"",
                            ""SnomedId"": ""16402000""
                        },
                        {
                            ""Result"": ""Hb SS"",
                            ""SnomedId"": ""127040003""
                        },
                        {
                            ""Result"": ""Hb SC"",
                            ""SnomedId"": ""35434009""
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
                                {
                                    ""Result"": ""Onions, garlic, asparagus""
                                }
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
                    ""Name"": ""Stool MCS/culture"",
                    ""SnomedId"": ""117028002"",
                    ""Specimen"": ""Faecal sample"",
                    ""Microbiology"": {
                        ""SpecificOrganism"": null,
                        ""MethyleneBlueStain"": true,
                        ""GramStain"": true,
                        ""Culture"": true,
                        ""AntibioticSensitivityTest"": true,
                        ""Microscopy"": true,
                        ""NugentScore"": false,
                        ""CommonResults"": true,
                        ""Dipstick"": null
                    },
                    ""Suggestions"": [
                        {
                            ""Result"": ""Ova"",
                            ""SnomedId"": ""167640000"",
                            ""Category"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Striped muscle fibres"",
                            ""SnomedId"": ""167632006"",
                            ""Category"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Undigested starch"",
                            ""SnomedId"": ""276391004"",
                            ""Category"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Vegetable fibres"",
                            ""Category"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Crystals"",
                            ""SnomedId"": ""365687009"",
                            ""Category"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Fat"",
                            ""Category"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Mucus in feces"",
                            ""SnomedId"": ""271864008"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Frothy stool"",
                            ""SnomedId"": ""449191000124109"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Acholic stool"",
                            ""SnomedId"": ""70396004"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Dark stools"",
                            ""SnomedId"": ""35064005"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Red stools"",
                            ""SnomedId"": ""64412006"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Stool color abnormal"",
                            ""SnomedId"": ""271863002"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Mucus in stool"",
                            ""SnomedId"": ""271864008"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Abnormal consistency of stool"",
                            ""SnomedId"": ""422784003"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Change in stool consistency"",
                            ""SnomedId"": ""229209009"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Creamy stool"",
                            ""SnomedId"": ""449201000124107"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Dry stool"",
                            ""SnomedId"": ""424278001"",
                            ""Category"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""adult or larval worms visible"",
                            ""Category"": ""Macroscopy""
                        }
                    ],
                    ""Type"": ""Microbiology""
                },
                {
                    ""Name"": ""Mantoux/Heaf test"",
                    ""SnomedId"": ""424489006"",
                    ""Specimen"": ""Skin"",
                    ""Microbiology"": {
                        ""SpecificOrganism"": ""Mycobacterium tuberculosis | 113861009"",
                        ""MethyleneBlueStain"": false,
                        ""GramStain"": false,
                        ""Culture"": false,
                        ""AntibioticSensitivityTest"": false,
                        ""Microscopy"": false,
                        ""NugentScore"": false,
                        ""CommonResults"": false,
                        ""Dipstick"": null
                    },
                    ""Suggestions"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Indeterminate"" },
                        { ""Result"": ""Negative"" },
                        {
                            ""Result"": ""Induration (firmness) that occurs 48-72 h after placement of the test."",
                            ""Category"": ""Macroscopy""
                        }
                    ],
                    ""Type"": ""Microbiology""
                }
            ],
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Mantoux/Heaf test"",
            ""SnomedId"": ""424489006"",
            ""Specimen"": ""Skin"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Indeterminate"" },
                { ""Result"": ""Negative"" },
                {
                    ""Result"": ""Induration (firmness) that occurs 48-72 h after placement of the test."",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": ""Mycobacterium tuberculosis | 113861009"",
            ""Dipstick"": null
        },
        {
            ""Name"": ""Malaria parasite microscopy"",
            ""SnomedId"": ""372071003"",
            ""Specimen"": ""Whole blood"",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" }
            ],
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                {
                    ""Result"": ""P. falciparum"",
                    ""SnomedId"": ""30020004""
                },
                {
                    ""Result"": ""P. vivax"",
                    ""SnomedId"": ""74746009""
                },
                {
                    ""Result"": ""P. ovale"",
                    ""SnomedId"": ""18508006""
                },
                {
                    ""Result"": ""Red blood cells"",
                    ""SnomedId"": ""168130002"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Plasmodia trophozoites"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Gametocytes"",
                    ""Category"": ""Microscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": ""P. falciparum | 30020004"",
            ""Dipstick"": null
        },
        {
            ""Name"": ""Malaria parasite RDT"",
            ""SnomedId"": ""171140005"",
            ""Specimen"": ""Whole blood"",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" },
                { ""Result"": ""Indeterminate"" }
            ],
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": ""Plasmodium spp."",
            ""Dipstick"": null
        },
        {
            ""Name"": ""Urinalysis"",
            ""SnomedId"": ""27171005"",
            ""Specimen"": ""Urine"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Dark Yellow"",
                    ""SnomedId"": ""720001001"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Discoloured Urine"",
                    ""SnomedId"": ""102867009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Porter coloured urine"",
                    ""SnomedId"": ""720002008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Reddish colour urine"",
                    ""SnomedId"": ""720003003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Turbid urine"",
                    ""SnomedId"": ""167238004"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear urine"",
                    ""SnomedId"": ""167236000"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Pale Urine"",
                    ""SnomedId"": ""162135003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Cloudy Urine"",
                    ""SnomedId"": ""7766007"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Abnormal urine odor"",
                    ""SnomedId"": ""8769003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine normal odor"",
                    ""SnomedId"": ""13103009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine smell ammoniacal"",
                    ""SnomedId"": ""167248002"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine smell sweet"",
                    ""SnomedId"": ""449151000124103"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": [
                {
                    ""Parameter"": ""Leukocytes"",
                    ""Ranges"": [
                        {
                            ""Unit"": ""(+)"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""+"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""++"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""+++"",
                                    ""Order"": 5
                                }
                            ]
                        },
                        {
                            ""Unit"": ""WBC/μL"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""70"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""125"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""500"",
                                    ""Order"": 5
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Nitrite"",
                    ""Ranges"": [
                        {
                            ""Unit"": """",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""Pos"",
                                    ""Order"": 3
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Urobilinogen"",
                    ""Ranges"": [
                        {
                            ""Unit"": ""mg/dl"",
                            ""Results"": [
                                {
                                    ""Result"": ""0.1"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""1"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""2"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""4"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""8"",
                                    ""Order"": 5
                                }
                            ]
                        },
                        {
                            ""Unit"": ""μmol/L"",
                            ""Results"": [
                                {
                                    ""Result"": ""Normal"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""16"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""33"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""66"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""131"",
                                    ""Order"": 5
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Protein"",
                    ""Ranges"": [
                        {
                            ""Unit"": ""(+)"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""+"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""++"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""+++"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""++++"",
                                    ""Order"": 6
                                }
                            ]
                        },
                        {
                            ""Unit"": ""mg/dl"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""30"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""100"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""300"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""1000"",
                                    ""Order"": 6
                                }
                            ]
                        },
                        {
                            ""Unit"": ""g/L"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""0.3"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""1"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""3"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""10"",
                                    ""Order"": 6
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""pH"",
                    ""Ranges"": [
                        {
                            ""Unit"": """",
                            ""Results"": [
                                {
                                    ""Result"": ""5"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""6"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""6.5"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""7"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""7.5"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""8"",
                                    ""Order"": 6
                                },
                                {
                                    ""Result"": ""8.5"",
                                    ""Order"": 7
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Blood"",
                    ""Ranges"": [
                        {
                            ""Unit"": ""(+)"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Haemolysis trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""+"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""++"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""+++"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""Non Hemolysis +"",
                                    ""Order"": 6
                                },
                                {
                                    ""Result"": ""Non Hemolysis ++"",
                                    ""Order"": 7
                                }
                            ]
                        },
                        {
                            ""Unit"": ""RBC/μL"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""Haemolysis trace"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""25"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""80"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""200"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""10"",
                                    ""Order"": 6
                                },
                                {
                                    ""Result"": ""80"",
                                    ""Order"": 7
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Specific Gravity"",
                    ""Ranges"": [
                        {
                            ""Unit"": """",
                            ""Results"": [
                                {
                                    ""Result"": ""1"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""1.005"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""1.01"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""1.015"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""1.02"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""1.025"",
                                    ""Order"": 6
                                },
                                {
                                    ""Result"": ""1.03"",
                                    ""Order"": 7
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Ketone"",
                    ""Ranges"": [
                        {
                            ""Unit"": ""(+)"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""+/-"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""+"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""++"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""+++"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""++++"",
                                    ""Order"": 6
                                }
                            ]
                        },
                        {
                            ""Unit"": ""mg/dl"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""5"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""15"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""40"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""80"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""160"",
                                    ""Order"": 6
                                }
                            ]
                        },
                        {
                            ""Unit"": ""mmol/L"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""0.5"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""1.5"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""3.9"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""8"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""16"",
                                    ""Order"": 6
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Bilirubin"",
                    ""Ranges"": [
                        {
                            ""Unit"": """",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""+"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""++"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""+++"",
                                    ""Order"": 5
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Glucose"",
                    ""Ranges"": [
                        {
                            ""Unit"": ""(+)"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""+/-"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""+"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""++"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""+++"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""++++"",
                                    ""Order"": 6
                                }
                            ]
                        },
                        {
                            ""Unit"": ""mg/dl"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""100"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""250"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""500"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""1000"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""2000"",
                                    ""Order"": 6
                                }
                            ]
                        },
                        {
                            ""Unit"": ""mmol/L"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""5.5"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""14"",
                                    ""Order"": 3
                                },
                                {
                                    ""Result"": ""28"",
                                    ""Order"": 4
                                },
                                {
                                    ""Result"": ""55"",
                                    ""Order"": 5
                                },
                                {
                                    ""Result"": ""111"",
                                    ""Order"": 6
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Parameter"": ""Ascorbic Acid"",
                    ""Ranges"": [
                        {
                            ""Unit"": ""(+)"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""+"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""++"",
                                    ""Order"": 3
                                }
                            ]
                        },
                        {
                            ""Unit"": ""mg/dl"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""20"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""40"",
                                    ""Order"": 3
                                }
                            ]
                        },
                        {
                            ""Unit"": ""mmol/L"",
                            ""Results"": [
                                {
                                    ""Result"": ""Neg"",
                                    ""Order"": 1
                                },
                                {
                                    ""Result"": ""1.2"",
                                    ""Order"": 2
                                },
                                {
                                    ""Result"": ""2.4"",
                                    ""Order"": 3
                                }
                            ]
                        }
                    ]
                }
            ]
        },
        {
            ""Name"": ""Urine microscopy"",
            ""SnomedId"": ""127800008"",
            ""Specimen"": ""Urine"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Crystals"",
                    ""SnomedId"": ""365688004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Urine casts"",
                    ""SnomedId"": ""5277004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Squamous cells"",
                    ""SnomedId"": ""80554009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Dark Yellow"",
                    ""SnomedId"": ""720001001"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Discoloured Urine"",
                    ""SnomedId"": ""102867009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Porter coloured urine"",
                    ""SnomedId"": ""720002008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Reddish colour urine"",
                    ""SnomedId"": ""720003003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Turbid urine"",
                    ""SnomedId"": ""167238004"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Clear urine"",
                    ""SnomedId"": ""167236000"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Pale Urine"",
                    ""SnomedId"": ""162135003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Cloudy Urine"",
                    ""SnomedId"": ""7766007"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Abnormal urine odor"",
                    ""SnomedId"": ""8769003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine normal odor"",
                    ""SnomedId"": ""13103009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine smell ammoniacal"",
                    ""SnomedId"": ""167248002"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Urine smell sweet"",
                    ""SnomedId"": ""449151000124103"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Occult blood test"",
            ""SnomedId"": ""104435004"",
            ""Specimen"": ""Faecal sample"",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" }
            ],
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                {
                    ""Result"": ""+"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""++"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""+++"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Negative"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Catheter tip MCS/culture"",
            ""SnomedId"": ""61594008:123038009=119312009"",
            ""Specimen"": ""Catheter tip"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Pus cells"",
                    ""SnomedId"": ""250439007"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Endocervical swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=16215131000119100"",
            ""Specimen"": ""Endocervix swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Urethral swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=258530009"",
            ""Specimen"": ""Urethral swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Respiratory tract specimen MCS/culture"",
            ""SnomedId"": ""401288006"",
            ""Specimen"": ""Respiratory tract sample"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Pus cells"",
                    ""SnomedId"": ""250439007"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Tissue biopsy MCS/culture"",
            ""SnomedId"": ""61594008:123038009= 309074002"",
            ""Specimen"": ""Tissue biopsy"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Green"",
                    ""SnomedId"": ""54662009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Other swab MCS/culture"",
            ""SnomedId"": ""61594008:123038009=257261003"",
            ""Specimen"": ""Swab"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Seminal fluid analysis"",
            ""SnomedId"": ""29651008"",
            ""Specimen"": ""Seminal fluid"",
            ""Components"": [
                {
                    ""Name"": ""Macroscopy "",
                    ""Components"": [
                        {
                            ""Name"": ""Time produced""
                        },
                        {
                            ""Name"": ""Time received""
                        },
                        {
                            ""Name"": ""Time examined""
                        },
                        {
                            ""Name"": ""Abstinence"",
                            ""Ranges"": [ { ""Unit"": ""days"" } ]
                        },
                        {
                            ""Name"": ""Liquefaction "",
                            ""Ranges"": [ { ""Unit"": ""minutes"" } ]
                        },
                        {
                            ""Name"": ""Interval between start of ejaculation and analysis (before liquifaction)"",
                            ""Ranges"": [ { ""Unit"": ""minutes"" } ]
                        },
                        {
                            ""Name"": ""Method of collection"",
                            ""Suggestions"": [
                                {
                                    ""Result"": ""Masturbation"",
                                    ""SnomedId"": ""17704007""
                                },
                                {
                                    ""Result"": ""Microsurgical epididymal sperm aspiration (MESA)"",
                                    ""SnomedId"": ""236364009""
                                },
                                {
                                    ""Result"": ""Testicular sperm aspiration (TESA)"",
                                    ""SnomedId"": ""236365005""
                                }
                            ]
                        },
                        {
                            ""Name"": ""Appearance"",
                            ""Suggestions"": [
                                {
                                    ""Result"": ""Homogenous grey opalescent"",
                                    ""SnomedId"": ""89042005""
                                },
                                {
                                    ""Result"": ""Reddish-brownish"",
                                    ""SnomedId"": ""57382009""
                                },
                                {
                                    ""Result"": ""Yellow"",
                                    ""SnomedId"": ""90998002""
                                }
                            ]
                        },
                        {
                            ""Name"": ""Viscosity "",
                            ""Suggestions"": [
                                {
                                    ""Result"": ""Low"",
                                    ""SnomedId"": ""62482003""
                                },
                                {
                                    ""Result"": ""Moderate"",
                                    ""SnomedId"": ""6736007""
                                },
                                {
                                    ""Result"": ""High"",
                                    ""SnomedId"": ""75540009""
                                }
                            ]
                        },
                        {
                            ""Name"": ""Volume"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""ml"",
                                    ""MinRange"": 1.5
                                }
                            ]
                        },
                        {
                            ""Name"": ""pH"",
                            ""Ranges"": [
                                {
                                    ""Unit"": """",
                                    ""MinRange"": 7.2
                                }
                            ]
                        },
                        {
                            ""Name"": ""Odour"",
                            ""Suggestions"": [
                                {
                                    ""Result"": ""Normal""
                                },
                                {
                                    ""Result"": ""Foul""
                                }
                            ]
                        },
                        {
                            ""Name"": ""Fructose "",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""μg/ml"",
                                    ""MinRange"": 1200,
                                    ""MaxRange"": 4500
                                }
                            ],
                            ""Results"": [
                                {
                                    ""Result"": ""Present""
                                },
                                {
                                    ""Result"": ""Absent""
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Name"": ""Total counts"",
                    ""Components"": [
                        {
                            ""Name"": ""Sperm Concentration"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""x10⁶/ml"",
                                    ""MinRange"": 15
                                }
                            ]
                        },
                        {
                            ""Name"": ""Total Sperm Count"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""x 10^6/ejaculate"",
                                    ""MinRange"": 39
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Name"": ""Morphology"",
                    ""Components"": [
                        {
                            ""Name"": ""Normal"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""%"",
                                    ""MinRange"": 4
                                }
                            ]
                        },
                        {
                            ""Name"": ""Abnormal"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Head Defects"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Neck Defects"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Tail Defects"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Cytoplasmic Droplets"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Headless (Pin-Head)"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Teratozoospermia Index"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""%"",
                                    ""MinRange"": 1.6,
                                    ""MaxRange"": 1.6
                                }
                            ]
                        }
                    ]
                },
                {
                    ""Name"": ""Motility"",
                    ""Components"": [
                        {
                            ""Name"": ""Total Mobile Spermatozoa"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""%"",
                                    ""MinRange"": 40
                                }
                            ]
                        },
                        {
                            ""Name"": ""Progressive Motility"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""%"",
                                    ""MinRange"": 32
                                }
                            ]
                        },
                        {
                            ""Name"": ""Non-Progressive"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Immotile"",
                            ""Ranges"": [ { ""Unit"": ""%"" } ]
                        },
                        {
                            ""Name"": ""Agglutination"",
                            ""Results"": [
                                {
                                    ""Result"": ""Present""
                                },
                                {
                                    ""Result"": ""Absent""
                                }
                            ]
                        },
                        {
                            ""Name"": ""Absolute Sperm Yield"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""million per ejaculation"",
                                    ""MaxRange"": 1,
                                    ""Other"": ""ICSI""
                                },
                                {
                                    ""Unit"": ""million per ejaculation"",
                                    ""MinRange"": 1,
                                    ""MaxRange"": 2,
                                    ""Other"": ""IVF""
                                },
                                {
                                    ""Unit"": ""million per ejaculation"",
                                    ""MinRange"": 2,
                                    ""MaxRange"": 5,
                                    ""Other"": ""IUI/IVF""
                                },
                                {
                                    ""Unit"": ""million per ejaculation"",
                                    ""MinRange"": 5,
                                    ""Other"": ""IUI""
                                }
                            ]
                        },
                        {
                            ""Name"": ""Sperm Vitality"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""%"",
                                    ""MinRange"": 58,
                                    ""MaxRange"": 100
                                }
                            ]
                        },
                        {
                            ""Name"": ""Round cell"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""per HPF"",
                                    ""MaxRange"": 5
                                }
                            ]
                        },
                        {
                            ""Name"": ""Pus Cells"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""x10⁶/ml"",
                                    ""MaxRange"": 1
                                }
                            ]
                        },
                        {
                            ""Name"": ""Red Cells"",
                            ""Ranges"": [
                                {
                                    ""Unit"": ""x10⁶/ml"",
                                    ""MaxRange"": 1
                                }
                            ]
                        }
                    ]
                }
            ],
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Normal study"",
                    ""SnomedId"": ""167768009""
                },
                {
                    ""Result"": ""Aspermia: absence of semen"",
                    ""SnomedId"": ""448921000""
                },
                {
                    ""Result"": ""Azoospermia: absence of sperm"",
                    ""SnomedId"": ""48188009""
                },
                {
                    ""Result"": ""Hypospermia: low semen volume"",
                    ""SnomedId"": null
                },
                {
                    ""Result"": ""Hyperspermia: high semen volume"",
                    ""SnomedId"": null
                },
                {
                    ""Result"": ""Oligozoospermia: very low sperm count"",
                    ""SnomedId"": ""88311004""
                },
                {
                    ""Result"": ""Asthenozoospermia: poor sperm motility"",
                    ""SnomedId"": ""24463005""
                },
                {
                    ""Result"": ""Teratozoospermia: sperm carry more morphological defects than usual"",
                    ""SnomedId"": ""236817003""
                },
                {
                    ""Result"": ""Necrozoospermia: all sperm in the ejaculate is dead"",
                    ""SnomedId"": null
                },
                {
                    ""Result"": ""Leucospermia: a high level of white blood cells in semen"",
                    ""SnomedId"": null
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Semen culture"",
            ""SnomedId"": ""80731003"",
            ""Specimen"": ""Seminal fluid"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": true,
                ""GramStain"": true,
                ""Culture"": true,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": true
            },
            ""Suggestions"": [
                {
                    ""Result"": ""Tumour cells"",
                    ""SnomedId"": ""252987004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""No bacteria seen"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bacteria cells"",
                    ""SnomedId"": ""710954001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Bloody"",
                    ""SnomedId"": ""307493003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Foul odour"",
                    ""SnomedId"": ""63225009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""No odour"",
                    ""SnomedId"": ""103371008"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Sputum acid-fast bacilli"",
            ""SnomedId"": ""117612008"",
            ""Specimen"": ""Sputum sample"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                { ""Result"": ""10/F"" },
                { ""Result"": ""1 - 10/F"" },
                { ""Result"": ""10 - 100/100F"" },
                { ""Result"": ""1 - 9/100F"" },
                { ""Result"": ""Negative"" }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""GeneXpert MTB/RIF assay"",
            ""SnomedId"": ""122384004"",
            ""Specimen"": ""Sputum"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                { ""Result"": ""MTB not detected"" },
                { ""Result"": ""MTB test invalid"" },
                { ""Result"": ""MTB detected"" },
                { ""Result"": ""RIF resistance not detected"" },
                { ""Result"": ""RIF resistance indeterminate"" },
                { ""Result"": ""RIF resistance detected"" }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": ""Mycobacterium tuberculosis | 113861009"",
            ""Dipstick"": null
        },
        {
            ""Name"": ""Ova and parasites"",
            ""SnomedId"": ""104223001"",
            ""Specimen"": ""Stool"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                { ""Result"": ""No parasites found"" },
                { ""Result"": ""Parasites found"" },
                { ""Result"": ""Negative"" },
                { ""Result"": ""Positive"" },
                {
                    ""Result"": ""Protozoal trophozoites"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Cysts"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Helminth larvae"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Helminth eggs"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Mucus in feces"",
                    ""SnomedId"": ""271864008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Frothy stool"",
                    ""SnomedId"": ""449191000124109"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Acholic stool"",
                    ""SnomedId"": ""70396004"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Dark stools"",
                    ""SnomedId"": ""35064005"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Red stools"",
                    ""SnomedId"": ""64412006"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Stool color abnormal"",
                    ""SnomedId"": ""271863002"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Mucus in stool"",
                    ""SnomedId"": ""271864008"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Abnormal consistency of stool"",
                    ""SnomedId"": ""422784003"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Change in stool consistency"",
                    ""SnomedId"": ""229209009"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Creamy stool"",
                    ""SnomedId"": ""449201000124107"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""Dry stool"",
                    ""SnomedId"": ""424278001"",
                    ""Category"": ""Macroscopy""
                },
                {
                    ""Result"": ""adult or larval worms visible"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Fungal culture"",
            ""SnomedId"": ""104187009"",
            ""Specimen"": ""Blood"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": true,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                { ""Result"": ""No fungus isolated"" },
                { ""Result"": ""Fungus isolated"" },
                {
                    ""Result"": ""Candida sp cells"",
                    ""SnomedId"": ""3265006"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Fungal dermatophytes visible"",
                    ""Category"": ""Macroscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Microfilariae"",
            ""SnomedId"": ""122037007"",
            ""Specimen"": ""Blood sample taken at night"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": true,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" },
                { ""Result"": ""Microfilaria detected"" },
                {
                    ""Result"": ""W. bancrofti"",
                    ""SnomedId"": ""84925004"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""B. malayi"",
                    ""SnomedId"": ""112443002"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""Loa loa"",
                    ""SnomedId"": ""47759003"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""O. volvulus"",
                    ""SnomedId"": ""52397001"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""B. timori"",
                    ""SnomedId"": ""15475009"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""M. perstans"",
                    ""SnomedId"": ""243669003"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""M. streptocerca"",
                    ""SnomedId"": ""243668006"",
                    ""Category"": ""Microscopy""
                },
                {
                    ""Result"": ""M. ozzardi"",
                    ""SnomedId"": ""243668006"",
                    ""Category"": ""Microscopy""
                }
            ],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Widal test"",
            ""SnomedId"": ""165828002"",
            ""Specimen"": ""Whole blood"",
            ""Components"": [
                {
                    ""Name"": ""O antigen titer"",
                    ""Results"": [
                        { ""Result"": ""> 1:160"" },
                        { ""Result"": ""< 1:160"" }
                    ],
                    ""Type"": ""Microbiology""
                },
                {
                    ""Name"": ""H antigen titer"",
                    ""Results"": [
                        { ""Result"": ""> 1:160"" },
                        { ""Result"": ""< 1:160"" }
                    ],
                    ""Type"": ""Microbiology""
                }
            ],
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        },
        {
            ""Name"": ""Extended spectrum beta lactamase (ESBL)"",
            ""Microbiology"": {
                ""MethyleneBlueStain"": false,
                ""GramStain"": false,
                ""Culture"": false,
                ""AntibioticSensitivityTest"": false,
                ""Microscopy"": false,
                ""NugentScore"": false,
                ""CommonResults"": false
            },
            ""Suggestions"": [],
            ""Type"": ""Microbiology"",
            ""SpecificOrganism"": null,
            ""Dipstick"": null
        }]";
    }
}
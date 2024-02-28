namespace Plateaumed.EHR.Misc.Json.Investigations
{
    public class SerologyInvestigationJson
    {
        public static readonly string jsonData = /*lang=json*/ @"[
        {
            ""Name"": ""Hepatitis B surface antigen"",
            ""SnomedId"": ""47758006"",
            ""ShortName"": ""Hep B virus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Hepatitis B surface antigen"",
                    ""SnomedId"": 47758006,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Hepatitis B profile"",
            ""SnomedId"": ""62889000"",
            ""ShortName"": ""Hep B profile"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Hepatitis B surface antigen"",
                    ""SnomedId"": 47758006,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis B e antigen"",
                    ""SnomedId"": 313476009,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis B e antibody"",
                    ""SnomedId"": 79172007,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis B core antibody, IgG"",
                    ""SnomedId"": 54555005,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis B core antibody, IgM"",
                    ""SnomedId"": 50506008,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis B surface antibody"",
                    ""SnomedId"": 65911000,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Hepatitis C virus"",
            ""SnomedId"": ""413107006"",
            ""ShortName"": ""Hep C virus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Hepatitis C virus IgM"",
                    ""SnomedId"": 710652004,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis C virus IgG"",
                    ""SnomedId"": 710451000,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis C virus total antibody"",
                    ""SnomedId"": 64411004,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Hepatitis A virus"",
            ""SnomedId"": ""40093005"",
            ""ShortName"": ""Hep A virus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Hepatitis A virus IgG"",
                    ""SnomedId"": 13070009,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis A virus IgM"",
                    ""SnomedId"": 88159009,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Hepatitis A virus total antibody"",
                    ""SnomedId"": 104374007,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Syphilis/VDRL (RPR agglutination)"",
            ""SnomedId"": ""19869000"",
            ""ShortName"": ""Syphilis VDRL (RPR agg)"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Syphilis antibodies"",
                    ""SnomedId"": 51571009,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""VDRL, qualitative"",
                    ""SnomedId"": 25696006,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Helicobacter pylori"",
            ""SnomedId"": ""117725006:122432006=80774000"",
            ""ShortName"": ""H. Pylori"",
            ""Specimen"": ""Stool"",
            ""Components"": [
                {
                    ""Name"": ""H. pylori stool antigen"",
                    ""SnomedId"": 118034007,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""H. pylori serum antibody"",
                    ""SnomedId"": 104285006,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""H. pylori urea breath test"",
                    ""SnomedId"": 164791003,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Chlamydia"",
            ""SnomedId"": ""117725006:122432006=63938009"",
            ""ShortName"": ""Chlamydia"",
            ""Specimen"": ""Genital swab"",
            ""Components"": [
                {
                    ""Name"": ""Chlamydia antigen - genital swab"",
                    ""SnomedId"": 122173003,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Chlamydia IgG"",
                    ""SnomedId"": 134256004,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Chlamydia IgM"",
                    ""SnomedId"": 104282009,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" },
                    ],
                    ""Ranges"": [
                        {
                            ""MinRange"": 0,
                            ""MaxRange"": 0.9
                        }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Herpes simplex virus"",
            ""SnomedId"": ""46898007"",
            ""ShortName"": ""Herpes simplex virus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Herpes simplex virus 1 IgG"",
                    ""SnomedId"": 720053009,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Herpes simplex virus 1 IgM"",
                    ""SnomedId"": 720054003,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Herpes simplex virus 2 IgG"",
                    ""SnomedId"": 720055002,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Herpes simplex virus 2 IgM"",
                    ""SnomedId"": 720056001,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Rubella virus"",
            ""SnomedId"": ""9954002"",
            ""ShortName"": ""Rubella virus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Rubella virus IgG"",
                    ""SnomedId"": 313670007,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Rubella virus IgM"",
                    ""SnomedId"": 313479002,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Cytomegalovirus"",
            ""SnomedId"": ""30200007"",
            ""ShortName"": ""Cytomegalovirus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Cytomegalovirus IgG"",
                    ""SnomedId"": 313604004,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Cytomegalovirus IgM"",
                    ""SnomedId"": 104309001,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Toxoplasma gondii"",
            ""SnomedId"": ""104308009"",
            ""ShortName"": ""Toxoplasma gondii"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Toxoplasma IgG"",
                    ""SnomedId"": 315199001,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Toxoplasma IgM"",
                    ""SnomedId"": 104306008,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Typhoid IgG/IgM rapid diagnostic test"",
            ""ShortName"": ""Typhoid IgG/IgM"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Typhoid IgG/IgM"",
                    ""SnomedId"": null,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Respiratory syncytial virus"",
            ""SnomedId"": ""315184001"",
            ""ShortName"": ""Resp syncytial virus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Respiratory syncytial virus antigen"",
                    ""SnomedId"": 315184001,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Influenza virus"",
            ""SnomedId"": ""118089005"",
            ""ShortName"": ""Influenza virus"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Influenza A virus antigen"",
                    ""SnomedId"": 122349003,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Influenza B virus antigen"",
                    ""SnomedId"": 122350003,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""Influenza A virus subtype H1N1 antigen"",
                    ""SnomedId"": 443995002,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Serum pregnancy test "",
            ""SnomedId"": ""166434005"",
            ""ShortName"": ""Serum hCG"",
            ""Specimen"": ""Whole blood/serum"",
            ""Components"": [
                {
                    ""Name"": ""Serum pregnancy test "",
                    ""SnomedId"": 166434005,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Urine pregnancy test"",
            ""SnomedId"": ""167252002"",
            ""ShortName"": ""Urine pregnancy test"",
            ""Specimen"": ""Urine"",
            ""Components"": [
                {
                    ""Name"": ""Urine pregnancy test"",
                    ""SnomedId"": 167252002,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        { ""Result"": ""Negative"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""[Specific organism] PCR"",
            ""SnomedId"": ""9718006"",
            ""ShortName"": ""PCR"",
            ""SpecificOrganism"": ""Search"",
            ""Results"": [
                { ""Result"": ""Reactive"" },
                { ""Result"": ""Non-reactive"" }
            ],
            ""Components"": [],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""[Specific organism] antigen test"",
            ""SnomedId"": ""121276004"",
            ""ShortName"": ""Antigen test"",
            ""SpecificOrganism"": ""Search"",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" }
            ],
            ""Components"": [],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""[Specific organism] antibody test"",
            ""SnomedId"": ""120646007"",
            ""ShortName"": ""Antibody test"",
            ""SpecificOrganism"": ""Search"",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" }
            ],
            ""Components"": [],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""[Specific organism] IgG antibody"",
            ""SnomedId"": ""45293001"",
            ""ShortName"": ""IgG antibody"",
            ""SpecificOrganism"": ""Search"",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" }
            ],
            ""Components"": [],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""[Specific organism] IgM antibody"",
            ""SnomedId"": ""45764000"",
            ""ShortName"": ""IgM antibody"",
            ""SpecificOrganism"": ""Search"",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" }
            ],
            ""Components"": [],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Viral load"",
            ""SnomedId"": ""395058002"",
            ""ShortName"": ""Viral load"",
            ""Specimen"": """",
            ""SpecificOrganism"": ""HIV, Hepatitis C"",
            ""Components"": [],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""CD4 count"",
            ""SnomedId"": ""395058002"",
            ""ShortName"": ""CD4 count"",
            ""Specimen"": ""Whole blood"",
            ""Ranges"": [
                {
                    ""AgeMin"": null,
                    ""AgeMinUnit"": null,
                    ""AgeMax"": null,
                    ""AgeMaxUnit"": null,
                    ""Gender"": null,
                    ""Other"": null,
                    ""Unit"": null,
                    ""MinRange"": 500,
                    ""MaxRange"": 1400
                }
            ],
            ""Components"": [],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""Retroviral screening (HIV stat)"",
            ""SnomedId"": ""165813002"",
            ""ShortName"": ""HIV stat"",
            ""Components"": [
                {
                    ""Name"": ""HIV stat"",
                    ""SnomedId"": 171121004,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        },
        {
            ""Name"": ""HIV assay"",
            ""SnomedId"": ""171121004"",
            ""ShortName"": ""HIV assay"",
            ""Specimen"": ""Whole blood"",
            ""Components"": [
                {
                    ""Name"": ""HIV p24 antigen test"",
                    ""SnomedId"": 104332002,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""HIV-1 antibody"",
                    ""SnomedId"": 28804003,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""HIV-2 antibody"",
                    ""SnomedId"": 27494001,
                    ""Results"": [
                        { ""Result"": ""Reactive"" },
                        { ""Result"": ""Non-reactive"" }
                    ],
                    ""Type"": ""Serology""
                },
                {
                    ""Name"": ""HIV viral load"",
                    ""SnomedId"": 315124004,
                    ""Ranges"": [
                        {
                            ""Unit"": ""ml"",
                            ""MinRange"": 0,
                            ""MaxRange"": 75
                        }
                    ],
                    ""Type"": ""Serology""
                }
            ],
            ""Type"": ""Serology""
        }]";
    }
}
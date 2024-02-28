namespace Plateaumed.EHR.Misc.Json.Investigations
{
    public class HaematologyInvestigationsJson
    {
        public static readonly string jsonData = /*lang=json*/ @"[
        {
            ""Name"": ""Full blood count (FBC)"",
            ""ShortName"": ""FBC"",
            ""SnomedId"": 26604007,
            ""Synonyms"": ""Complete blood count"",
            ""Components"": [
                {
                    ""Name"": ""Red cell count"",
                    ""SnomedId"": 26604007,
                    ""Synonyms"": ""Complete blood count"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10¹²/L"",
                            ""MinRange"": ""4"",
                            ""MaxRange"": ""6""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Haemoglobin"",
                    ""SnomedId"": 117356000,
                    ""Synonyms"": ""Blood cell count, automated"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""18"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 14,
                            ""MaxRange"": ""18"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""18"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 12,
                            ""MaxRange"": ""16"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10,
                            ""MaxRange"": ""15.5"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10,
                            ""MaxRange"": ""15.5"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""1"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""1"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""3"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""3"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""2"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""2"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""31"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10.7,
                            ""MaxRange"": ""17.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""31"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10.7,
                            ""MaxRange"": ""17.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""0"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 13.4,
                            ""MaxRange"": ""19.9"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""0"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 13.4,
                            ""MaxRange"": ""19.9"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 11,
                            ""MaxRange"": null,
                            ""Other"": ""Pregnant""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""PCV"",
                    ""SnomedId"": 8830800,
                    ""Synonyms"": ""Blood cell count"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 40,
                            ""MaxRange"": 54
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 35,
                            ""MaxRange"": 47
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 11,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 37,
                            ""MaxRange"": 48
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 11,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 34,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 5,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""11"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 35,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 5,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""11"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 35,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 29,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 29,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 2,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 28,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 2,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 28,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 31,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 33,
                            ""MaxRange"": 54
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 31,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 33,
                            ""MaxRange"": 54
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""%"",
                            ""MinRange"": 42,
                            ""MaxRange"": 64
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""%"",
                            ""MinRange"": 42,
                            ""MaxRange"": 64
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""MCV"",
                    ""SnomedId"": ""104133003"",
                    ""Synonyms"": ""Blood cell count, manual | 117355001"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""fL"",
                            ""MinRange"": ""80"",
                            ""MaxRange"": ""95""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""MCH"",
                    ""SnomedId"": ""54706004"",
                    ""Synonyms"": ""Complete blood count with white cell differential, automated | 9564003"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""pg"",
                            ""MinRange"": ""27"",
                            ""MaxRange"": ""32""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""MCHC"",
                    ""SnomedId"": ""37254006"",
                    ""Synonyms"": ""Hemogram, automated, with RBC, WBC, Hgb, Hct, Indices, Platelet count, and automated complete WBC differential | 104093004"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""g/dL"",
                            ""MinRange"": ""32"",
                            ""MaxRange"": ""36""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""RDW"",
                    ""SnomedId"": ""66842004"",
                    ""Synonyms"": ""Hemogram, automated, with red blood cells, white blood cells, hemoglobin, hematocrit, Indices, Platelet count, and automated complete white blood cell differential | 104093004"",
                    ""Specimen"": ""Whole blood"",
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
                            ""MinRange"": ""12.1"",
                            ""MaxRange"": ""16.2""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""White cell count"",
                    ""SnomedId"": 104092009,
                    ""Synonyms"": ""Haemogram, automated, with RBC, WBC, Hgb, Hct, Indices, Platelet count, and automated partial WBC differential"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""4"",
                            ""MaxRange"": ""11""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Neutrophils"",
                    ""SnomedId"": ""30630007"",
                    ""Synonyms"": ""CBC with manual differential | 35774004"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""2"",
                            ""MaxRange"": ""7.5""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Neutrophils"",
                    ""SnomedId"": ""271035003"",
                    ""Synonyms"": ""Complete blood count without differential | 43789009"",
                    ""Specimen"": ""Whole blood"",
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
                            ""MinRange"": ""55"",
                            ""MaxRange"": ""70""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Lymphocytes"",
                    ""SnomedId"": ""74765001"",
                    ""Synonyms"": ""Haemogram, automated, with RBC, WBC, Hgb, Hct, Indices, Platelet count, and manual WBC differential | 104091002"",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""4""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Lymphocytes"",
                    ""SnomedId"": ""271036002"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
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
                            ""MinRange"": ""20"",
                            ""MaxRange"": ""40""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Monocytes"",
                    ""SnomedId"": ""67776007"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0.1"",
                            ""MaxRange"": ""0.7""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Monocytes"",
                    ""SnomedId"": ""271037006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
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
                            ""MinRange"": ""2"",
                            ""MaxRange"": ""8""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Eosinophils"",
                    ""SnomedId"": ""71960002"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0.05"",
                            ""MaxRange"": ""0.5""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Eosinophils"",
                    ""SnomedId"": ""310540006"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
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
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""4""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Basophils"",
                    ""SnomedId"": ""42351005"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0.025"",
                            ""MaxRange"": ""0.1""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Basophils"",
                    ""SnomedId"": ""271038001"",
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
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
                            ""MinRange"": ""0.5"",
                            ""MaxRange"": ""1""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Mid-cell percentage"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""0.9""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Mid-cell percentage"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
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
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""12""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Platelet count"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""150"",
                            ""MaxRange"": ""450""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Erythrocyte sedimentation rate (ESR)"",
            ""ShortName"": ""ESR"",
            ""SnomedId"": 416838001,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Erythrocyte sedimentation rate (ESR)"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""mm/h"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""20""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Packed cell volume (PCV)"",
            ""ShortName"": ""PCV"",
            ""SnomedId"": 250231008,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""PCV"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 40,
                            ""MaxRange"": 54
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 18,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 35,
                            ""MaxRange"": 47
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 11,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 37,
                            ""MaxRange"": 48
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 11,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 34,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 5,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""11"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 35,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 5,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""11"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 35,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""5"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 44
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 6,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""%"",
                            ""MinRange"": 31,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 29,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 3,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 29,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 2,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 28,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 2,
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 28,
                            ""MaxRange"": 41
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 31,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 33,
                            ""MaxRange"": 54
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 31,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""%"",
                            ""MinRange"": 33,
                            ""MaxRange"": 54
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""%"",
                            ""MinRange"": 42,
                            ""MaxRange"": 64
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""%"",
                            ""MinRange"": 42,
                            ""MaxRange"": 64
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Haemoglobin (Hb)"",
            ""ShortName"": ""Hb"",
            ""SnomedId"": 441689006,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Haemoglobin"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""18"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 14,
                            ""MaxRange"": ""18"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""18"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 12,
                            ""MaxRange"": ""16"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10,
                            ""MaxRange"": ""15.5"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""18"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10,
                            ""MaxRange"": ""15.5"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""1"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""1"",
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""6"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""1"",
                            ""AgeMaxUnit"": ""Year"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""3"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""3"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""6"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9.5,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""2"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9,
                            ""MaxRange"": ""14.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""2"",
                            ""AgeMinUnit"": ""Month"",
                            ""AgeMax"": ""3"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 9,
                            ""MaxRange"": ""14"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""31"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10.7,
                            ""MaxRange"": ""17.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""31"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""2"",
                            ""AgeMaxUnit"": ""Month"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 10.7,
                            ""MaxRange"": ""17.1"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Male"",
                            ""AgeMin"": ""0"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 13.4,
                            ""MaxRange"": ""19.9"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": ""0"",
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": ""31"",
                            ""AgeMaxUnit"": ""Day"",
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 13.4,
                            ""MaxRange"": ""19.9"",
                            ""Other"": """"
                        },
                        {
                            ""Gender"": ""Female"",
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Unit"": ""g/dL"",
                            ""MinRange"": 11,
                            ""MaxRange"": null,
                            ""Other"": ""Pregnant""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Malaria parasite microscopy"",
            ""ShortName"": ""MP microscopy"",
            ""SnomedId"": 372071003,
            ""Synonyms"": """",
            ""Results"": [
                { ""Result"": ""Positive"" },
                { ""Result"": ""Negative"" }
            ],
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
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Widal test"",
            ""ShortName"": ""Widal test"",
            ""SnomedId"": 165828002,
            ""Synonyms"": """",
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
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Blood Group & Rh"",
            ""ShortName"": ""Blood Group & Rh"",
            ""SnomedId"": 81228005,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Blood Group & Rh"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 2,
                    ""Suggestions"": [
                        {
                            ""Result"": ""A RhD -ve"",
                            ""SnomedId"": ""278152006""
                        },
                        {
                            ""Result"": ""A RhD +ve"",
                            ""SnomedId"": ""278149003""
                        },
                        {
                            ""Result"": ""AB RhD -ve"",
                            ""SnomedId"": ""278154007""
                        },
                        {
                            ""Result"": ""AB RhD +ve"",
                            ""SnomedId"": ""278151004""
                        },
                        {
                            ""Result"": ""B RhD -ve"",
                            ""SnomedId"": ""278153001""
                        },
                        {
                            ""Result"": ""B RhD +ve"",
                            ""SnomedId"": ""278150003""
                        },
                        {
                            ""Result"": ""O RhD -ve"",
                            ""SnomedId"": ""278148006""
                        },
                        {
                            ""Result"": ""O RhD +ve"",
                            ""SnomedId"": ""278147001""
                        },
                        {
                            ""Result"": ""A Rh Bombay"",
                            ""SnomedId"": ""115731008""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Blood Group"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": null,
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
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Rh"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": null,
                    ""Suggestions"": [
                        {
                            ""Result"": ""RhD -ve"",
                            ""SnomedId"": ""165746003""
                        },
                        {
                            ""Result"": ""RhD +ve"",
                            ""SnomedId"": ""165787007""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Hb genotype"",
            ""ShortName"": ""Hb genotype"",
            ""SnomedId"": 413019001,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Hb genotype"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 2,
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
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Sickling test"",
            ""ShortName"": ""Sickling test"",
            ""SnomedId"": 252289009,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Sickling test"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 3,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Retroviral screening"",
            ""ShortName"": ""RVD screen"",
            ""SnomedId"": 171121004,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Retroviral screening"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 3,
                    ""Results"": [
                        { ""Result"": ""Positive"" },
                        {
                            ""Result"": ""Negative"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Medical check - blood group, genotype, urinalysis, stool analysis, Mantoux test"",
            ""ShortName"": ""Medical check"",
            ""SnomedId"": 225886003,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Blood group"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole Blood"",
                    ""CardStyle"": null,
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
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Hb genotype"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 2,
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
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Urinalysis"",
                    ""ShortName"": ""Urinalysis"",
                    ""SnomedId"": 27171005,
                    ""Synonyms"": """",
                    ""Type"": ""Haematology"",
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ]
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
                            ""Type"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Striped muscle fibres"",
                            ""SnomedId"": ""167632006"",
                            ""Type"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Undigested starch"",
                            ""SnomedId"": ""276391004"",
                            ""Type"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Vegetable fibres"",
                            ""Type"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Crystals"",
                            ""SnomedId"": ""365687009"",
                            ""Type"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Fat"",
                            ""Type"": ""Microscopy""
                        },
                        {
                            ""Result"": ""Mucus in feces"",
                            ""SnomedId"": ""271864008"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Frothy stool"",
                            ""SnomedId"": ""449191000124109"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Acholic stool"",
                            ""SnomedId"": ""70396004"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Dark stools"",
                            ""SnomedId"": ""35064005"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Red stools"",
                            ""SnomedId"": ""64412006"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Stool color abnormal"",
                            ""SnomedId"": ""271863002"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Mucus in stool"",
                            ""SnomedId"": ""271864008"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Abnormal consistency of stool"",
                            ""SnomedId"": ""422784003"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Change in stool consistency"",
                            ""SnomedId"": ""229209009"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Creamy stool"",
                            ""SnomedId"": ""449201000124107"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""Dry stool"",
                            ""SnomedId"": ""424278001"",
                            ""Type"": ""Macroscopy""
                        },
                        {
                            ""Result"": ""adult or larval worms visible"",
                            ""Type"": ""Macroscopy""
                        }
                    ],
                    ""Type"": ""Haematology""
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
                            ""Type"": ""Macroscopy""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Direct Coombs test"",
            ""ShortName"": ""Direct Coombs test"",
            ""SnomedId"": 77020008,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Direct Coombs test"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 4,
                    ""Results"": [
                        { ""Result"": ""Agglutination observed"" },
                        {
                            ""Result"": ""No Agglutination"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Indirect Coombs test"",
            ""ShortName"": ""Indirect Coombs test"",
            ""SnomedId"": 16742006,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Indirect Coombs test"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": ""Whole blood"",
                    ""CardStyle"": 4,
                    ""Results"": [
                        { ""Result"": ""Agglutination observed"" },
                        {
                            ""Result"": ""No Agglutination"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Group & cross match"",
            ""ShortName"": ""Group X cross match"",
            ""SnomedId"": 90777005,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Group X cross match"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 5,
                    ""Results"": [
                        { ""Result"": ""Agglutination observed"" },
                        {
                            ""Result"": ""No Agglutination"",
                            ""Normal"": true
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Blood film"",
            ""ShortName"": ""Blood film"",
            ""SnomedId"": 104130000,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Blood film"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 6,
                    ""Suggestions"": [
                        {
                            ""Result"": ""Normocytes"",
                            ""SnomedId"": 165473003
                        },
                        {
                            ""Result"": ""Normochromic cells"",
                            ""SnomedId"": 250254004
                        },
                        {
                            ""Result"": ""Microcytes"",
                            ""SnomedId"": 397022008
                        },
                        {
                            ""Result"": ""Hypochromic cells"",
                            ""SnomedId"": 165486007
                        },
                        {
                            ""Result"": ""Macrocytes"",
                            ""SnomedId"": 259681001
                        },
                        {
                            ""Result"": ""Anisocytes"",
                            ""SnomedId"": 165475005
                        },
                        {
                            ""Result"": ""Polychromasia"",
                            ""SnomedId"": 165489000
                        },
                        {
                            ""Result"": ""Sickle cells"",
                            ""SnomedId"": 49938009
                        },
                        {
                            ""Result"": ""Target cells"",
                            ""SnomedId"": 398977003
                        },
                        {
                            ""Result"": ""Normoblasts"",
                            ""SnomedId"": 269816001
                        },
                        {
                            ""Result"": ""Megaloblasts"",
                            ""SnomedId"": 165488008
                        },
                        {
                            ""Result"": ""Heinz bodies"",
                            ""SnomedId"": 250236003
                        },
                        {
                            ""Result"": ""Howell-Jolly bodies"",
                            ""SnomedId"": 250234000
                        },
                        {
                            ""Result"": ""Dimorphic red cells"",
                            ""SnomedId"": 250240007
                        },
                        {
                            ""Result"": ""Siderocytes"",
                            ""SnomedId"": 61837008
                        },
                        {
                            ""Result"": ""Spherocytes"",
                            ""SnomedId"": 259682008
                        },
                        {
                            ""Result"": ""Elliptocytes"",
                            ""SnomedId"": 45028007
                        },
                        {
                            ""Result"": ""Spur cells"",
                            ""SnomedId"": 63599005
                        },
                        {
                            ""Result"": ""Burr cells"",
                            ""SnomedId"": 51384001
                        },
                        {
                            ""Result"": ""Fragmented cells"",
                            ""SnomedId"": 70310009
                        },
                        {
                            ""Result"": ""Poikilocytes"",
                            ""SnomedId"": 397020000
                        },
                        {
                            ""Result"": ""Acanthocytes"",
                            ""SnomedId"": 63599005
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Reticulocyte count"",
            ""ShortName"": ""Reticulocytes"",
            ""SnomedId"": 45995003,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Reticulocyte count"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 1,
                    ""Ranges"": [
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": 1,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 2,
                            ""MaxRange"": 6
                        },
                        {
                            ""AgeMin"": 1,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": 0.2,
                            ""MaxRange"": 2
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""G6PD screening"",
            ""ShortName"": ""G6PD screening"",
            ""SnomedId"": 104692005,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""G6PD screening"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": 7,
                    ""Ranges"": [
                        {
                            ""AgeMin"": 0,
                            ""AgeMinUnit"": ""Day"",
                            ""AgeMax"": 5,
                            ""AgeMaxUnit"": ""Year"",
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""U/g"",
                            ""MinRange"": 6.4,
                            ""MaxRange"": 15.6
                        },
                        {
                            ""AgeMin"": 5,
                            ""AgeMinUnit"": ""Year"",
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""U/g"",
                            ""MinRange"": 8.6,
                            ""MaxRange"": 18.6
                        }
                    ],
                    ""Results"": [
                        { ""Result"": ""Detected"" },
                        { ""Result"": ""Not Detected"" }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Bleeding time"",
            ""ShortName"": ""Bleeding time"",
            ""SnomedId"": 72406003,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Bleeding time"",
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
                            ""Unit"": ""Seconds"",
                            ""MinRange"": ""70"",
                            ""MaxRange"": ""120""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Clotting time"",
            ""ShortName"": ""Clotting time"",
            ""SnomedId"": 165566005,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Clotting time"",
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
                            ""Unit"": ""Minutes"",
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""9""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Prothrombin time"",
            ""ShortName"": ""Prothrombin time"",
            ""SnomedId"": 396451008,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Prothrombin time"",
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
                            ""Unit"": ""Seconds"",
                            ""MinRange"": ""10"",
                            ""MaxRange"": ""13""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""PT/INR"",
            ""ShortName"": ""PT/INR"",
            ""SnomedId"": 165581004,
            ""Synonyms"": """",
            ""Components"": [
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
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""1.5""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Red blood cell count"",
            ""ShortName"": ""RBC count"",
            ""SnomedId"": 14089001,
            ""Synonyms"": """",
            ""Components"": [
                {
                    ""Name"": ""Red cell count"",
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
                            ""Unit"": ""x10¹²/L"",
                            ""MinRange"": ""4"",
                            ""MaxRange"": ""6""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""White blood cell count"",
            ""ShortName"": ""WBC count"",
            ""SnomedId"": 165511009,
            ""Synonyms"": ""WBC count"",
            ""Components"": [
                {
                    ""Name"": ""White cell count"",
                    ""SnomedId"": 767002,
                    ""Synonyms"": ""WBC count"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""4"",
                            ""MaxRange"": ""11""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Neutrophils"",
                    ""SnomedId"": ""30630007"",
                    ""Synonyms"": ""Total white blood cell count | 391558003"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""2"",
                            ""MaxRange"": ""7.5""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Neutrophils"",
                    ""SnomedId"": ""271035003"",
                    ""Synonyms"": ""WBC estimate | 42396003"",
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
                            ""Unit"": ""%"",
                            ""MinRange"": ""55"",
                            ""MaxRange"": ""70""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Lymphocytes"",
                    ""SnomedId"": ""74765001"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""4""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Lymphocytes"",
                    ""SnomedId"": ""271036002"",
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
                            ""Unit"": ""%"",
                            ""MinRange"": ""20"",
                            ""MaxRange"": ""40""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Monocytes"",
                    ""SnomedId"": ""67776007"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0.1"",
                            ""MaxRange"": ""0.7""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Monocytes"",
                    ""SnomedId"": ""271037006"",
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
                            ""Unit"": ""%"",
                            ""MinRange"": ""2"",
                            ""MaxRange"": ""8""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Eosinophils"",
                    ""SnomedId"": ""71960002"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0.05"",
                            ""MaxRange"": ""0.5""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Eosinophils"",
                    ""SnomedId"": ""310540006"",
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
                            ""Unit"": ""%"",
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""4""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Basophils"",
                    ""SnomedId"": ""42351005"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0.025"",
                            ""MaxRange"": ""0.1""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Basophils"",
                    ""SnomedId"": ""271038001"",
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
                            ""Unit"": ""%"",
                            ""MinRange"": ""0.5"",
                            ""MaxRange"": ""1""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Mid-cell percentage"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""0.9""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Mid-cell percentage"",
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
                            ""Unit"": ""%"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""12""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Myeloblasts"",
                    ""SnomedId"": null,
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": null,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""5""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Promyelocytes"",
                    ""SnomedId"": ""43446009"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": null,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""5.5""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Metamyelocytes"",
                    ""SnomedId"": ""83321009"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": null,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": ""1"",
                            ""MaxRange"": ""12""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Band cells"",
                    ""SnomedId"": ""702697008"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""0.7""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Band cells"",
                    ""SnomedId"": ""702697008"",
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
                            ""Unit"": ""%"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""5""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Lymphoblasts"",
                    ""SnomedId"": ""15433008"",
                    ""Synonyms"": """",
                    ""Specimen"": """",
                    ""CardStyle"": null,
                    ""Ranges"": [
                        {
                            ""AgeMin"": null,
                            ""AgeMinUnit"": null,
                            ""AgeMax"": null,
                            ""AgeMaxUnit"": null,
                            ""Gender"": null,
                            ""Other"": null,
                            ""Unit"": ""%"",
                            ""MinRange"": ""0"",
                            ""MaxRange"": ""1""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        },
        {
            ""Name"": ""Platelet count"",
            ""ShortName"": ""Platelet count"",
            ""SnomedId"": 61928009,
            ""Synonyms"": ""Platelet count, blood, automated | 104107006"",
            ""Components"": [
                {
                    ""Name"": ""Platelet count"",
                    ""SnomedId"": 104107006,
                    ""Synonyms"": ""Platelet count, blood, automated"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""150"",
                            ""MaxRange"": ""450""
                        }
                    ],
                    ""Type"": ""Haematology""
                },
                {
                    ""Name"": ""Platelet count"",
                    ""SnomedId"": 104106002,
                    ""Synonyms"": ""Platelet count, blood, manual"",
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
                            ""Unit"": ""x10⁹/L"",
                            ""MinRange"": ""150"",
                            ""MaxRange"": ""450""
                        }
                    ],
                    ""Type"": ""Haematology""
                }
            ],
            ""Type"": ""Haematology""
        }]";
    }
};

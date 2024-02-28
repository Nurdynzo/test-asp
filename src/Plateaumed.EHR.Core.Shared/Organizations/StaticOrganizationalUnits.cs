using System;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Organizations
{
    public static class StaticOrganizationalUnits
    {
        public static Unit[] AllUnits()
        {
            return new[]
              {
                new Unit
                {
                    DisplayName = "Internal Medicine",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Accident & Emergency (A & E) Medicine",
                            ShortName = "A & E Medicine",
                            Type = OrganizationUnitType.Unit,
                            ServiceCentre = ServiceCentreType.AccidentAndEmergency,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Neurology",
                            ShortName = "Neuro Medicine",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Cardiology",
                            ShortName = "Cardio Medicine",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Respiratory Medicine",
                            Type = OrganizationUnitType.Unit,
                            ShortName = "Resp Medicine",
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Gastroenterology and Hepatology",
                            ShortName = "Gastro Medicine",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Infectious Diseases Unit",
                            ShortName = "IDU",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Endocrinology",
                            ShortName = "Endo",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Renal",
                            ShortName = "Renal",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Haematology and Oncology",
                            ShortName = "Haemat",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Dermatology, Genitourinary, Medicine and Rheaumatology",
                            ShortName = "Dermatology",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    },
                },
                new Unit
                {
                    DisplayName = "Family Medicine",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "General Outpatient Clinic",
                            ShortName = "GOP Clinic",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Surgery",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Accident & Emergency (A & E) Surgery",
                            ShortName = "(A & E) Surgery",
                            Type = OrganizationUnitType.Unit,
                            ServiceCentre = ServiceCentreType.AccidentAndEmergency,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Neurosurgery",
                            ShortName = "Neurosurgery",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Ears, Nose & Throat",
                            ShortName = "ENT",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Orthopaedics",
                            ShortName = "Ortho",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Burns & Plastic Surgery",
                            ShortName = "BPSU",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Cardiothoracic Surgery",
                            ShortName = "CTSU",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "General Surgery",
                            ShortName = "Gen Surg",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Urology",
                            ShortName = "Urology",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Paediatric Surgery",
                            ShortName = "Paed Surg",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Emergency/Intensive Care",
                            ShortName = "ICU",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Obstetrics & Gynaecology",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Obstetrics & Gynaecology",
                            ShortName = "O & G",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Experimental and Maternal Medicine",
                            ShortName = "EMM",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Oncology and Pathological Studies",
                            ShortName = "OPS",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Reproductive Endocrinology and Fertility Regulation",
                            ShortName = "REF",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Ultrasonography and Fetal Medicine",
                            ShortName = "UFM",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Paediatrics",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Neonatology/Perinatology",
                            ShortName = "Neonatal",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Clinical Nutrition/Gastroencology",
                            ShortName = "Paed Gastro",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Haematology/Oncology",
                            ShortName = "Paed Haemat",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Respiratory Paediatrics",
                            ShortName = "Resp Paed",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Cardiology",
                            ShortName = "Paed Cardio",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Neurology/Developmental Paediatrics",
                            ShortName = "Paed Neuro",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Endocrinology/Immunology",
                            ShortName = "Paed Endo",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Nephrology",
                            ShortName = "Paed Renal",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Social/Community Paediatrics",
                            ShortName = "Comm Paed",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Paediatric Infections",
                            ShortName = "Paed IDU",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Adolescent Paediatrics",
                            ShortName = "Adol Paed",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Rheumatology and School Health",
                            ShortName = "Paed Rheumat",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Immunization",
                            ShortName = "Immunization",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Ophthalmology",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Ophthalmology",
                            ShortName = "Ophthalmology",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Glaucoma",
                            ShortName = "Glaucoma",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Vitreoretina",
                            ShortName = "Vitreo",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Paediatric Ophthalmology",
                            ShortName = "Paed Ophthalmology",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Anterior Segment",
                            ShortName = "Anterior Seg",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Refraction/Low Vision",
                            ShortName = "Refraction",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Physiotherapy",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Physiotherapy",
                            ShortName = "Physio",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Neurophysiotherapy",
                            ShortName = "Neuro Physio",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Orthopaedic Physiotherapy",
                            ShortName = "Ortho Physio",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Paediatric Physiotherapy",
                            ShortName = "Paed Physio",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                        new Unit
                        {
                            DisplayName = "Ergonomics Physiotherapy",
                            ShortName = "Erg Physio",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Dentistry",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Dental",
                            ShortName = "Dental",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Dietetics",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Dietetics",
                            ShortName = "Dietetics",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
                new Unit
                {
                    DisplayName = "Social Care",
                    ShortName = "",
                    Type = OrganizationUnitType.Department,
                    Children = new[]
                    {
                        new Unit
                        {
                            DisplayName = "Social Care",
                            ShortName = "Social Care",
                            Type = OrganizationUnitType.Unit,
                            Children = Array.Empty<Unit>()
                        },
                    }
                },
              };

        }
    }

    public class Unit
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string ShortName { get; set; }
        public OrganizationUnitType Type { get; set; }
        public Unit[] Children { get; set; }
        public ServiceCentreType? ServiceCentre { get; set; }
    }
}

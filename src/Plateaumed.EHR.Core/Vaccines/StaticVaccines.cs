using System.Collections.Generic;
using System.Linq;
using PayPalCheckoutSdk.Orders;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Vaccines;

public class StaticVaccines
{
    public static List<VaccineGroup> GetVaccineGroups()
    {
        return new List<VaccineGroup>
        {
            new VaccineGroup
            {
                Name = "National Immunization Schedule"
            }
        };
    }

    public static List<Vaccine> GetVaccines(List<VaccineGroup> vaccineGroups)
    {
        var npi = vaccineGroups.First(v => v.Name == "National Immunization Schedule");

        return new List<Vaccine>
        {
            new Vaccine
            {
                Name = "Chicken Pox",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 2,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        Notes = "3 months interval between 1st and 2nd doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Meningococcal Conjugate",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "8 weeks interval between 1st and 2nd doses."
                    },
                    new VaccineSchedule
                    {
                        Doses = 1,
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Year,
                    }
                }
            },
            new Vaccine
            {
                Name = "MMR",
                FullName = "Measles, Mumps, Rubella",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 2,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        Notes = "4 weeks interval between 1st and 2nd doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Cholera",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 2,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        Notes = "2 weeks interval between 1st and 2nd doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Rotavirus",
                FullName = "Rotavirus Vaccine",
                Group = npi,
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Dosage = "1ml",
                        Doses = 1,
                        RouteOfAdministration = "Oral",
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Week
                    },
                    new VaccineSchedule //TODO conflict
                    {
                        Doses = 2,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "4 weeks interval between 1st and 2nd doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Typhoid Conjugate",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 1,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                    }
                }
            },
            new Vaccine
            {
                Name = "Hepatitis B Vaccine",
                Group = npi,
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Dosage = "0.5ml",
                        Doses = 1,
                        RouteOfAdministration = "Intramuscular",
                        AgeMin = 0,
                        AgeMinUnit = UnitOfTime.Day,
                        AgeMax = 1,
                        AgeMaxUnit = UnitOfTime.Day,
                        Notes = "Within first 24 hours after birth"
                    },
                    new VaccineSchedule //TODO conflict
                    {
                        Doses = 4,
                        AgeMin = 0,
                        AgeMinUnit = UnitOfTime.Day,
                        AgeMax = 1,
                        AgeMaxUnit = UnitOfTime.Month,
                        Notes =
                            "4 weeks interval between 1st and 2nd doses. 8 weeks interval between 2nd and 3rd doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Yellow Fever Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 1, 
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month
                    }
                }
            },
            new Vaccine
            {
                Name = "BCG",
                FullName = "Bacillus Calmette-Guérin",
                Group = npi,
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Dosage = "0.05ml",
                        Doses = 1,
                        RouteOfAdministration = "Intradermal",
                        AgeMin = 0,
                        AgeMinUnit = UnitOfTime.Day,
                        Notes = "As soon as possible after birth"
                    },
                    new VaccineSchedule
                    {
                        Doses = 1, 
                        AgeMin = 1, 
                        AgeMaxUnit = UnitOfTime.Month, 
                        AgeMax = 40,
                        AgeMinUnit = UnitOfTime.Year
                    }
                }
            },
            new Vaccine
            {
                Name = "Measles",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 2,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 18,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "4 weeks interval between 1st and 2nd doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Diphtheria, Pertussis, Tetanus",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 5,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes =
                            "8 weeks interval between 1st and 2nd doses. 8 weeks interval between 2nd and 3rd doses. 9 months interval between 3rd and 4th doses. 2yr interval between 4th and 5th doses."
                    },
                    new VaccineSchedule
                    {
                        Doses = 1,
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Year,
                    },
                }
            },
            new Vaccine
            {
                Name = "OPV",
                FullName = "Oral Polio Vaccine",
                Group = npi,
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Dosage = "2-3 drops",
                        Doses = 1,
                        RouteOfAdministration = "Oral",
                        AgeMin = 0,
                        AgeMinUnit = UnitOfTime.Day
                    },
                    new VaccineSchedule
                    {
                        Dosage = "2 drops",
                        Doses = 1,
                        RouteOfAdministration = "Oral",
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Week
                    },
                    new VaccineSchedule
                    {
                        Dosage = "2 drops",
                        Doses = 1,
                        RouteOfAdministration = "Oral",
                        AgeMin = 10,
                        AgeMinUnit = UnitOfTime.Week
                    },
                    new VaccineSchedule
                    {
                        Dosage = "2 drops",
                        Doses = 1,
                        RouteOfAdministration = "Oral",
                        AgeMin = 14,
                        AgeMinUnit = UnitOfTime.Week
                    },
                    new VaccineSchedule //TODO conflict
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "4 weeks interval between primary doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Tetanus Toxoid",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        Notes = "4 weeks interval between primary doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "PCV",
                FullName = "Pneumococcal Conjugate Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Dosage = "0.5ml",
                        Doses = 1,
                        RouteOfAdministration = "Intramuscular",
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Week,
                    },
                    new VaccineSchedule
                    {
                        Dosage = "0.5ml",
                        Doses = 1,
                        RouteOfAdministration = "Intramuscular",
                        AgeMin = 10,
                        AgeMinUnit = UnitOfTime.Week,
                    },
                    new VaccineSchedule
                    {
                        Dosage = "0.5ml",
                        Doses = 1,
                        RouteOfAdministration = "Intramuscular",
                        AgeMin = 14,
                        AgeMinUnit = UnitOfTime.Week,
                    },
                    new VaccineSchedule //TODO conflict
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "4 weeks interval between primary doses."
                    },
                    new VaccineSchedule
                    {
                        Doses = 3,
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Year,
                        AgeMax = 18,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "4 weeks interval between primary doses."
                    },
                    new VaccineSchedule
                    {
                        Doses = 1,
                        AgeMin = 19,
                        AgeMinUnit = UnitOfTime.Year
                    }
                }
            },
            new Vaccine
            {
                Name = "Haemophilus Influenza Type B (Hib) Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "4 weeks interval between primary doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Hepatitis A Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 2,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        Notes = "6 months interval between doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Human Papilloma Virus (HPV) Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 3,
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Year,
                        AgeMax = 65,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes =
                            "4 weeks between the first and second dose, 12 weeks between the second and third doses, and 5 months between the first and last doses(especially if only 2 doses)."
                    }
                }
            },
            new Vaccine
            {
                Name = "Pentavalent",
                FullName = "Diphtheria, Pertussis, Tetanus, Hepatitis B, Haemophilus Influenza Type b",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Dosage = "0.5ml",
                        Doses = 1,
                        RouteOfAdministration = "Intramuscular",
                        AgeMin = 6,
                        AgeMinUnit = UnitOfTime.Week
                    },
                    new VaccineSchedule
                    {
                        Dosage = "0.5ml",
                        Doses = 1,
                        RouteOfAdministration = "Intramuscular",
                        AgeMin = 10,
                        AgeMinUnit = UnitOfTime.Week
                    },
                    new VaccineSchedule
                    {
                        Dosage = "0.5ml",
                        Doses = 1,
                        RouteOfAdministration = "Intramuscular",
                        AgeMin = 14,
                        AgeMinUnit = UnitOfTime.Week
                    },
                    new VaccineSchedule //TODO conflict
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "4 weeks interval between primary doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Inactivated Polio Virus (IPV) Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes =
                            "4 weeks interval between primary doses and a booster dose 6 months after the 3rd dose"
                    }
                }
            },
            new Vaccine
            {
                Name = "Vitamin A Supplement",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Dosage = "(100,000 IU to 200,000 IU)",
                        Doses = 9,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "6 months interval between doses."
                    }
                }
            },
            new Vaccine
            {
                Name = "Anti-Rabies Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 5,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "Administer on days 0, 3, 7, 14, and 28"
                    }
                }
            },
            new Vaccine
            {
                Name = "COVID-19 Vaccine",
                Schedules = new List<VaccineSchedule>
                {
                    new VaccineSchedule
                    {
                        Doses = 3,
                        AgeMin = 1,
                        AgeMinUnit = UnitOfTime.Month,
                        AgeMax = 5,
                        AgeMaxUnit = UnitOfTime.Year,
                        Notes = "8 weeks interval between 1st and 2nd doses."
                    }
                }
            }
        };
    }
}
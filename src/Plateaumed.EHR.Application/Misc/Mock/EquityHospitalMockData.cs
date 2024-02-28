using System.Collections.Generic;
using Plateaumed.EHR.ValueObjects;
namespace Plateaumed.EHR.Misc.Mock
{
    public static class EquityHospitalMockData
    {

        public static List<PricingData> GetImagingPricingData()
        {
            return new List<PricingData>()
            {
                new()
                {
                    Name = "Pelvic Scan",
                    Amount = new Money(8000)
                },
                new()
                {
                    Name = "Obstetric Scan",
                    Amount = new Money(8000)
                },
                new()
                {
                    Name = "BPP(Biophysical Profile)",
                    Amount = new Money(15000)
                },
                new ()
                {
                    Name = "Transvaginal/Scrotal Testicular Scan",
                    Amount = new Money(10000)
                },
                new ()
                {
                    Name = "Abdominal Scan",
                    Amount = new Money(8000)
                },
                new()
                {
                    Name = "Abdomino-Pelvic Scan",
                    Amount = new Money(15000)
                },
                new()
                {
                    Name = "Kidney And Urinary Bladder Only",
                    Amount = new Money(12000)
                },
                new()
                {
                    Name = "Thyroid Scan",
                    Amount = new Money(10000)
                },
                new()
                {
                    Name = "Transfontanelle Scan",
                    Amount = new Money(10000)
                },
                new()
                {
                    Name = "Echocardiogram",
                    Amount = new Money(40000)
                },
                new()
                {
                    Name = "Doppler Abdominal Arterial",
                    Amount = new Money(25000)
                },
                new()
                {
                    Name = "Doppler Abdominal Venous",
                    Amount = new Money(25000)
                },
                new()
                {
                    Name = "Doppler Carotid",
                    Amount = new Money(25000)
                },
                new()
                {
                    Name = "Prostrate Scan + Abdomen",
                    Amount = new Money(15000)
                },
                new()
                {
                    Name = "Transrectal Prostrate Ultrasound",
                    Amount = new Money(15000)
                },
                new()
                {
                    Name = "Breast Scan",
                    Amount = new Money(10000)
                }
            };
        }

        public static List<PricingData> GetTheatrePricingData()
        {
            return new List<PricingData>()
            {
                new()
                {
                    Name = "Minor surgeries With C-ARM",
                    Amount = new Money(100000)
                },
                new()
                {
                    Name = "Minor surgeries Without C-ARM",
                    Amount = new Money(175000)
                },
                new()
                {
                    Name = "Intermediate surgeries With C-ARM",
                    Amount = new Money(150000)
                },
                new()
                {
                    Name = "Intermediate surgeries Without C-ARM",
                    Amount = new Money(175000)
                },
                new()
                {
                    Name = "Major surgeries With C-ARM",
                    Amount = new Money(200000)
                },
                new()
                {
                    Name = "Major surgeries Without C-ARM",
                    Amount = new Money(175000)
                },
                new()
                {
                    Name = "Anaesthetic Fee",
                    Amount = new Money(200000)
                },
                new()
                {
                    Name = "Anaesthsia",
                    Amount = new Money(50000)
                },
                new()
                {
                    Name = "Oxygen use per Hour",
                    Amount = new Money(10000)
                }
            };
        }

        public static List<PricingData> GetLabPricingData()
        {
            return new List<PricingData>()
            {
                new()
                {
                    Name = "Eletrolytes, Urea & Creatinine",
                    Amount = new Money(7200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Electrolytes (Na, K, Cl, Bicarb)",
                    Amount = new Money(4800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Urea+Creatinine",
                    Amount = new Money(5000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Sodium",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Potassium",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Chloride",
                    Amount = new Money(1800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Bicarbonate",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Urea",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Creatinine",
                    Amount = new Money(3600)
                },
                new()
                {
                    Name = "Creatinine (urine)",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Microalbuminuria (Albumin/Creatinine ratio)",
                    Amount = new Money(15600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Creatinine Clearance (Uncorrected)",
                    Amount = new Money(9300)
                },
                new()
                {
                    Name = "Creatinine Clearance (Corrected)",
                    Amount = new Money(7800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Urinary Protein",
                    Amount = new Money(5100),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Uric Acid",
                    Amount = new Money(3000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Uric Acid (Urinary)",
                    Amount = new Money(6000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Magnesium",
                    Amount = new Money(6000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Urinalysis (Biochemistry alone)",
                    Amount = new Money(1800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Urinalysis (Microscopy+Biochemistry)",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Calcium+Albumin",
                    Amount = new Money(5000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Calcium",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Phosphate (Serum)",
                    Amount = new Money(3300),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "LDH",
                    Amount = new Money(6600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "LFT-Liver Enzymes+Bilirubin+Protein",
                    Amount = new Money(11700),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Liver Enzymes",
                    Amount = new Money(9600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Serum Protein Eletrophoresis",
                    Amount = new Money(10100),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Urinary Protein Eletrophoresis",
                    Amount = new Money(64800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Albumin",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Total Protein",
                    Amount = new Money(2100),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Total Protein+Albumin",
                    Amount = new Money(3000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Bilirubin (Total)",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Bilirubin (conjugated)",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Bilirubin (Total+conjugated)",
                    Amount = new Money(4200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "ALP",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "GGT",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "AST",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "ALT",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Amylase (Serum)",
                    Amount = new Money(7200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Amylase (Urine)",
                    Amount = new Money(5100),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Lipase",
                    Amount = new Money(6000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Myoglobin (Card Reader)",
                    Amount = new Money(10800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CK-MB (Card Reader)",
                    Amount = new Money(10800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Troponin I",
                    Amount = new Money(11400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Troponin T (Card Reader)",
                    Amount = new Money(12000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Lipogram (Lipid Profile)",
                    Amount = new Money(9600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Cholesterol HDL",
                    Amount = new Money(6000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Cholesterol LDL",
                    Amount = new Money(6000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Cholesterol Total",
                    Amount = new Money(3900),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Triglycerides",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Total Protein (CSF)",
                    Amount = new Money(3600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Glucose CSF",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Glucose Fasting",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Glucose Random",
                    Amount = new Money(2400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Glucose 2HPP",
                    Amount = new Money(5700),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Glucose Tolerance Test (2 hours)",
                    Amount = new Money(4800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "HbA1c",
                    Amount = new Money(10500),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CRP",
                    Amount = new Money(7200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Ultrasensitive CRP",
                    Amount = new Money(16000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "TSH",
                    Amount = new Money(8000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "FREE T4",
                    Amount = new Money(7200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "FREE T3",
                    Amount = new Money(7200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "TFT (TSH, FREE T3, FREE T4)",
                    Amount = new Money(15000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Thyroglobulin Antibody",
                    Amount = new Money(23200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Thyroperoxidase",
                    Amount = new Money(18000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Parathyroid Hormone (PTH)",
                    Amount = new Money(32000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Fertility profile (Female: LH, FSH, PROL, PROG)",
                    Amount = new Money(32400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Fertility profile (Male)",
                    Amount = new Money(32400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Semen Analysis",
                    Amount = new Money(10400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Semen Analysis+MCS",
                    Amount = new Money(18000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Beta HCG (Quantitative)",
                    Amount = new Money(7800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Beta HCG (Qualitative)",
                    Amount = new Money(4000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Beta HCG (Male)",
                    Amount = new Money(7800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Prolactin",
                    Amount = new Money(10400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "FSH",
                    Amount = new Money(9600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "LH",
                    Amount = new Money(8700),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Estradiol (E2)",
                    Amount = new Money(10500),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Progesterone (Ovulation Day 21)",
                    Amount = new Money(10500),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Anti Mullerian Hormone",
                    Amount = new Money(20700),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "DHEA-S",
                    Amount = new Money(13200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Testosterone",
                    Amount = new Money(14400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Aldosterone",
                    Amount = new Money(14400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Renin",
                    Amount = new Money(46800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Cortisol (Serum)",
                    Amount = new Money(8700),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Cortisol (24 hours urine)",
                    Amount = new Money(12000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Growth Hormone",
                    Amount = new Money(17400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "ACTH",
                    Amount = new Money(41700),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Anti Diuretic Hormone",
                    Amount = new Money(44700),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "PSA",
                    Amount = new Money(13200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CEA (GIT, Lungs, Breast)",
                    Amount = new Money(7800),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CA 19-9 (GIT, Pancreas)",
                    Amount = new Money(11400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CA 125 (Ovary)",
                    Amount = new Money(11400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CA 15-3 (Breast)",
                    Amount = new Money(11400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CA 724 (GIT)",
                    Amount = new Money(29000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "AFP",
                    Amount = new Money(13200),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "ANCA",
                    Amount = new Money(23400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "ANF+ANTI DNA (ds DNA) FENA",
                    Amount = new Money(30000),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Anti Phospholipid Antibody",
                    Amount = new Money(99600),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Cardiolipin Antibody",
                    Amount = new Money(29400),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "Lupus Anticoagulant",
                    Amount = new Money(30900),
                    SubCategory = "Chemistry"
                },
                new()
                {
                    Name = "CBC",
                    Amount = new Money(6000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Platelet Count",
                    Amount = new Money(3000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "PCV",
                    Amount = new Money(2600),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Haemoglobin",
                    Amount = new Money(1500),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Reticulocytes Count",
                    Amount = new Money(3700),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "PBF",
                    Amount = new Money(4000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "ESR",
                    Amount = new Money(2000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Ferritin",
                    Amount = new Money(7800),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Total Iron Binding Capacity (TIBC)",
                    Amount = new Money(4500),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "RBC Folate",
                    Amount = new Money(27000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Serum Folate",
                    Amount = new Money(15600),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Vit. B12",
                    Amount = new Money(13200),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Hb Electrophoresis",
                    Amount = new Money(3600),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Hb Electrophoresis (Quantitative)",
                    Amount = new Money(24000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "ABO/Rh Blood Grouping",
                    Amount = new Money(3200),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "G6PD Estimation",
                    Amount = new Money(12000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Coomb's Test (Direct)",
                    Amount = new Money(6000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Coomb's Test (Indirect)",
                    Amount = new Money(8400),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "PT+INR",
                    Amount = new Money(7200),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "APTT",
                    Amount = new Money(7500),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Bleeding time",
                    Amount = new Money(1800),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Clotting time",
                    Amount = new Money(1800),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "D-Dimer",
                    Amount = new Money(11000),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Protein C",
                    Amount = new Money(84300),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "Protein S",
                    Amount = new Money(84300),
                    SubCategory = "Haematology"
                },
                new()
                {
                    Name = "HAV Screening",
                    Amount = new Money(5400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HBsAg Screening",
                    Amount = new Money(5000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Hepatitis B panel (Rapid)",
                    Amount = new Money(12000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HCV Screening",
                    Amount = new Money(5100),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Retroviral (HIV) Screening",
                    Amount = new Money(6000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "VDRL (Syphilis) Screening",
                    Amount = new Money(2400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Chlamydia Screening",
                    Amount = new Money(7200),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Rubella IgM",
                    Amount = new Money(11400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Rubella Immunity (IgG only)",
                    Amount = new Money(11100),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Toxoplasma IgM",
                    Amount = new Money(15000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "TPHA (Syphilis)",
                    Amount = new Money(12000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Herpes Simplex I&II (IgM/IgG)",
                    Amount = new Money(26400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Mumps IgM",
                    Amount = new Money(15000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Mumps IgG",
                    Amount = new Money(12000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Measles IgM",
                    Amount = new Money(15000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Measles IgG",
                    Amount = new Money(12000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "CD4 count",
                    Amount = new Money(20400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HIV ELISA Confirmation",
                    Amount = new Money(12600),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "P24 Antigen",
                    Amount = new Money(9400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Salmonella IgG/IgM",
                    Amount = new Money(3000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Salmonella IgG/IgM+Malaria RDT+Smear",
                    Amount = new Money(7000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Malaria RDT",
                    Amount = new Money(1800),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Malaria Smear",
                    Amount = new Money(2700),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Malaria RDT+Smear",
                    Amount = new Money(4200),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Urine Microscopy",
                    Amount = new Money(1500),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Urine MCS",
                    Amount = new Money(9000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Urine Reducing Substances",
                    Amount = new Money(4800),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Stool Microscopy",
                    Amount = new Money(2400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Stool MCS",
                    Amount = new Money(9000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Stool Reducing Substances",
                    Amount = new Money(4800),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Faecal Occult Blood",
                    Amount = new Money(4500),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Helicobacter pylori Antigen",
                    Amount = new Money(9600),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Helicobacter pylori Antibody",
                    Amount = new Money(7200),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "CSF MCS+Cell count+Chemistry",
                    Amount = new Money(12000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "CSF Cell count",
                    Amount = new Money(3600),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "CSF MCS",
                    Amount = new Money(13200),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Blood Culture MCS",
                    Amount = new Money(21600),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Blood Microscopy for Parasites (microfilariae)",
                    Amount = new Money(2400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "High vaginal/urethral/penile/endocervical MCS",
                    Amount = new Money(9000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Nose/eye/ear MCS",
                    Amount = new Money(9000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Throat/Sputum MCS",
                    Amount = new Money(9000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Mantoux Test",
                    Amount = new Money(1800),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "TB Quantiferon",
                    Amount = new Money(45800),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Wound/Pus MCS",
                    Amount = new Money(11400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Aspirate/Fluid MCS",
                    Amount = new Money(8400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Hepatitis A IgM Antibody (Quantitative)",
                    Amount = new Money(11500),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "Hepatitis B Profile",
                    Amount = new Money(44400),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HBV DNA Viral load",
                    Amount = new Money(39600),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HBV Core IgM Antibody",
                    Amount = new Money(11000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HBV Core Total Antibody",
                    Amount = new Money(12000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HBV Surface Antigen (Quantitative)",
                    Amount = new Money(13500),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HBV Envelope Antigen",
                    Amount = new Money(11000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HBV Envelope Antibody",
                    Amount = new Money(12000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HIV Viral load",
                    Amount = new Money(45000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HCV Genotype",
                    Amount = new Money(166800),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "HCV Viral Load",
                    Amount = new Money(39600),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "PCR Chlamydia trachomatis",
                    Amount = new Money(54000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "PCR Neisseria gonorrhoeae",
                    Amount = new Money(39600),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "PCR Herpes 1&2",
                    Amount = new Money(72000),
                    SubCategory = "Microbiology"
                },
                new()
                {
                    Name = "NIPT (Natera Pre-Natal Non-Invasive Test)",
                    Amount = new Money(489600),
                    SubCategory = "Microbiology"
                },
            };
        }

        public static List<PricingData> GetRegistrationPricing()
        {
            return new List<PricingData>()
            {
                new()
                {
                    Name = "Personal Registration",
                    Amount = new Money(1500)
                },
                new()
                {
                    Name = "Family of four (4)",
                    Amount = new Money(5000),
                    SubCategory = "Family Registration"
                }
            };
        }

        public static List<PricingData> GetConsultationPricing()
        {
            return new List<PricingData>()
            {
                new()
                {
                    Name = "G.P",
                    Amount = new Money(10000)
                },
                new()
                {
                    Name = "G.P Follow-up",
                    Amount = new Money(7500),
                },
                new()
                {
                    Name = "Family Physician",
                    Amount = new Money(15000),
                },
                new()
                {
                    Name = "Family Physician Follow up",
                    Amount = new Money(10000),
                }
            };
        }

        public static List<PricingData> GetAmbulancePricing()
        {
            return new List<PricingData>()
            {
                new()
                {
                    Name = "WITHIN AND AROUND  LEKKI PHASE 1(VI, IKOYI, JAKANDE)",
                    Amount = new Money(50000)
                },
                new()
                {
                    Name = "BETWEEN LEKKI PHASE 2- AJAH AND LAGOS ISLAND",
                    Amount = new Money(75000),
                },
                new()
                {
                    Name = "AFTER AJAH AND MAINLAND",
                    Amount = new Money(100000),
                },
                new()
                {
                    Name = "A Whole Day Event (With a Doctor)",
                    Amount = new Money(20000)
                },
                new()
                {
                    Name = "REGISTERED PATIENT",
                    Amount = new Money(50000)
                }

            };
        }

        public static List<PricingData> GetWardPricing()
        {
            return new List<PricingData>()
            {
                new()
                {
                    Name = "General",
                    Amount = new Money(20000)
                },
                new()
                {
                    Name = "Private",
                    Amount = new Money(40000)
                },
                new()
                {
                    Name = "VIP",
                    Amount = new Money(50000)
                },
                new()
                {
                    Name = "VVIP",
                    Amount = new Money(75000)
                },
                new()
                {
                    Name = "Oxygen use/Hour",
                    Amount = new Money(10000)

                },
                new()
                {
                    Name = "Nursing Services/Day",
                    Amount = new Money(10000)
                },
                new()
                {
                    Name = "Professional fees/Day",
                    Amount = new Money(15000)
                },
                new()
                {
                    Name = "ICU (To cover 3 days admission)",
                    Amount = new Money(1500000)
                }
            };
        }
    }

    public class PricingData
    {
        public string Name { get; set; }
        
        public Money Amount { get; set; }

        public string SubCategory { get; set; }
    }
}
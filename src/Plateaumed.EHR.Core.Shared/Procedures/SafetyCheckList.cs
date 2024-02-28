using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Plateaumed.EHR.Procedures
{
    public class SafetyCheckList
    {
        [StringLength(250)]
        public string Name { get; set; }
        public bool Checked { get; set; }
        public ProcedureEntryType Type { get; set; }
        public DateTime? DateChecked { get; set; }
        public static List<SafetyCheckList> GetDefaultList()
        {
            return new List<SafetyCheckList>()
            {
                new()
                {
                    Name = "Has operation site been marked?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Has appropriate shaving has been done?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Has nail polish has been removed?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Patient has allergies?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Patient has pacemaker electrodes?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Patient uses hearing aid?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Patient has dentures?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Patient has braces?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Patient has brace material?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Number of blood units available?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop

                },
                new()
                {
                    Name = "Preoperative observations have been made?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Has informed consent form has been signed?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Investigation results included",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Full Blood Count",
                    Checked = false,
                    Type = ProcedureEntryType.Preop

                },
                new()
                {
                    Name = "Electrolytes, Urea & Creatinine",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "X-ray",
                    Checked = false,
                    Type = ProcedureEntryType.Preop,
                },
                new()
                {
                    Name = "Is appropriate implant included?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Is preoperative pack available?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Is anaesthetic pack available?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Is payment receipt available?",
                    Checked = false,
                    Type = ProcedureEntryType.Preop
                },
                new()
                {
                    Name = "Have members of the surgical team been confirmed by names and roles?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop
                },
                new()
                {
                    Name = "Has patient been confirmed by name?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop,
                },
                new()
                {
                    Name = "Has operation site been confirmed?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop
                },
                new()
                {
                    Name = "Has procedure been confirmed?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop
                },
                new()
                {
                    Name = "Have critical or unexpected steps been reviewed by the specialists?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop
                },
                new()
                {
                    Name = "Has Nurse confirmed sterility of instruments?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop
                },
                new()
                {
                    Name = "Are consumables adequate?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop
                },
                new()
                {
                    Name = "Are extra instruments available?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop,
                },
                new()
                {
                    Name = "Has prophylactic antibiotics been given?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop

                },
                new()
                {
                    Name = "Is essential imaging displayed?",
                    Checked = false,
                    Type = ProcedureEntryType.Intraop
                },
                new()
                {
                    Name = "Was the name of procedure recorded?",
                    Checked = false,
                    Type = ProcedureEntryType.Postop

                },
                new()
                {
                    Name = "Are instruments, sponge, and needle counts correct?",
                    Checked = false,
                    Type = ProcedureEntryType.Postop
                },
                new()
                {
                    Name = "Is specimen labeling accurate?",
                    Checked = false,
                    Type = ProcedureEntryType.Postop

                },
                new()
                {
                    Name = "Have equipment problems been addressed, if any?",
                    Checked = false,
                    Type = ProcedureEntryType.Postop

                },
                new()
                {
                    Name = "Specialist(s), Anaesthetist(s) and Nurse(s) have reviewed the key concerns for recovery and management of the patient?",
                    Checked = false,
                    Type = ProcedureEntryType.Postop
                }
            };

        }
    }
}

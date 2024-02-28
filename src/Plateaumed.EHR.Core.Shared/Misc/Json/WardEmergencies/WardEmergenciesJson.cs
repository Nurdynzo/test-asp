namespace Plateaumed.EHR.Misc.Json.WardEmergencies;

public class WardEmergenciesJson
{
    public static readonly string jsonData = /*lang=json*/ @"[
    {
        ""SnomedId"": 386661006,
        ""ShortName"": ""Febrile"",
        ""Event"": ""Patient febrile"",
        ""Interventions"": [ { ""Name"": ""Measured temperature"" } ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Chills and rigor "",
        ""Event"": ""Patient observed to be having chills and rigors"",
        ""Interventions"": [ { ""Name"": ""Checked vital signs"" } ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Complains of cold"",
        ""Event"": ""Patient complained of being cold"",
        ""Interventions"": [ { ""Name"": ""Exposed patient to air"" } ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Warm to touch"",
        ""Event"": ""Patient warm to touch"",
        ""Interventions"": [
            { ""Name"": ""Tepid sponged"" },
            { ""Name"": ""Administered antipyretics"" },
            { ""Name"": ""Covered patient with top sheet"" },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": 23141003,
        ""ShortName"": ""Gasping"",
        ""Event"": ""Patient gasping"",
        ""Interventions"": [ { ""Name"": ""Commenced CPR"" } ]
    },
    {
        ""SnomedId"": 418107008,
        ""ShortName"": ""Unconscious"",
        ""Event"": ""Patient unconscious"",
        ""Interventions"": [
            {
                ""Name"": ""Commenced intranasal O₂ therapy via nasal cannula""
            }
        ]
    },
    {
        ""SnomedId"": 271783006,
        ""ShortName"": ""Poor and semi-conscious"",
        ""Event"": ""Patient poor and semi-conscious"",
        ""Interventions"": [ { ""Name"": ""Oxygen unavailable"" } ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Unresponsive to call or touch"",
        ""Event"": ""Patient unresponsive to call or touch"",
        ""Interventions"": [ { ""Name"": ""Oxygen concentrator unavailable"" } ]
    },
    {
        ""SnomedId"": 20573003,
        ""ShortName"": ""Ineffective breathing pattern seen"",
        ""Event"": ""Patient displaying ineffective breathing pattern"",
        ""Interventions"": [
            { ""Name"": ""Regular SpO₂ monitoring commenced"" }
        ]
    },
    {
        ""SnomedId"": 271787007,
        ""ShortName"": ""Collapsed"",
        ""Event"": ""Patient collapsed"",
        ""Interventions"": [ { ""Name"": ""Pulse oximeter unavailable"" } ]
    },
    {
        ""SnomedId"": 309585006,
        ""ShortName"": ""Fainted"",
        ""Event"": ""Patient fainted"",
        ""Interventions"": [
            {
                ""Name"": ""Performed quick physical examination""
            }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Apneic"",
        ""Event"": ""Patient apneic"",
        ""Interventions"": [
            { ""Name"": ""Checked pulse"" },
            { ""Name"": ""No pulse detected"" },
            { ""Name"": ""Checked vital signs"" },
            { ""Name"": ""Checked GCS score"" },
            { ""Name"": ""Nursed in cardiac position"" },
            { ""Name"": ""Administered IV hydrocortisone"" },
            { ""Name"": ""Administered medication"" },
            { ""Name"": ""Commenced IV fluids"" },
            { ""Name"": ""Marked as critically ill"" },
            { ""Name"": ""Informed doctors on call"" },
            { ""Name"": ""Doctor in attendance"" },
            {
                ""Name"": ""Called CTSU for intubation assessment""
            }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Apparently stopped breathing"",
        ""Event"": ""Patient observed to have stopped breathing"",
        ""Interventions"": [
            {
                ""Name"": ""Performed quick patient assessment""
            },
            { ""Name"": ""Commenced CPR"" },
            { ""Name"": ""Checked pupillary reflex"" },
            { ""Name"": ""Pupils fixed and dilated"" },
            {
                ""Name"": ""Checked for spontaneous respiration""
            },
            { ""Name"": ""Checked for heart sounds"" },
            { ""Name"": ""No heart sounds detected"" },
            { ""Name"": ""Checked pulse"" },
            { ""Name"": ""No pulse detected"" },
            { ""Name"": ""Called attention of the doctors"" },
            { ""Name"": ""Doctor in attendance"" },
            { ""Name"": ""Patient resuscitated"" },
            { ""Name"": ""Pulmonary activity resumed"" },
            { ""Name"": ""Regular SpO₂ monitoring commenced"" },
            {
                ""Name"": ""Commenced intranasal O₂ therapy via nasal cannula""
            },
            { ""Name"": ""Checked vital signs"" },
            { ""Name"": ""Administered 0.5mg adrenaline"" },
            { ""Name"": ""Administered medication"" },
            { ""Name"": ""Commenced IV fluids"" },
            {
                ""Name"": ""All efforts to resuscitate patient unsuccessful""
            },
            { ""Name"": ""Patient certified dead by doctor"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Death"",
        ""Event"": ""Patient dead"",
        ""Interventions"": [
            { ""Name"": ""Last office performed"" },
            { ""Name"": ""Informed relatives"" },
            {
                ""Name"": ""Patient yet to be transferred to morgue""
            },
            { ""Name"": ""Mortuary paper sent"" },
            { ""Name"": ""Transferred corpse to morgue"" },
            { ""Name"": ""Informed unit consultant"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Fall from bed"",
        ""Event"": ""Patient observed to have fallen from bed"",
        ""Interventions"": [
            { ""Name"": ""Assessed patient at point of fall"" },
            { ""Name"": ""No fractures observed"" },
            { ""Name"": ""Fracture observed"" },
            { ""Name"": ""Nil bleeding observed"" },
            { ""Name"": ""Bleeding observed"" },
            { ""Name"": ""Nil swelling observed"" },
            { ""Name"": ""Swelling observed"" },
            { ""Name"": ""Fracture site dressed"" },
            {
                ""Name"": ""Bleeding stopped and wound site dressed""
            },
            { ""Name"": ""Patient moved back into bed"" },
            { ""Name"": ""Patient stabilized"" },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""IV line tissued"",
        ""Event"": ""Patient's IV line tissued"",
        ""Interventions"": [ { ""Name"": ""For IV line reset"" } ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""IV line removed"",
        ""Event"": ""Patient's IV line removed"",
        ""Interventions"": [
            { ""Name"": ""Informed doctors"" },
            { ""Name"": ""IV line reset carried out"" }
        ]
    },
    {
        ""SnomedId"": 91175000,
        ""ShortName"": ""Seizuring"",
        ""Event"": ""Patient having a seizure"",
        ""Interventions"": [
            {
                ""Name"": ""Removed dangerous items in immediate environment to prevent harm to patient""
            }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Convulsing"",
        ""Event"": ""Patient convulsing"",
        ""Interventions"": [
            {
                ""Name"": ""Placed patient in recovery position""
            },
            {
                ""Name"": ""Length of seizure recorded as < 5 minutes""
            },
            {
                ""Name"": ""Length of seizure recorded as > 5 minutes""
            },
            { ""Name"": ""Administered diazepam"" },
            {
                ""Name"": ""Administered phenytoin following seizure persistence""
            },
            {
                ""Name"": ""Administered phenobarbital following seizure persistence""
            },
            { ""Name"": ""Informed doctors"" },
            { ""Name"": ""Seizure resolved"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Feeding tube dislodged"",
        ""Event"": ""Patient's feeding tube dislodged"",
        ""Interventions"": [ { ""Name"": ""Informed doctors to reset"" } ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Tracheostomy tube dislodged"",
        ""Event"": ""Patient's tracheostomy tube dislodged"",
        ""Interventions"": [
            {
                ""Name"": ""Patient calmed to reduce agitation""
            }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Drain tube dislodged"",
        ""Event"": ""Patient's drain tube dislodged"",
        ""Interventions"": [
            {
                ""Name"": ""Patient restrained on account of agitation""
            }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Urethral catheter dislodged"",
        ""Event"": ""Patient's urethral catheter dislodged"",
        ""Interventions"": [
            {
                ""Name"": ""Placed a light dressing over the drain insertion site""
            },
            { ""Name"": ""Medication given to calm patient"" },
            { ""Name"": ""Patient cleaned up and bed tidied"" },
            {
                ""Name"": ""Placed patient in cardiac position""
            },
            {
                ""Name"": ""Placed an absorbent pad on the bed pending tube reinsertion""
            },
            { ""Name"": ""Tube reinsertion completed"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Paul's tube detached"",
        ""Event"": ""Patient's Paul's tube detached"",
        ""Interventions"": [
            { ""Name"": ""Attempted to reattach Paul's tube"" },
            {
                ""Name"": ""Efforts to reattach Paul's tube proved abortive""
            },
            {
                ""Name"": ""Paul's tube reattached successfully""
            },
            { ""Name"": ""Informed doctors to reset"" }
        ]
    },
    {
        ""SnomedId"": 249489001,
        ""ShortName"": ""Choking"",
        ""Event"": ""Patient choking"",
        ""Interventions"": [
            { ""Name"": ""Assessed patient for cause"" },
            { ""Name"": ""Asked patient to cough"" },
            { ""Name"": ""Performed Helmlich manouvre"" },
            { ""Name"": ""Choking successfully stopped"" },
            {
                ""Name"": ""Attempts to stop choking unsuccessful""
            },
            {
                ""Name"": ""Foreign body causing choking dislodged""
            },
            {
                ""Name"": ""Resulted in patient being unconscious""
            },
            { ""Name"": ""Calmed patient down"" },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": 131148009,
        ""ShortName"": ""Bleeding"",
        ""Event"": ""Patient bleeding"",
        ""Interventions"": [
            {
                ""Name"": ""Removed debris and clothing from the bleeding site""
            },
            { ""Name"": ""Placed gauze on the wound"" },
            { ""Name"": ""Applied pressure to stop bleeding"" },
            {
                ""Name"": ""Applied a tourniquet to control bleeding""
            },
            { ""Name"": ""Applied wound dressing "" },
            { ""Name"": ""Calmed the patient"" },
            { ""Name"": ""Bleeding successfully stopped"" },
            { ""Name"": ""Bleeding continued"" },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Blood sugar low"",
        ""Event"": ""Patient's blood sugar low"",
        ""Interventions"": [
            { ""Name"": ""Commenced IV dextrose"" },
            { ""Name"": ""Checked vital signs"" },
            { ""Name"": ""Checked GCS score"" },
            {
                ""Name"": ""Commenced regular blood sugar monitoring""
            },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Blood sugar high"",
        ""Event"": ""Patient's blood sugar high"",
        ""Interventions"": [
            { ""Name"": ""Administered medication"" },
            { ""Name"": ""Checked vital signs"" },
            { ""Name"": ""Checked GCS score"" },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Vomiting"",
        ""Event"": ""Patient observed to be vomiting"",
        ""Interventions"": [
            { ""Name"": ""Vomitus appeared copious"" },
            { ""Name"": ""Vomitus appeared scanty"" },
            { ""Name"": ""contained food particles"" },
            { ""Name"": ""was projectile"" },
            { ""Name"": ""greenish"" },
            { ""Name"": ""bloody"" },
            { ""Name"": ""bilous"" },
            { ""Name"": ""black or bloody"" },
            { ""Name"": ""non-bilous"" },
            { ""Name"": ""feculent"" },
            { ""Name"": ""began a few hours after eating"" },
            { ""Name"": ""Calmed patient down"" },
            {
                ""Name"": ""Cleaned up patient and tidied environment""
            },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Vomited after oral medication"",
        ""Event"": ""Patient vomited after oral medication"",
        ""Interventions"": [
            { ""Name"": ""Calmed patient down"" },
            {
                ""Name"": ""Cleaned up patient and environment""
            },
            {
                ""Name"": ""Could see oral medication in vomitus""
            },
            { ""Name"": ""Repeated medication"" },
            { ""Name"": ""Did not repeat medication"" },
            { ""Name"": ""Informed doctors"" }
        ]
    },
    {
        ""SnomedId"": 29857009,
        ""ShortName"": ""Chest pain"",
        ""Event"": ""Patient complaining of chest pain"",
        ""Interventions"": [
            { ""Name"": ""Pain level assessed"" },
            { ""Name"": ""Checked vital signs"" },
            { ""Name"": ""Nursed in upright position"" },
            { ""Name"": ""Physical examination carried out"" },
            {
                ""Name"": ""Administered prescribed medication ""
            },
            { ""Name"": ""Informed doctors"" },
            {
                ""Name"": ""Pain reported as subsiding by patient""
            }
        ]
    },
    {
        ""SnomedId"": 21522001,
        ""ShortName"": ""Abdominal pain"",
        ""Event"": ""Patient complaining of abdominal pain pain"",
        ""Interventions"": [
            { ""Name"": ""Pain level assessed"" },
            { ""Name"": ""Physical examination carried out"" },
            { ""Name"": ""Diversional therapy provided"" },
            {
                ""Name"": ""Administered prescribed medication ""
            },
            { ""Name"": ""Checked vital signs"" },
            { ""Name"": ""Informed doctors"" },
            {
                ""Name"": ""Pain reported as subsiding by patient""
            }
        ]
    },
    {
        ""SnomedId"": null,
        ""ShortName"": ""Patient hypotensive"",
        ""Event"": ""Patient hypotensive"",
        ""Interventions"": [ { ""Name"": ""Commenced inotropic support"" } ]
    }
]";
}
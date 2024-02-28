using System.Collections.Generic;

namespace Plateaumed.EHR.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
        }

        public static class JobRoles
        {
            public const string Doctor = "Doctor";
            public const string DentalDoctor = "Dental Doctor";
            public const string Nurse = "Nurse";
            public const string Pharmacist = "Pharmacist";
            public const string PharmacyTechnician = "Pharmacy Technician";
            public const string LaboratoryScientist = "Laboratory Scientist";
            public const string LaboratoryTechnician = "Laboratory Technician";
            public const string Radiographer = "Radiographer";
            public const string XrayTechnician = "X-ray Technician";
            public const string Physiotherapist = "Physiotherapist";
            public const string Dietetics = "Dietetics";
            public const string SocialCare = "Social Care";
            public const string FrontDesk = "Front Desk";
            public const string Accountant = "Accountant";
            public const string Anaethetist = "Anaethetist";
            public const string AdminPersonnel = "Administrative Personnel";
        }

        public static class TeamRoles
        {
            public const string CMD = "CMD";
            public const string HOD = "HOD";
        }

        public static class Tenants
        {
            public const string Admin = "Admin";

            public const string User = "User";

            public static List<string> AllRoles = new()
            {
                Admin,
                User,
                JobRoles.Doctor,
                JobRoles.Nurse,
                JobRoles.Pharmacist,
                JobRoles.LaboratoryScientist,
                JobRoles.FrontDesk,
                JobRoles.Anaethetist,
                JobRoles.DentalDoctor,
                JobRoles.PharmacyTechnician,
                JobRoles.LaboratoryTechnician,
                JobRoles.Radiographer,
                JobRoles.XrayTechnician,
                JobRoles.Physiotherapist,
                JobRoles.Dietetics,
                JobRoles.SocialCare,
                JobRoles.Accountant,
                JobRoles.AdminPersonnel,
                TeamRoles.CMD,
                TeamRoles.HOD
            };
        }
    }
}

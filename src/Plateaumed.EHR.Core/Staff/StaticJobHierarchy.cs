using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Staff
{
    public class StaticJobHierarchy
    {
        public static List<JobTitle> GetDefaultJobTitlesHierarchyForTenant(int? tenantId, long facilityId)
        {
            return new List<JobTitle>
            {
                new()
                {
                    Name = StaticRoleNames.JobRoles.Doctor,
                    ShortName = "Dr",
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Consultant",
                            Rank = 0,
                            ShortName = "C",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "Senior Registrar",
                            Rank = 1,
                            ShortName = "SR",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "Junior Registrar",
                            Rank = 2,
                            ShortName = "JR",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "Medical Officer",
                            Rank = 3,
                            ShortName = "MO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "House Officer",
                            Rank = 4,
                            ShortName = "HO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.DentalDoctor,
                    ShortName = "Dr",
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Consultant",
                            Rank = 0,
                            ShortName = "C",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "Senior Registrar",
                            Rank = 1,
                            ShortName = "SR",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "Junior Registrar",
                            Rank = 2,
                            ShortName = "JR",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "Dental Officer",
                            Rank = 3,
                            ShortName = "DO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        },
                        new()
                        {
                            Name = "House Officer",
                            Rank = 4,
                            ShortName = "HO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dr
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.Nurse,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Chief Matron",
                            Rank = 0,
                            ShortName = "CM",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Cm
                        },
                        new()
                        {
                            Name = "Director,Nursing Services",
                            Rank = 1,
                            ShortName = "DNS",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Dns
                        },
                        new()
                        {
                            Name = "Assistant Director,Nursing Services",
                            Rank = 2,
                            ShortName = "ADNS",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Adns
                        },
                        new()
                        {
                            Name = "Chief Nursing Officer",
                            Rank = 3,
                            ShortName = "CNO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Cno
                        },
                        new()
                        {
                            Name = "Assistant Chief Nursing Officer",
                            Rank = 4,
                            ShortName = "ACNO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Acno
                        },
                        new()
                        {
                            Name = "Principal Nurse Officer",
                            Rank = 5,
                            ShortName = "PNO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Pno
                        },
                        new()
                        {
                            Name = "Senior Nurse Officer",
                            Rank = 6,
                            ShortName = "SNO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Sno
                        },
                        new()
                        {
                            Name = "Nurse Officer",
                            Rank = 7,
                            ShortName = "NO",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.No
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.Pharmacist,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Chief Pharmacist",
                            Rank = 0,
                            ShortName = "CP",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Pharm
                        },
                        new()
                        {
                            Name = "Senior Pharmacist",
                            Rank = 1,
                            ShortName = "SP",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Pharm
                        },
                        new()
                        {
                            Name = "Pharmacist",
                            Rank = 2,
                            ShortName = "P",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.Pharm
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.PharmacyTechnician,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Chief Pharmacy Technician",
                            Rank = 0,
                            ShortName = "CPT",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.PharmTech
                        },
                        new()
                        {
                            Name = "Principal Pharmacy Technician",
                            Rank = 1,
                            ShortName = "PPT",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.PharmTech
                        },
                        new()
                        {
                            Name = "Pharmacy Technician",
                            Rank = 2,
                            ShortName = "PT",
                            TenantId = tenantId,
                            IsStatic = true,
                            TitleOfAddress = TitleType.PharmTech
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.LaboratoryScientist,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Chief Medical Laboratory Scientist",
                            Rank = 0,
                            ShortName = "CMLS",
                            IsStatic = true,
                            TenantId = tenantId,
                        },
                        new()
                        {
                            Name = "Senior Medical Laboratory Scientist",
                            Rank = 1,
                            ShortName = "SMLS",
                            IsStatic = true,
                            TenantId = tenantId,
                        },
                        new()
                        {
                            Name = "Medical Laboratory Scientist",
                            Rank = 2,
                            ShortName = "MLS",
                            IsStatic = true,
                            TenantId = tenantId,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.LaboratoryTechnician,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Senior Medical Laboratory Technician",
                            Rank = 0,
                            ShortName = "SMLT",
                            TenantId = tenantId,
                            IsStatic = true,
                        },
                        new()
                        {
                            Name = "Medical Laboratory Technician",
                            Rank = 1,
                            ShortName = "MLT",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.Radiographer,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Radiographer",
                            Rank = 0,
                            ShortName = "R",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.XrayTechnician,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "X-ray Technician",
                            Rank = 0,
                            ShortName = "X-ray T",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.Physiotherapist,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Physiotherapist",
                            Rank = 0,
                            ShortName = "P",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.Dietetics,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Dietitian",
                            Rank = 0,
                            ShortName = "D",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.SocialCare,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Social Worker",
                            Rank = 0,
                            ShortName = "SW",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.FrontDesk,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                           Name = "Front Desk Personnel",
                           Rank = 0,
                           ShortName = "Reception",
                           TenantId = tenantId,
                           IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.Accountant,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Accountant",
                            Rank = 0,
                            ShortName = "Acct",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                },
                new()
                {
                    Name = StaticRoleNames.JobRoles.AdminPersonnel,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    IsStatic = true,
                    JobLevels = new List<JobLevel>
                    {
                        new()
                        {
                            Name = "Administrative Personnel",
                            Rank = 0,
                            ShortName = "Admin",
                            TenantId = tenantId,
                            IsStatic = true,
                        }
                    }
                }
            };
        }
    }
}
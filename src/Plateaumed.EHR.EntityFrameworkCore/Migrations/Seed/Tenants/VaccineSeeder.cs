using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Vaccines;

namespace Plateaumed.EHR.Migrations.Seed.Tenants;

internal static class VaccineSeeder
{
    public static void Seed(EHRDbContext context)
    {
        var vaccinesGroups = StaticVaccines.GetVaccineGroups();

        vaccinesGroups.ForEach(group =>
        {
            var existingGroup = context.VaccineGroups.IgnoreQueryFilters()
                .FirstOrDefault(v => v.Name == group.Name);

            if (existingGroup == null)
            {
                context.VaccineGroups.Add(group);
            }
        });
        
        var vaccines = StaticVaccines.GetVaccines(context.VaccineGroups.ToList());

        vaccines.ForEach(vaccine =>
        {
            var existingVaccine = context.Vaccines.Include(v => v.Schedules).IgnoreQueryFilters()
                .FirstOrDefault(v => v.Name == vaccine.Name);
            
            if (existingVaccine == null)
            {
                context.Vaccines.Add(vaccine);
            }
        });

        context.SaveChanges();
    }
}
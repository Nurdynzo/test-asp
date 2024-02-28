using Microsoft.EntityFrameworkCore.Diagnostics;
using Plateaumed.EHR.Patients;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plateaumed.EHR.EntityFrameworkCore.Interceptors;

public class StaffEncounterEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is EHRDbContext context)
        {
            var encounters = context.ChangeTracker.Entries<PatientEncounter>()
                .Select(e => e.Entity)
                .ToList();

            if (encounters.Count > 0 && eventData.EntitiesSavedCount > 0)
            {
                var userId = context.AbpSession.UserId;
                encounters.ForEach(e =>
                {
                    var staffMember = context.StaffMembers.FirstOrDefault(s => s.UserId == userId);

                    if (staffMember == null) return;

                    var staffEncounter = new StaffEncounter
                    {
                        StaffId = staffMember.Id,
                        EncounterId = e.Id
                    };

                    context.StaffEncounters.Add(staffEncounter);
                });

                context.SaveChanges();
            }
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
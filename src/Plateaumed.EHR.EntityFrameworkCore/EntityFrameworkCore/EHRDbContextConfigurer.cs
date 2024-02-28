using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.EntityFrameworkCore
{
    public static class EHRDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EHRDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EHRDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
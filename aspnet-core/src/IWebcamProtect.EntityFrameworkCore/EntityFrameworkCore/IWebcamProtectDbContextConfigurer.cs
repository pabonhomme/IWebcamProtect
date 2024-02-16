using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace IWebcamProtect.EntityFrameworkCore
{
    public static class IWebcamProtectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<IWebcamProtectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<IWebcamProtectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

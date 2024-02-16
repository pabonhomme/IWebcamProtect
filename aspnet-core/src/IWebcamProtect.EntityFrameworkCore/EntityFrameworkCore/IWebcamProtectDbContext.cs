using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using IWebcamProtect.Authorization.Roles;
using IWebcamProtect.Authorization.Users;
using IWebcamProtect.MultiTenancy;

namespace IWebcamProtect.EntityFrameworkCore
{
    public class IWebcamProtectDbContext : AbpZeroDbContext<Tenant, Role, User, IWebcamProtectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public IWebcamProtectDbContext(DbContextOptions<IWebcamProtectDbContext> options)
            : base(options)
        {
        }
    }
}

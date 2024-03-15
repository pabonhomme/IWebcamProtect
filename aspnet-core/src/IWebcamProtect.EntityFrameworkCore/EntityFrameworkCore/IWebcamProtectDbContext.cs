using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using IWebcamProtect.Authorization.Roles;
using IWebcamProtect.Authorization.Users;
using IWebcamProtect.MultiTenancy;
using IWebcamProtect.Models;

namespace IWebcamProtect.EntityFrameworkCore
{
    public class IWebcamProtectDbContext : AbpZeroDbContext<Tenant, Role, User, IWebcamProtectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<EntityType> EntityTypes { get; set; }

        public virtual DbSet<Camera> Cameras { get; set; }

        public IWebcamProtectDbContext(DbContextOptions<IWebcamProtectDbContext> options)
            : base(options)
        {
        }
    }
}

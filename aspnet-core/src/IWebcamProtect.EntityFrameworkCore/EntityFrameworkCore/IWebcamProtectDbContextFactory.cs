﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using IWebcamProtect.Configuration;
using IWebcamProtect.Web;

namespace IWebcamProtect.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class IWebcamProtectDbContextFactory : IDesignTimeDbContextFactory<IWebcamProtectDbContext>
    {
        public IWebcamProtectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IWebcamProtectDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            IWebcamProtectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(IWebcamProtectConsts.ConnectionStringName));

            return new IWebcamProtectDbContext(builder.Options);
        }
    }
}

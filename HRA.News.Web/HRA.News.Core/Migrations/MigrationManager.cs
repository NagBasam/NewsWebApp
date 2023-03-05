using FluentMigrator.Runner;
using HRA.News.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRA.News.Core.Migrations
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<IDataBase>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    databaseService.CreateDatabase("HraNewsDB");
                    migrationService.ListMigrations();
                    migrationService.MigrateUp(202303030001);
                }
                catch (Exception ex)
                {
                    //log errors
                    throw ex;
                }
            }

            return host;
        }
    }
}

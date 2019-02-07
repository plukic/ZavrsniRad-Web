using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace COAssistance.DATA.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<COAssistanceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

    
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deneysan_DAL.Entities;

namespace deneysan_DAL.Context
{
    public class Configration : DbMigrationsConfiguration<DeneysanContext>
    {
        public Configration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deneysan_DAL.Context;

namespace myBLOGData.Context
{
    public class DatabaseCreatorClass : IDatabaseInitializer<DeneysanContext>
    {
        public void InitializeDatabase(DeneysanContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
            else
            {
                context.Database.Create();
              
            }
          
          
        }
    
    }
}

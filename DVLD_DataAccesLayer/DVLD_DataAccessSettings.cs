using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccesLayer
{
    public class DVLD_DataAccessSettings
    {
        static public string ConnectionString = "Server=.;Database=DVLD_TEST;User Id=sa;Password=sa123456;";

        static public void LogExceptions(string SourceName, Exception ex)
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }
            EventLog.WriteEntry(SourceName, ex.Message.ToString(), EventLogEntryType.Error);
        }
    }
}

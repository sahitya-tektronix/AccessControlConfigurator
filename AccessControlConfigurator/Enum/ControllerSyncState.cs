using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessControlSystem.Models;


namespace AccessControlConfigurator.Enum
{
    public enum ControllerSyncState
    {
        Unknown = 0,
        SyncRequired = 1,
        Syncing = 2,
        SyncCompleted = 3,
        SyncFailed = 4,
        Offline = 5
    
}
}
